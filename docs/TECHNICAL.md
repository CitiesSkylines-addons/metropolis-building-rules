# Technical design and validation / Disegno tecnico e validazione

## English

The Harmony postfix on `BuildingManager.GetRandomBuildingInfo` runs only when enabled, when the vanilla result is non-null, and when the game's `style` argument is zero. A cache keyed by service, subservice, level, width, depth and zoning mode avoids rescanning every loaded prefab on every spawn. `RuleEngine.Choose` keeps the vanilla asset if it satisfies the preferred minimum/maximum height interval and optional name keyword; otherwise it picks the closest compatible height within that interval. It falls back to vanilla when no candidate matches. The cache is cleared when a level unloads. If the settings momentarily form an invalid interval, selection also falls back to vanilla.

The build script compiles against local game and CitiesHarmony assemblies. It runs seven pure rule checks for accepted assets, replacement, combined filters, interval filtering and fallback. These tests do not prove that a city loads or that Harmony patches coexist with other mods.

Before a stable release, run a copied-save matrix: clean CS1, CitiesHarmony only, then with Building Themes 2, TM:PE and Move It separately. For each, check enable/disable, new zone growth, district styles, level upgrades, empty lots, game log, save/reload, and whether candidate asset availability changes. Record a baseline number of spawns and frame/tick times. If a conflict appears, disable this mod and document the exact combination.

## Italiano

La postfix Harmony su `BuildingManager.GetRandomBuildingInfo` si esegue solo se attiva, con risultato vanilla non nullo e argomento `style` uguale a zero. Una cache indicizzata per servizio, sottoservizio, livello, larghezza, profondità e modalità di zoning evita la scansione di tutti gli asset a ogni costruzione. `RuleEngine.Choose` conserva l’asset vanilla se rispetta l’intervallo di altezza minima/massima e la parola facoltativa; altrimenti sceglie l’altezza compatibile più vicina nell’intervallo. Se non trova candidati, conserva l’asset vanilla. La cache viene svuotata alla chiusura del livello. Se le impostazioni creano momentaneamente un intervallo invalido, resta la scelta vanilla.

Lo script compila contro le librerie locali di gioco e CitiesHarmony. Esegue sette controlli indipendenti su asset già valido, sostituzione, filtri combinati, intervallo e fallback. I controlli non dimostrano che una città si carichi o che le patch Harmony convivano con altre mod.

Prima di una release stabile serve una matrice su salvataggi copiati: CS1 pulito, solo CitiesHarmony, poi Building Themes 2, TM:PE e Move It separatamente. Verificare attivazione, disattivazione, nuove zone, stili distrettuali, passaggi di livello, lotti vuoti, log, salvataggio/ricaricamento e disponibilità degli asset. Registrare numero di nuove costruzioni e tempi di frame/tick di base. Se emerge un conflitto, disattivare la mod e documentare la combinazione esatta.
