# Lanes of Time

A **real-time strategy (RTS) game for Android** developed in **Unity (C#)**.  
Two bases from different historical eras fight for control of time across a battlefield divided into five lanes.  
Available in **Campaign mode** (single-player vs AI) and **local 1v1 mode** (two players on the same device).

---

## Overview

Lanes of Time was developed as the final project for the **Game Design** course at ISEL, evolving over several months from the initial concept through to a fully playable build with user playtesting.  
The project focused on **innovative mechanic design**, **AI development** and **optimisation for mid-range Android devices**.

| **Category** | **Description** |
|---|---|
| **Engine** | Unity (URP) |
| **Language** | C# |
| **Platform** | Android (landscape orientation) |
| **Game Modes** | Campaign (vs AI) · Local 1v1 (shared screen) |
| **Match Duration** | ~5 minutes |
| **Development Time** | 1 semester (solo project) |

---

## Concept

In *Lanes of Time*, the player takes the role of commander of a civilisation that can travel between **five distinct historical eras**: Prehistory, Classical Antiquity, Middle Ages, Industrial Era and the Future. The goal is simple: **destroy the enemy base** while protecting your own.

The game rejects the idea of memorising a fixed winning strategy. The **dynamic economy**, **temporary combos** and **reactive audience** ensure that the best decision always depends on the current state of the battlefield — rewarding improvisation, creativity and adaptation.

---

## Screenshots

<p align="center">
  <img src="images/menu_inicial.png" width="49%" alt="Main Menu"/>
  <img src="images/menu_campanha.png" width="49%" alt="Campaign Menu"/>
</p>
<p align="center">
  <em>Main Menu (left) · Campaign Menu (right)</em>
</p>

<p align="center">
  <img src="images/menu_melhorias.png" width="49%" alt="Upgrades Menu"/>
  <img src="images/gameplay.png" width="49%" alt="Gameplay"/>
</p>
<p align="center">
  <em>Upgrades Menu (left) · Battlefield gameplay (right)</em>
</p>

---

## Core Mechanics

### Era System
When the experience bar fills up, the player automatically advances to the next era, unlocking:
- A unique set of **three troop classes** (*melee*, *ranged*, special) with aesthetics adapted to the historical period.
- An era-exclusive **catastrophic event** (asteroids, area-damage gladiators, trains, toxic smoke, mass summoning).
- A **distinct base skin** matching the era.

### Dynamic Economy
Gold is the primary resource, earned automatically over time. The core innovation is a **supply-and-demand system**: each time a specific troop is summoned repeatedly, its cost temporarily increases while less-used troops become cheaper. This forces the player to diversify their choices instead of spamming the same unit.

### Troop Fusion
When two troops of the same class meet on the same lane, they **automatically merge** into a stronger unit with a visual effect. Fusing troops increases the likelihood of earning audience support.

### Combo System
During each match, the game defines **specific summoning sequences** for each player. Executing those sequences within a short time window completes a combo, granting gold or experience rewards and increasing the chance of audience approval.

### Reactive Audience
2D spectator stands observe the battle and react positively to fusions, combos and loot box collection. When impressed, the audience offers **random rewards** (bonus gold, experience), adding a layer of unpredictability and creating secondary objectives throughout the match.

### Loot Boxes
Occasionally, loot crates parachute down randomly into one of the lanes. The first player to collect them earns bonus gold or experience — creating a secondary objective that forces risky decisions.

---

## Technical Implementation

### Key Scripts

**`GameManager.cs`**  
Manages the overall match state. Controls available eras, troop spawning across the five lanes, game modes, era-specific special events and victory conditions.

**`PlayerScript.cs`**  
Handles each player's resources and progression. Controls gold, experience, the XP bar and era transitions.

**`UIManager.cs`**  
Centralises the interface and player input. Manages troop buttons, lane selection, visual cooldowns, dynamic pricing, the match timer and the end-of-match screen with statistics.

**`CharacterScript.cs`**  
Controls the behaviour of individual units: automatic movement, *melee* or *ranged* attacks, health management, gold/XP rewards on death, **automatic troop fusion** and target selection within the lane.

### Script Architecture

```text
Scripts/
├── GameManager.cs
├── PlayerScript.cs
├── UIManager.cs
└── CharacterScript.cs
```

### Collision Detection
Uses Unity's *collider* and *trigger* system. Physical colliders ensure troops and projectiles interact correctly with the ground and bases. Triggers handle attack range, troop fusion and loot box collection. Tag-based differentiation prevents friendly fire between troops on the same side.

### Campaign AI
The opponent is AI-controlled with parameters that scale progressively across 15 levels, increasing troop health, attack and speed. The AI replicates the same mechanics available to the player — troop summoning, era switching and catastrophic event usage.

### Rendering
The game uses a **3D world with a fixed orthographic camera** and a 2D UI overlay. Bases, troops and loot boxes are low-poly 3D models. The audience is a 2D texture on background stands. All UI elements (bars, buttons, timer) are built with a 2D Canvas.

---

## Visual Design

### Era Identity
Each era has its own colour palette and environment, reinforcing the emotional identity of each period:

| **Era** | **Palette** | **Atmosphere** |
|---|---|---|
| Prehistory | Brown · Burnt orange | Warm, primitive, organic |
| Classical Antiquity | Gold · Beige · Light yellow | Glorious, balanced, arid |
| Middle Ages | Dark green · Brown · Dark red | Heavy, rustic, conflict-ridden |
| Industrial | Grey · Blue-grey · Rust | Mechanical, worn, progressive |
| Future | Deep blue · Electric blue · Cyan | Cold, technological, digital |

### Visual Concept
The game is presented as a **temporal colosseum** where civilisations from different eras are summoned to fight. In 1v1 mode the arena is neutral with 2D audience stands. In Campaign mode each battle takes place in a thematic environment matching the level's era.

---

## Design Evolution

The *Lanes of Time* concept went through several iterations over the course of the semester:

**Version 1.00** — Core concept with lane-based battles between historical eras, a fixed economy and loot boxes. Inspired by *Age of War* and *Warlords: Call to Arms*.

**Version 1.10** — After a Likert-scale peer survey, innovation was identified as the weakest point. Introduced: **supply-and-demand economy**, **troop fusion**, **combos** and **reactive audience**. Removed low-cohesion elements (pedometer integration, random lane portals).

**Version 2.00** — Consolidated with concept art, mood boards, colour script and era visual identity. Five complete eras with unique troops, bases and catastrophic events.

---

## Playtesting

Campaign mode was evaluated with **10 participants** via online questionnaire and direct observation.

| **Metric** | **Result** |
|---|---|
| Overall experience score | **7.4 / 10** |
| Would recommend the game | **100%** |
| Would play again | **80%** |
| Control responsiveness | **4.6 / 5** |
| Decisions felt impactful | **80% positive** |
| Average level reached | **7.7 / 15** |

**Key strengths:** Variety of era-specific events, progression through historical eras, match pacing and visual presentation.

**Main areas for improvement:** Dynamic economy clarity, touch target sizing, early learning curve and troop type distinction.

---

## Features

- Five playable eras with distinct bases, troops and events.
- Battlefield with five parallel lanes.
- Dynamic economy with variable costs based on supply and demand.
- Parachute loot boxes that drop randomly.
- Automatic troop fusion.
- Combo system with rewards.
- Audience that reacts to standout plays.
- Local 1v1 mode on a shared screen.
- 5-minute timer with an end-of-match statistics screen.
- Permanent upgrade system between levels (attack, defence, speed, audience, events).

---

## Challenges & Solutions

**Challenge:** Communicating the dynamic economy clearly without interrupting match flow.  
**Solution:** Dedicated tutorial for the mechanic, visual indicators on troop prices and contextual pop-ups during play.

**Challenge:** Balancing AI difficulty to be challenging without frustrating casual players.  
**Solution:** Progressive parameter scaling across 15 levels, with a gentler curve in the first three levels.

**Challenge:** Keeping the interface readable with multiple troops, lanes, pop-ups and buttons on a mobile screen.  
**Solution:** Clear visual hierarchy with 3D elements for gameplay components and 2D UI for information, fixed orthographic camera and distinct colour-coded button categories.

**Challenge:** Implementing local 1v1 on a single device without players interfering with each other's controls.  
**Solution:** Mirrored interface with each player's buttons on opposite sides of the screen and tag-based action differentiation.

---

## Links
[Play on Itch.io](https://gamedevrafael.itch.io/lanes-of-time)  
[Visit my LinkedIn](https://www.linkedin.com/in/rgtd-faustino)
