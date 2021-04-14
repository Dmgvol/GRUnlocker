using System;
using System.Collections.Generic;
using System.Linq;

namespace GRUnlocker {
    public class InputHandler {

        private List<OptionCore> options = new List<OptionCore>();

        public InputHandler() {
            // print intro
            Console.WriteLine("════Ghostrunner Unlocker [by DmgVol]════");
            Console.WriteLine($"Save Path: {Config.getInstance().saveLocation}\nSave Type: {Config.getInstance().saveType}");
            if(Config.getInstance().DisplayPath) Console.WriteLine($"Path: {Config.getInstance().SaveFilePath}");
            Console.WriteLine();
            bool SavFileFound = CheckSaveExists(false);
            
            if(SavFileFound) {
                // ==levels==
                options.Add(new Option_UnlockAllCollectibles());
                options.Add(new Option_UnlockAllLevels());
                options.Add(new Option_UnlockAllLevelsHC());
                options.Add(new Option_UnlockAllLevelsKR());
                // everything
                options.Add(new Option_UnlockEverything());         
                options.Add(new Option_UnlockEverythingHC());
                options.Add(new Option_UnlockEverythingKR());
                // up to level
                options.Add(new Option_UnlockUpToLevel());          
                options.Add(new Option_UnlockUpToLevelHC());  
                // other
                options.Add(new Option_NewGameCollectibles());      
                options.Add(new Option_NewGameSword());
                options.Add(new Option_NGKR());
                options.Add(new Option_ReplaceSelectedSword());     
                // ==resetting==
                options.Add(new Option_ResetLevelDetails());
                options.Add(new Option_ResetCollectibles());
                options.Add(new Option_ToggleIntros());

                // fake
                options.Add(new Option_0XDLC());
            }
        }

        public void Handle() {
            Console.WriteLine("─────────────────────────────────");
            Console.WriteLine("───Options (type in the digit)───");
            int[] spacers = new int[4] { 3, 6, 8, 12 };
            for(int i = 0; i < options.Count; i++) {
                Console.WriteLine($"{i + 1} - {options[i].desc}");
                if(spacers.Contains(i)) Console.WriteLine();
            }
            Console.WriteLine("─────────────────────────────────");

            // read input
            Console.Write("\nOption:\t");
            string input = Console.ReadLine();
            

            // valid?
            bool successFlag = true;
            int optionNumber;
            if(Int32.TryParse(input, out optionNumber) && optionNumber > 0 && optionNumber < options.Count + 1) {
                int i = optionNumber - 1;
                // special case - unlock up to level, requires more input
                if(options[i] is Option_UnlockUpToLevel || options[i] is Option_UnlockUpToLevelHC) {
                    Console.WriteLine("Up to which level to unlock? (1-16)");
                    Console.WriteLine("─────────────────────────────────");
                    Console.Write(">");
                    optionNumber = GetOptionNumber(0, 17);
                    successFlag = options[i].Patch(optionNumber);
                } else if(options[i] is Option_ReplaceSelectedSword || options[i] is Option_NewGameSword) {
                    // special case - replace selected sword, requires more input
                    Console.WriteLine("Select your sword: (no pre-order swords)");
                    Console.WriteLine(SwordData.DisplaySwordNames());
                    Console.WriteLine("─────────────────────────────────");
                    Console.Write("Sword number: ");
                    optionNumber = GetOptionNumber(-1, 15);
                    successFlag = options[i].Patch(optionNumber);
                } else {
                    // patch and return result
                    successFlag = options[i].Patch();
                }
            } else {
                Console.WriteLine("Error: invalid input.");
                ExitProgram();
            }

            // print results
            if(successFlag)
                Console.WriteLine("Patched successfully!");
            else
                Console.WriteLine("Error: failed to patch!\n-Something went wrong, missing permissions/files or corrupted .sav file");

            ExitProgram();
        }

        private int GetOptionNumber(int min, int max) {
            int optionNumber = -1;
            string input = Console.ReadLine();
            if(Int32.TryParse(input, out optionNumber) && optionNumber > min && optionNumber < max) {
                return optionNumber;
            }

            Console.WriteLine("Error: invalid input.");
            ExitProgram();
            return -1;
        }

        public static bool CheckSaveExists(bool exitIfMissing = true) {
            if(!Unlocker.FileExists()) {
                Console.WriteLine("Error: Missing .sav file!");

                // error msg based on save location
                switch(Config.getInstance().saveLocation) {
                    case Config.SaveLocation.Local:
                        Console.WriteLine("-Make sure the save file is in the same directory as the executable.");
                        break;
                    case Config.SaveLocation.Remote:
                        Console.WriteLine("-Save file is missing from the given path (config file).");
                        break;
                    case Config.SaveLocation.Auto:
                        Console.WriteLine("-Can't find save file.");
                        break;
                }
              
                if(exitIfMissing) ExitProgram();
                return false;
            }
            return true;
        }
        public static void ExitProgram() {
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}