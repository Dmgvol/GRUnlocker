[![](https://img.shields.io/badge/Jack-Unlocked-green)](https://github.com/Dmgvol/GRUnlocker/) [![](https://img.shields.io/badge/Discord-GRSR-red)](https://discord.com/invite/eZRz3Q5) ![](https://img.shields.io/github/downloads/dmgvol/grunlocker/total)


# GRUnlocker
Ghostrunner - Unlocks collectibles, levels, 100% save, and more!

**New Update! Supports Winter-Patch (hardcore levels)!**

### Available options
- Unlock collectibles (levels untouched)
- Unlock all levels (classic + hardcore support) (collectibles untouched)     ![](https://img.shields.io/badge/%20-%20New-brightgreen)   
- Unlock everything (classic + hardcore support) (overwrites with 100% save)  ![](https://img.shields.io/badge/%20-%20New-brightgreen)
- Unlock up to a specific level (1-16 with corresponding upgrades/skills)   
- Unlock up to a specific hardcore level (1-16)                               ![](https://img.shields.io/badge/%20-%20New-brightgreen)
- New game with collectibles unlocked (overwrites with a custom new-game save)
- New game with custom sword (equipped sword yet nothing unlocked) 
- Replace selected sword (equip specific sword, even if not unlocked) 
- Reset deaths & times  
- Reset collectibles    
- Toggle Intros (Splash + trailer when launching the game)


## Usage
### Options 1 ![](https://img.shields.io/badge/%20-%20New-brightgreen)
The latest version introduces the 'save-file auto detect', GRUnlocker will attempt to find the default save file location.
Will display "Save Path: Auto" if successfully found the save file.

#### Option 2 (Easiest)
- Copy GRUnlocker.exe to the save directory and run it from there.

#### Option 3 
Using the config file (can create through the GRUnlocker)
make sure you paste the full paths accordingly, for example:
```
{
  "SaveDirectory": "C:\Users\<username>\AppData\Local\Ghostrunner\Saved\SaveGames\<long-number>",
  "GameDirectory": "C:\Program Files\Steam\steamapps\common\Ghostrunner"
}
```
**Valid config file is required for 'Toggle Intros' Feature**

#### Option 4 (Old)
- Copy your ```.sav``` file to the same directory as the GRUnlocker.exe.
- Run the program and choose one of the given options.
- Copy back the updated ```.sav``` file to GR save folder.

#### Starting arguments
```-displaypath``` - Will display the current save file path which it is working with.

~~You can start the program with the digit of the choice in the args.~~ (Removed in latest version)

## GR save file location
### Windows (Steam/GOG)
```%LOCALAPPDATA%\Ghostrunner\Saved\SavedGames\<long-number>\Ghostrunner.sav```
### Windows (Epic Games Store)
```%LOCALAPPDATA%\Ghostrunner\Saved\SavedGames\GhostrunnerSave.sav```
### Linux
```<steamfolder>/steamapps/compatdata/1139900/pfx/drive_c/users/steamuser/Local Settings/Application Data/Ghosteamuser/Local Settings/Application Data/Ghostrunner/Saved/SaveGames/<long-number>/Ghostrunner.sav```

## Who it is aimed at?
Mostly for players that finished the game and want to practice or speedrun the game with their favorite katanas or specific progress unlocked.
Or people which lost their save files and want to recover from where they've left.
If you haven't finished the game nor collected all the collectibles beforehand.. I'll highly recommend doing so.

## Disclaimer
Make sure you backup your .save file before you run the program! (just in case).
I'm not responsible for any progress loss or/and achievements completions, so keep a backup prior to using it.

### Speedrunning
Please note that New-Game modifications (collectibles unlocked or custom sword) are not allowed for use during full runs at this time.
However, you're allowed to do ILs(Individual Levels) with it.

### Achievements
The GRUnlocker may and **will** trigger different achievements, you have been warned!
So if you want to get those achievements, you should do it by yourself.
(Thanks to animayyo for testing and finding this out)

## Reporting
If you encounter any type of issues/bugs or even suggestions, @ me on Discord.
