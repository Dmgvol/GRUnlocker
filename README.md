[![](https://img.shields.io/badge/Jack-Unlocked-green)](https://github.com/Dmgvol/GRUnlocker/) [![](https://img.shields.io/badge/Discord-GRSR-red)](https://discord.com/invite/eZRz3Q5) ![](https://img.shields.io/github/downloads/dmgvol/grunlocker/total)


# GRUnlocker
Ghostrunner - Unlocks collectibles, levels, 100% and more!

### Available options
- Unlock collectibles (levels untouched)
- Unlock levels (collectibles untouched)
- Unlock everything (overwrites with 100% save)
- Unlock up to a specific level (1-16 with corresponding upgrades/skills)   
- New game with collectibles unlocked (overwrites with a custom new-game save)
- New game with custom sword (equipped sword yet nothing unlocked) ![](https://img.shields.io/badge/%20-%20New-brightgreen)
- Replace selected sword (equip specific sword, even if not unlocked) ![](https://img.shields.io/badge/%20-%20New-brightgreen)
- Reset deaths & times  
- Reset collectibles    
- Toggle Intros (Splash + trailer when launching the game) ![](https://img.shields.io/badge/%20-%20New-brightgreen)

** Epic Games Store Users [!] **
The EGS version is not fully supported yet, a quick workaround is to rename 
the "GhostrunnerSave.sav" to "Ghostrunner.sav", apply the unlocker and rename is back.

## Usage
#### Option 1 ![](https://img.shields.io/badge/%20-%20New-brightgreen)
The latest version introduces the config file so you don't have to move the exe/save file everytime, 
make sure you paste the full paths accordingly, for example:
```
{
  "SaveDirectory": "C:\Users\<username>\AppData\Local\Ghostrunner\Saved\SaveGames\<long-number>",
  "GameDirectory": "C:\Program Files\Steam\steamapps\common\Ghostrunner"
}
```
**You can still use it as the previous versions but with less functionality(not referring to .sav file).**

#### Option 2 (Easiest)
- Copy GRUnlocker.exe to the save directory and run it from there.
- It will overwrite the current save.

#### Option 3
- Copy your ```.sav``` file to the same directory as the GRUnlocker.exe.
- Run the program and choose one of the given options.
- Copy back the updated ```.sav``` file to GR save folder.

#### Starting arguments
You can start the program with the digit of the choice in the args.

## GR save file location
### Windows (Steam)
```%LOCALAPPDATA%\Ghostrunner\Saved\SavedGames\<long-number>\Ghostrunner.sav```
### Windows (Epic Games Store)
```%LOCALAPPDATA%\Ghostrunner\Saved\SavedGames\GhostrunnerSave.sav```
### Linux
```<steamfolder>/steamapps/compatdata/1139900/pfx/drive_c/users/steamuser/Local Settings/Application Data/Ghosteamuser/Local Settings/Application Data/Ghostrunner/Saved/SaveGames/<long-number>/Ghostrunner.sav```

## Who it is aimed at?
Mostly for players that finished the game and want to practice or speedrun the game with their favorite katanas.
If you haven't finished the game nor collected all the collectibles beforehand.. I'll highly recommend to do so.

## Disclaimer
Make sure you backup your .save file before you run the program! (just in case).

This program/script does not guarantee you'll receive the corresponded achievements if any at all, so if you want to get those achievements, you should do it yourself.

## Reporting
If you encounter any type of issues/bugs or even suggestions, @ me in Discord.
