using System;
using System.Collections.Generic;
using System.Reflection;
using ICities;
using HarmonyLib;
using CitiesHarmony.API;

namespace MetropolisBuildingRules
{
    public sealed class Mod : IUserMod
    {
        public string Name { get { return "Metropolis Building Rules (alpha)"; } }
        public string Description { get { return "Opt-in growable height and asset-name preferences."; } }

        public void OnEnabled()
        {
            Patches.RequestApply();
            HarmonyHelper.DoOnHarmonyReady(delegate { Patches.Apply(); });
        }

        public void OnDisabled()
        {
            Patches.Remove();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group = helper.AddGroup("Metropolis Building Rules / Regole edifici");
            group.AddCheckbox("Enable rules / Attiva regole", Settings.Enabled,
                delegate(bool value) { Settings.Enabled = value; });
            group.AddSlider("Preferred maximum height (m) / Altezza massima preferita", 8f, 300f, 1f,
                Settings.MaximumHeight, delegate(float value) {
                    Settings.MaximumHeight = value;
                    Settings.MinimumHeight = Math.Min(Settings.MinimumHeight, value);
                });
            group.AddSlider("Preferred minimum height (m) / Altezza minima preferita", 0f, 300f, 1f,
                Settings.MinimumHeight, delegate(float value) {
                    Settings.MinimumHeight = Math.Min(value, Settings.MaximumHeight);
                });
            group.AddTextfield("Asset name contains / Nome asset contiene", Settings.NameContains,
                delegate(string value) { Settings.NameContains = value ?? string.Empty; });
        }
    }

    public static class Settings
    {
        public static bool Enabled;
        public static float MaximumHeight = 60f;
        public static float MinimumHeight;
        public static string NameContains = string.Empty;
    }

    public static class Patches
    {
        private const string Id = "org.citiesskylines-addons.metropolis-building-rules";
        private static bool _applied;
        private static bool _wanted;

        public static void RequestApply() { _wanted = true; }

        public static void Apply()
        {
            if (!_wanted || _applied) return;
            try
            {
                new Harmony(Id).PatchAll(Assembly.GetExecutingAssembly());
                if (_wanted) _applied = true;
                else new Harmony(Id).UnpatchAll(Id);
            }
            catch (Exception error)
            {
                UnityEngine.Debug.LogError("[MetropolisBuildingRules] Patch failed: " + error);
            }
        }

        public static void Remove()
        {
            _wanted = false;
            if (!_applied) return;
            new Harmony(Id).UnpatchAll(Id);
            _applied = false;
        }
    }

    internal struct SpawnKey : IEquatable<SpawnKey>
    {
        public ItemClass.Service Service;
        public ItemClass.SubService SubService;
        public ItemClass.Level Level;
        public int Width;
        public int Length;
        public BuildingInfo.ZoningMode ZoningMode;

        public bool Equals(SpawnKey other)
        {
            return Service == other.Service && SubService == other.SubService && Level == other.Level &&
                Width == other.Width && Length == other.Length && ZoningMode == other.ZoningMode;
        }

        public override bool Equals(object other) { return other is SpawnKey && Equals((SpawnKey)other); }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)Service;
                hash = hash * 31 + (int)SubService;
                hash = hash * 31 + (int)Level;
                hash = hash * 31 + Width;
                hash = hash * 31 + Length;
                return hash * 31 + (int)ZoningMode;
            }
        }
    }

    [HarmonyPatch(typeof(BuildingManager), "GetRandomBuildingInfo")]
    internal static class GrowablePatch
    {
        private sealed class CacheEntry
        {
            public readonly List<BuildingInfo> Infos = new List<BuildingInfo>();
            public readonly List<BuildingCandidate> Candidates = new List<BuildingCandidate>();
        }

        private static readonly Dictionary<SpawnKey, CacheEntry> Cache =
            new Dictionary<SpawnKey, CacheEntry>();

        public static void ClearCache() { Cache.Clear(); }

        [HarmonyPostfix]
        private static void Postfix(ref BuildingInfo __result, ItemClass.Service service,
            ItemClass.SubService subService, ItemClass.Level level, int width, int length,
            BuildingInfo.ZoningMode zoningMode, int style)
        {
            if (!Settings.Enabled || __result == null || style != 0) return;
            SpawnKey key = new SpawnKey {
                Service = service, SubService = subService, Level = level,
                Width = width, Length = length, ZoningMode = zoningMode
            };
            CacheEntry compatible;
            if (!Cache.TryGetValue(key, out compatible))
            {
                compatible = new CacheEntry();
                int count = PrefabCollection<BuildingInfo>.LoadedCount();
                for (uint i = 0; i < count; i++)
                {
                    BuildingInfo info = PrefabCollection<BuildingInfo>.GetLoaded(i);
                    if (info == null || info.m_class == null) continue;
                    if (info.m_class.m_service != service || info.m_class.m_subService != subService ||
                        info.m_class.m_level != level || info.m_cellWidth != width ||
                        info.m_cellLength != length || info.m_zoningMode != zoningMode) continue;
                    compatible.Candidates.Add(new BuildingCandidate(info.name, info.m_size.y, compatible.Infos.Count));
                    compatible.Infos.Add(info);
                }
                Cache[key] = compatible;
            }
            int vanillaIndex = -1;
            for (int i = 0; i < compatible.Infos.Count; i++)
            {
                BuildingInfo info = compatible.Infos[i];
                if (info == __result) vanillaIndex = i;
            }
            if (vanillaIndex < 0) return; // Another mod may have selected a special asset.
            int selected = RuleEngine.Choose(compatible.Candidates, vanillaIndex, __result.m_size.y,
                Settings.MinimumHeight, Settings.MaximumHeight, Settings.NameContains);
            if (selected >= 0 && selected < compatible.Infos.Count) __result = compatible.Infos[selected];
        }
    }

    public sealed class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelUnloading()
        {
            GrowablePatch.ClearCache();
            base.OnLevelUnloading();
        }
    }
}
