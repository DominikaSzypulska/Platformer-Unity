# Sunny Land – 2D Platformer Game

## Table of Contents

1. [Project Overview](#project-overview)
2. [Features](#features)
3. [Game Mechanics](#game-mechanics)
4. [Project Structure](#project-structure)
5. [Requirements](#requirements)
6. [Installation and Running](#installation-and-running)
7. [Credits](#credits)

## Project Overview

**Sunny Land** is a 2D platformer game developed in Unity. The player controls a fox (Fox) navigating through levels filled with enemies, moving platforms, collectible keys, and bonus lives. The game integrates advanced mechanics such as a Game Manager for state handling and a HUD to display game information in real-time.

The project is designed to be modular and easily extensible, allowing new levels, enemies, platforms, and items to be added quickly using prefabs.

## Features

* **Player Character (Fox)**

  * Movement and jumping controls.
  * Life system with respawn upon death.
* **Enemies**

  * Patrolling enemies with Fly and Dead animations.
  * Can be defeated by jumping on top.
  * Collision with enemies reduces player life.
* **Collectibles and Bonuses**

  * Keys required to complete levels.
  * Extra lives in hard-to-reach locations.
  * Score and key collection tracking with console messages (Debug.Log).
* **Moving Platforms**

  * Platforms move horizontally in cycles.
  * Player moves together with platforms.
  * Advanced platform patterns supported.
* **Game System**

  * **GameManager** handles game states: start, pause, and level completion.
  * HUD displays lives, collected keys, and score in real-time.
  * Main menu, pause menu, and level end screens.
  * Scene switching mechanism between levels and menus.
* **Player Safety**

  * Detection of falling below the level with automatic respawn.
* **Modularity**

  * Easy to add new levels, enemies, platforms, and collectibles using prefabs.
  * HUD and GameManager can be expanded for additional game stats.

## Game Mechanics

* **Player Movement**

  * Horizontal movement and jumping using keyboard input.
  * Collision detection with platforms and enemies.
* **Enemy Behavior**

  * Patrol left and right within a defined range.
  * Death animation triggered upon player attack.
* **Collectibles**

  * Keys and extra lives trigger events upon collection.
* **Moving Platforms**

  * Platforms move independently and carry the player when standing on top.
* **Game State Management**

  * Start, pause, resume, and level completion handled by GameManager.
  * HUD updates dynamically according to player stats.

## Project Structure

```
Assets/
├─ Animations/             # All animation controllers
├─ Prefabs/                # Player, enemies, platforms, keys, and bonus prefabs
├─ Scenes/                 # Unity scenes for levels and menus
├─ Scripts/                # C# scripts for game logic
├─ Sprites/                # Player, enemies, items, and environment graphics
└─ UI/                     # HUD and menu UI assets
```

## Requirements

* Unity 2021.3 LTS or higher.
* .NET Framework 4.x or compatible scripting runtime.
* Windows or macOS for development and testing.

## Installation and Running

1. Clone or download the project repository.
2. Open Unity Hub and add the project.
3. Open the `MainMenu` scene to start the game.
4. Use keyboard controls to navigate the fox, collect keys, avoid or defeat enemies, and reach the level exit.
5. Adjust player stats, level difficulty, or add new prefabs as needed.

## Credits

* Game graphics and animations: Sunny Land Asset Pack.
* Development: [Dominika Szypulska](https://github.com/DominikaSzypulska) (under the supervision of Dr. Eng. Mariusz Szwoch)
* Additional free assets: 2D Pixel Item Asset Pack.
* Inspired by classic 2D platformer mechanics.
