# Install Instructions

1. [Install BepInEx](https://github.com/BepInEx/BepInEx/releases): Download the latest [64-bit BepInEx version](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.4/BepInEx_win_x64_5.4.23.4.zip), extract into the game's steam install folder (in the root game folder there should be: BepInEx [folder], changelog.txt, doorstop_config.ini, winhttp.dll, along with the game's exe).

2. Run the game once and close it. You'll know if BepInEx worked if inside the BepInEx folder is now more stuff like "Config", "LogOutput.log", etc

3. Download any of my mods from the **Releases** tab

4. Extract into \MineMogul\BepInEx\plugins

# Mods

## Build Platforms Anywhere

Allows Platforms and Ramps to ignore any validity checks -- build them anywhere! 

### Version
- Mod: 1.0.0
- Tested with:
	- BepInEx: 5.4.23.4
	- MineMogul 0.1.1.0 Early Access

## Freeze Ghost

Like in Satisfactory, press H to toggle freezing a buildable ghost/hologram so you can inspect your placement more thoroughly.

While frozen, can nudge horizontally with arrow keys, or vertically with page up or down.

### Version
- Mod: 1.1.0
- Tested with:
	- BepInEx: 5.4.23.4
	- MineMogul 0.1.1.0 Early Access

<details>
	<summary>Changelog</summary>
	
	1.1.0
	- Now allows you to freeze a ghost in spot and pick up the old buildable manually without unfreezing the ghost for slightly easier swapping
</details>
	
## Hide Quest HUD

Removes HUD text for active quests (they are still active and can be viewed in the Quests screen)

### Version
- Mod: 1.1.0
- Tested with:
	- BepInEx: 5.4.23.4
	- MineMogul 0.1.1.0 Early Access
	
<details>
	<summary>Changelog</summary>
	
	1.1.0
	- Stopped game from constantly attempting to regenerate empty Quest HUD UI
</details>

## No Supports

Prevents all buildables from building supports. 

Non-destructive; if the mod is removed all supports are created normally.

### Version
- Mod: 1.0.0
- Tested with:
	- BepInEx: 5.4.23.4
	- MineMogul 0.1.1.0 Early Access
	
## Show Ore Info

Shows the types of active physics resources in the start menu's active physics object text.

### Version
- Mod: 1.1.0
- Tested with:
	- BepInEx: 5.4.23.4
	- MineMogul 0.1.1.0 Early Access
	
<details>
	<summary>Changelog</summary>
	
	1.1.0
	- Memory allocation optimization since that text runs every frame (changed it to update once a second)
</details>
	
## Delete Pickaxes

Deletes all pickaxes (including ones in your inventory) when deleting physics objects from the pause menu

(Someone bought 1000 pickaxes)

### Version
- Mod: 1.0.0
- Tested with:
	- BepInEx: 5.4.23.4
	- MineMogul 0.1.0.3 Early Access