using System;

namespace GRUnlocker {
    public class InputHandler {

        private readonly string[] options = new string[] {
            "Unlock all collectibles",
            "Unlock all levels",
            "Unlock everything (100%)",
            "Unlock up to a specific level (1-16)",
            "New-game with collectibles unlocked",
            "Reset deaths & times",
            "Reset collectibles"
        };

        // unlocker util
        private Unlocker unlocker;

        public InputHandler() {
            unlocker = new Unlocker();
        }

        public void Handle() {
            // print options
            Console.WriteLine("==Ghostrunner Unlocker [by DmgVol]==");
            CheckFileExists();

            Console.WriteLine("Choose one of the options (type in the digit):");
            for(int i = 0; i < options.Length; i++) {
                Console.WriteLine($"{i + 1} - {options[i]}");
            }
            Console.Write("Option: ");

            // read input or args if any
            string input = "";
            if(Program.ARGS != null && Program.ARGS.Length == 1) {
                input += Program.ARGS[0][0];
                Console.WriteLine("Using args: " + input);
            } else {
                input = Console.ReadLine();
            }

            // valid?
            bool successFlag = true;
            if(input.Length == 1 && char.IsDigit(input[0]) && int.Parse(input) > 0 && int.Parse(input) < options.Length + 1) {
                int i = int.Parse(input);
                // unlock by given input
                if(i == 1) { 
                    successFlag = unlocker.UnlockCollcetibles();
                } else if(i == 2) {
                    successFlag = unlocker.UnlockLevels();
                } else if(i == 3) {
                    unlocker.UnlockAll();
                }else if(i == 4) {
                    Console.WriteLine("Up to which level to unlock? (1-16)");
                    input = Console.ReadLine();
                    if(char.IsDigit(input[0]) && int.Parse(input) > 0 && int.Parse(input) < 17) {
                        successFlag = unlocker.UnlockUpToLevel(int.Parse(input));
                    } else {
                        successFlag = false;
                    }
                } else if(i == 5) {
                    successFlag = unlocker.NewGameCollectiables();
                }else if(i == 6) {
                    successFlag = unlocker.ResetLevelDetails();
                } else if(i == 7) {
                    successFlag = unlocker.ResetCollectibles();
                }

            } else {
                Console.WriteLine("Error: invalid input.");
                ExitProgram();
            }

            if(successFlag) 
                Console.WriteLine("Patched successfully!");
            else
                Console.WriteLine("Error: failed to patch!\n-Missing permissions or corrupted .sav file");

            ExitProgram();
        }
        public static void CheckFileExists() {
            if(!Unlocker.FileExists()) {
                Console.WriteLine("Error: Missing .sav file!\n-Save file(Ghostrunner.sav) and this executable must be in the same directory");
                ExitProgram();
            }
        }

        public static void ExitProgram() {
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
