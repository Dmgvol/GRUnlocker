using GRUnlocker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GRUnlocker {
    class Unlocker {

        public const string FILE_NAME = "Ghostrunner.sav";
        public static bool FileExists() => File.Exists(FILE_NAME);

        public bool UnlockCollcetibles() {
            InputHandler.CheckFileExists();
            try {
                var bytesData = File.ReadAllBytes(FILE_NAME);
                // find anchor
                byte[] anchorBytes = Encoding.UTF8.GetBytes("UnlockedList");
                int[] anchorPoints = ByteUtils.Locate(bytesData, anchorBytes);
                if(anchorPoints.Length > 0) {
                    // replace with new data
                    List<byte> lst = bytesData.ToList();
                    lst.RemoveRange(anchorPoints[0], lst.Count - anchorPoints[0]);
                    lst.AddRange(Resources.FullUnlockedList.ToList());
                    // save
                    File.WriteAllBytes(FILE_NAME, lst.ToArray());
                    return true;
                }
            } catch(Exception) {
                return false;
            }
            return false;
        }

        public bool ResetCollectibles() {
            InputHandler.CheckFileExists();
            try {
                var bytesData = File.ReadAllBytes(FILE_NAME);
                // find anchor
                int startAnchor = ByteUtils.Locate(bytesData, Encoding.UTF8.GetBytes("UnlockedList"))[0];
                if(startAnchor == 0) return false;
                // replace with new data
                List<byte> lst = bytesData.ToList();
                lst.RemoveRange(startAnchor, lst.Count - startAnchor);
                lst.AddRange(Resources.EmptyUnlockedList.ToList());
                // save
                File.WriteAllBytes(FILE_NAME, lst.ToArray());

            } catch(Exception) {
                return false;
            }
            return true;
        }

        public bool UnlockLevels() {
            InputHandler.CheckFileExists();
            try {
                var bytesData = File.ReadAllBytes(FILE_NAME);
                // find anchors
                int[] start_anchorPoints = ByteUtils.Locate(bytesData, Encoding.UTF8.GetBytes("LastLevel"));
                int[] end_anchorPoints = ByteUtils.Locate(bytesData, Encoding.UTF8.GetBytes("UnlockedList"));
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
            } catch(Exception) {
                return false;
            }
            return false;
        }

        public bool UnlockAll() {
            InputHandler.CheckFileExists();
            try {
                File.WriteAllBytes(FILE_NAME, Resources.Hundo);
            } catch(Exception) {
                return false;
            }
            return true;
        }

        public bool NewGameCollectiables() {
            InputHandler.CheckFileExists();
            try {
                File.WriteAllBytes(FILE_NAME, Resources.Newgame_collectiables);
            } catch(Exception) {
                return false;
            }
            return true;
        }


        public bool ResetLevelDetails() {
            InputHandler.CheckFileExists();
            try {
                var data = File.ReadAllBytes(FILE_NAME);
                int secondPartAnchor = ByteUtils.Locate(data, Encoding.UTF8.GetBytes("PartLevelList"))[0];
                ByteUtils.ReplaceByteRange(ref data, Resources.defaultLevelDetails, "LeastDeaths", "None");
                ByteUtils.ReplaceByteRange(ref data, Resources.defaultLevelDetails_2, "Deaths", "None", secondPartAnchor);
                File.WriteAllBytes(FILE_NAME, data);
            } catch(Exception) {
                return false;
            }
            return true;
        }


        public bool UnlockUpToLevel(int level) {
            if(level < 1 || level > 16) return false;
            try {
                var data = File.ReadAllBytes(FILE_NAME).ToList();
                var dataFiller = GetResourceLevelData(level);
                int startAnchor = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int endAnchor = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];
                if(startAnchor < 1 || endAnchor < 1) return false;
                // swap bytes
                data.RemoveRange(startAnchor, endAnchor - startAnchor);
                data.InsertRange(startAnchor, dataFiller);

                File.WriteAllBytes(FILE_NAME, data.ToArray());
            } catch(Exception) {
                return false;
            }
            return true;
        }

        private byte[] GetResourceLevelData(int i ) {
            switch(i) {
                case 1:
                    return Resources.Ghostrunner_lvl1;
                case 2:
                    return Resources.Ghostrunner_lvl2;
                case 3:
                    return Resources.Ghostrunner_lvl3;
                case 4:
                    return Resources.Ghostrunner_lvl4;
                case 5:
                    return Resources.Ghostrunner_lvl5;
                case 6:
                    return Resources.Ghostrunner_lvl6;
                case 7:
                    return Resources.Ghostrunner_lvl7;
                case 8:
                    return Resources.Ghostrunner_lvl8;
                case 9:
                    return Resources.Ghostrunner_lvl9;
                case 10:
                    return Resources.Ghostrunner_lvl10;
                case 11:
                    return Resources.Ghostrunner_lvl11;
                case 12:
                    return Resources.Ghostrunner_lvl12;
                case 13:
                    return Resources.Ghostrunner_lvl13;
                case 14:
                    return Resources.Ghostrunner_lvl14;
                case 15:
                    return Resources.Ghostrunner_lvl15;
                case 16:
                    return Resources.Ghostrunner_lvl16;
            }
            return null;
        }

    }
}

