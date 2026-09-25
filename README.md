# Metropolis Building Rules · CS1

**v0.1.0-alpha.1 — experimental source release.** Compiles against the local Epic build `1.21.1-f9-epic-win` with CitiesHarmony `2.2.2-0`; in-game spawning and compatibility have not yet been validated.

## English

This opt-in mod intercepts CS1's choice of a **new growable building**. If the vanilla choice exceeds the preferred height or does not contain a requested asset-name keyword, it looks for another loaded asset with the **same service, subservice, level, cell width, cell depth and zoning mode**. If no candidate matches, the vanilla choice remains. The rule is off by default, runs only when the game's `style` argument is zero, and affects future spawns only.

The height value comes from the asset's `BuildingInfo.m_size.y`; it is an asset dimension, not a true editable floor count. A name keyword is a simple asset filter, not an architectural style taxonomy. This release does **not** change meshes, individual existing buildings, setbacks, floor use or mixed-use simulation. Settings are session-only. This patch touches the same selection area as other theme mods, so use a copied save and avoid combining it with Building Themes 2 until compatibility is measured.

### Build and try

`./build.ps1` builds `dist/MetropolisBuildingRules.dll` and runs five rule checks. It references the installed game and CitiesHarmony; no third-party DLL is included in the repository. Copy only the resulting DLL to a new folder under `Files/Mods`. Ensure CitiesHarmony is installed and enabled, load a **copy** of a save, enable the rule in Mod Options, set a preferred maximum height and optional asset-name keyword, then zone a new vanilla-style area. Disable the rule and reload the copied save if any spawn behavior is unexpected. No prebuilt binary is distributed with this alpha.

Read [technical design and validation](docs/TECHNICAL.md), [roadmap](docs/ROADMAP.md), and the [suite overview](https://github.com/CitiesSkylines-addons/metropolis-performance-lab/blob/main/docs/SUITE.md).

## Italiano

Questa mod facoltativa interviene sulla scelta di un **nuovo edificio growable**. Se l’asset vanilla supera l’altezza preferita o non contiene la parola scelta nel nome, cerca un altro asset caricato con **lo stesso servizio, sottoservizio, livello, larghezza e profondità in celle e modalità di zoning**. Se non trova un candidato, mantiene la scelta vanilla. La regola è disattivata all’avvio, opera solo quando l’argomento `style` del gioco è zero e riguarda solo le nuove costruzioni.

L’altezza proviene da `BuildingInfo.m_size.y`: è una dimensione dell’asset, non un numero di piani modificabile. La parola nel nome è un filtro semplice, non una classificazione di stili architettonici. Questa versione **non** modifica mesh, edifici esistenti, arretramenti, destinazione dei piani o simulazione mista. Le impostazioni valgono solo per la sessione. La patch interviene nella stessa area delle mod di temi: usare un salvataggio copiato e non combinarla con Building Themes 2 finché la compatibilità non è stata misurata.

### Compilazione e prova

`./build.ps1` produce `dist/MetropolisBuildingRules.dll` ed esegue cinque controlli. Usa le librerie installate del gioco e di CitiesHarmony; il repository non include DLL di terzi. Copiare solo la DLL risultante in una nuova cartella sotto `Files/Mods`. Verificare che CitiesHarmony sia installato e attivo, caricare una **copia** del salvataggio, attivare la regola nelle opzioni, scegliere altezza massima preferita e parola facoltativa, poi zonizzare un’area con stile vanilla. Disattivare la regola e ricaricare la copia se il comportamento è inatteso. Questa alfa non distribuisce binari precompilati.

Leggere [disegno tecnico e validazione](docs/TECHNICAL.md) e [roadmap](docs/ROADMAP.md).

## Discoverability / Ricerca

**Topics:** `cities-skylines`, `cities-skylines-1`, `cities-skylines-mod`, `csharp`, `building-control`, `growables`, `zoning`

**Keywords:** growable building height, footprint, asset selection, architecture, city builder; altezza edifici, dimensioni, asset, stili, urbanistica.

## Support / Donazioni

Optional / Facoltative: [Ko-fi](https://ko-fi.com/mrjonam) · [Buy Me a Coffee](https://www.buymeacoffee.com/mrjonam) · [PayPal](https://paypal.me/manorollo).

Original source: MIT. Game assemblies, CitiesHarmony and other third-party code are not redistributed. Independent of Colossal Order, Paradox and Citystate Metropolis.
