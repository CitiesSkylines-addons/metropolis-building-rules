# Roadmap / Piano di sviluppo

Goal / Obiettivo: bring Citystate Metropolis-style **building choices** to CS1 through compatible existing assets first; study procedural visuals and simulation separately / portare in CS1 le **scelte sugli edifici** usando prima gli asset esistenti compatibili; studiare separatamente immagini procedurali e simulazione. See [comparison](CITYSTATE_COMPARISON.md) / Vedi [confronto](CITYSTATE_COMPARISON.md).

## Done in source / Fatto nel sorgente

- `0.1`: opt-in maximum prefab height and asset-name keyword for new vanilla-style growables / altezza massima facoltativa e parola nel nome per nuovi growable vanilla.
- Current development: optional minimum height creates a preferred height interval; seven pure selector checks / altezza minima facoltativa crea un intervallo preferito; sette controlli indipendenti sul selettore.

## Next deliverables / Prossimi risultati

1. **Persistence and safety / Persistenza e sicurezza.** Save settings via CS1's supported settings path and reset on disable; cap cache work and log fallback counts. Acceptance / Criterio: copied-save enable, disable, reload and upgrade matrix; no changed existing prefab / matrice di attivazione, disattivazione, ricarica e avanzamento su copia; nessun prefab esistente cambiato.
2. **Named asset profiles / Profili asset nominati.** Explicit allowlists for building families and facade/color variants, with width, depth, service, level and zoning checks. Acceptance / Criterio: each profile previews available assets, missing assets fall back to vanilla, theme-mod combinations tested / ogni profilo mostra gli asset disponibili; quelli mancanti tornano a vanilla; prove con mod di temi.
3. **District and lot context / Contesto di distretto e lotto.** Identify the spawn site in zone growth and resolve a versioned plan from `metropolis-terrain-lots`. Acceptance / Criterio: adjacent lots can choose different compatible profiles; no leakage across districts, threads or upgrades; absent bridge uses global rule / lotti vicini usano profili diversi; nessuna contaminazione tra distretti, thread o avanzamenti; senza collegamento resta la regola globale.
4. **Setbacks and parking / Arretramenti e parcheggi.** Research reserved cells, decorative props and road access before controls. Acceptance / Criterio: visible changes and save/reload verified on a copied city; traffic and occupation retain vanilla fallback / modifiche visibili e salvataggio verificati su copia; traffico e occupazione conservano fallback vanilla.
5. **Procedural building feasibility / Fattibilità edifici procedurali.** Separate proof for meshes, LOD, materials, collisions, zoning, asset lifecycle, save/load, performance and third-party rights. Acceptance / Criterio: playable copied-save prototype with measured resource use and no game-asset redistribution / prototipo giocabile su copia con risorse misurate e nessuna redistribuzione di asset del gioco.

Exact floors, mixed uses and Citystate's socioeconomic/crowd simulation are not features of the current selector / Piani esatti, usi misti e simulazione socioeconomica/per gruppi di Citystate non sono funzioni del selettore attuale. No dates or parity claims / Nessuna data né promessa di equivalenza.
