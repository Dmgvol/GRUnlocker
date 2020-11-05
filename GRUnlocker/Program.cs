using GRUnlocker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GRUnlocker {
    class Program {
        const string FILE_NAME = "Ghostrunner.sav";
        static void Main(string[] args) {

            Console.WriteLine("==Ghostrunner Unlocker [by DmgVol]==");
            Console.WriteLine("Choose one of the options (type in the digit): \n" +
                "1 - Unlock all collectibles\n" +
                "2 - Unlock all levels\n" +
                "3 - Unlock everything\n" +
                "4 - New game with collectibles unlocked\n\n");
            Console.Write("Option: ");

            string input = "";
            // read input or args if any
            if(args != null && args.Length == 1) {
                input += args[0][0];
                Console.WriteLine("Using args: " + input);
            } else {
                input = Console.ReadLine();
            }

            // valid?
            bool successFlag = true;
            if(input.Length == 1 && char.IsDigit(input[0])) {
                int i = int.Parse(input);
                // unlock by given input
                if(i == 1) {
                    successFlag = UnlockCollcetibles();
                }else if(i == 2) {
                    successFlag = UnlockLevels();
                } else if(i == 3) {
                    UnlockAll();
                } else if(i == 4) {
                    NewGameCollectiables();
                }
                
            } else {
                Console.WriteLine("Error: invalid input.");
            }

            if(successFlag) Console.WriteLine("Patched successfully!");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
        }

        static bool FileExists() => File.Exists(FILE_NAME);

        static bool UnlockCollcetibles() {
            if(!FileExists()) {
                Console.WriteLine("Error: Missing .sav file!");
                return false;
            }

            var bytesData = File.ReadAllBytes(FILE_NAME);
            // find anchor
            byte[] anchorBytes = Encoding.UTF8.GetBytes("UnlockedList");
            int[] anchorPoints = Locate(bytesData, anchorBytes);
            if(anchorPoints.Length > 0) {
                // replace with new data
                List<byte> lst = bytesData.ToList();
                lst.RemoveRange(anchorPoints[0], lst.Count - anchorPoints[0]);
                lst.AddRange(Resources.patch.ToList());
                // save
                File.WriteAllBytes(FILE_NAME, lst.ToArray());
                return true;
            }
            return false;
        }

        static bool UnlockLevels() {
            if(!FileExists()) {
                Console.WriteLine("Error: Missing .sav file!");
                return false;
            }

            var bytesData = File.ReadAllBytes(FILE_NAME);
            // find anchors
            int[] start_anchorPoints = Locate(bytesData, Encoding.UTF8.GetBytes("LastLevel"));
            int[] end_anchorPoints = Locate(bytesData, Encoding.UTF8.GetBytes("UnlockedList"));
            if(start_anchorPoints.Length > 0 && end_anchorPoints.Length > 0 && end_anchorPoints[0] > start_anchorPoints[0]) {
                List<byte> lst = bytesData.ToList();
                start_anchorPoints[0] -= 3; //offset 
                end_anchorPoints[0] -= 3;
                // replace
                lst.RemoveRange(start_anchorPoints[0], end_anchorPoints[0] - start_anchorPoints[0]);
                lst.InsertRange(start_anchorPoints[0], Resources.Levels);
                // save
                File.WriteAllBytes(FILE_NAME, lst.ToArray());
                return true;
            }
            return false;
        }

        static void UnlockAll() {
            File.WriteAllBytes(FILE_NAME, Resources.Hundo);
        }

        static void NewGameCollectiables() {
            File.WriteAllBytes(FILE_NAME, Resources.Newgame_collectiables);
        }

        ////// Byte Utils//////
        static readonly int[] Empty = new int[0];

        static int[] Locate(byte[] self, byte[] candidate) {
            if(IsEmptyLocate(self, candidate)) return Empty;

            var list = new List<int>();

            for(int i = 0; i < self.Length; i++) {
                if(!IsMatch(self, i, candidate))
                    continue;

                list.Add(i);
            }
            return list.Count == 0 ? Empty : list.ToArray();
        }

        static bool IsMatch(byte[] array, int position, byte[] candidate) {
            if(candidate.Length > (array.Length - position)) return false;

            for(int i = 0; i < candidate.Length; i++)
                if(array[position + i] != candidate[i])
                    return false;

            return true;
        }

        static bool IsEmptyLocate(byte[] array, byte[] candidate) {
            return array == null
                || candidate == null
                || array.Length == 0
                || candidate.Length == 0
                || candidate.Length > array.Length;
        }
    }
}

