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
            if(File.Exists(FILE_NAME)) {
                if(Patch()) {
                    Console.WriteLine("Patched successfully!");
                } else {
                    Console.WriteLine("Error: Failed to patch .sav file");
                }
            } else {
                Console.WriteLine("Error: Missing .sav file");
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
        }

        static bool Patch() {
            var bytesData = File.ReadAllBytes(FILE_NAME);

            // find fist anchor
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

