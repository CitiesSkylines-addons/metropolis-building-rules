using System;
using System.Collections.Generic;

namespace MetropolisBuildingRules
{
    public struct BuildingCandidate
    {
        public readonly string Name;
        public readonly float HeightMeters;
        public readonly int Index;

        public BuildingCandidate(string name, float heightMeters, int index)
        {
            Name = name ?? string.Empty;
            HeightMeters = heightMeters;
            Index = index;
        }
    }

    public static class RuleEngine
    {
        public static int Choose(IList<BuildingCandidate> candidates, int vanillaIndex,
            float vanillaHeight, float maximumHeight, string nameContains)
        {
            return Choose(candidates, vanillaIndex, vanillaHeight, 0, maximumHeight, nameContains);
        }

        public static int Choose(IList<BuildingCandidate> candidates, int vanillaIndex,
            float vanillaHeight, float minimumHeight, float maximumHeight, string nameContains)
        {
            if (candidates == null) throw new ArgumentNullException("candidates");
            if (float.IsNaN(minimumHeight) || minimumHeight < 0) throw new ArgumentOutOfRangeException("minimumHeight");
            if (float.IsNaN(maximumHeight) || maximumHeight < 0) throw new ArgumentOutOfRangeException("maximumHeight");
            if (maximumHeight > 0 && minimumHeight > maximumHeight) return vanillaIndex;
            string keyword = (nameContains ?? string.Empty).Trim();
            bool vanillaFits = vanillaHeight >= minimumHeight &&
                (maximumHeight == 0 || vanillaHeight <= maximumHeight) &&
                (keyword.Length == 0 || ContainsName(candidates, vanillaIndex, keyword));
            if (vanillaFits) return vanillaIndex;

            int best = vanillaIndex;
            float bestDistance = float.MaxValue;
            foreach (BuildingCandidate candidate in candidates)
            {
                if (float.IsNaN(candidate.HeightMeters) || candidate.HeightMeters <= 0) continue;
                if (candidate.HeightMeters < minimumHeight) continue;
                if (maximumHeight > 0 && candidate.HeightMeters > maximumHeight) continue;
                if (keyword.Length > 0 && candidate.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0) continue;
                float distance = Math.Abs(candidate.HeightMeters - vanillaHeight);
                if (distance < bestDistance || (distance == bestDistance && candidate.Index < best))
                {
                    bestDistance = distance;
                    best = candidate.Index;
                }
            }
            return best; // If no candidate matches, keep the vanilla asset.
        }

        private static bool ContainsName(IList<BuildingCandidate> candidates, int index, string keyword)
        {
            foreach (BuildingCandidate candidate in candidates)
                if (candidate.Index == index)
                    return candidate.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
            return false;
        }
    }
}
