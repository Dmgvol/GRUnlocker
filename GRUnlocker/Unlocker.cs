using GRUnlocker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GRUnlocker {
    public class Unlocker {

        // paths and file names
        public const string GameIntroDirectory = @"Ghostrunner\Content\Movies\";
        public const string IntroFileName1 = "SplashScreen.mp4";
        public const string IntroFileName2 = "GR_TRAILER_Demo.mp4";
        public const string FileToggleTag = "~";

        // Path checkers/getters
        public static bool FileExists() => File.Exists(Config.getInstance().SaveFilePath);

        //public static string GetSavePath() => Path.Combine(Config.getInstance().SaveDirectory, IsSteam ? FILE_NAME_Steam : FILE_NAME_EGS);
        public static string GetSavePath() => Config.getInstance().SaveFilePath;

        public bool UnlockCollectibles() {
            InputHandler.CheckSaveExists();
            try {
                var bytesData = File.ReadAllBytes(GetSavePath());
                // find anchor
                byte[] anchorBytes = Encoding.UTF8.GetBytes("UnlockedList");
                int[] anchorPoints = ByteUtils.Locate(bytesData, anchorBytes);
                if(anchorPoints.Length > 0) {
                    // replace with new data
                    List<byte> lst = bytesData.ToList();
                    lst.RemoveRange(anchorPoints[0], lst.Count - anchorPoints[0]);
                    lst.AddRange(Resources.FullUnlockedList.ToList());
                    // save
                    File.WriteAllBytes(GetSavePath(), lst.ToArray());
                    return true;
                }
            } catch(Exception) {
                return false;
            }
            return false;
        }

        public bool ResetCollectibles() {
            InputHandler.CheckSaveExists();
            try {
                var bytesData = File.ReadAllBytes(GetSavePath());
                // find anchor
                int startAnchor = ByteUtils.Locate(bytesData, Encoding.UTF8.GetBytes("UnlockedList"))[0];
                if(startAnchor == 0) return false;
                // replace with new data
                List<byte> lst = bytesData.ToList();
                lst.RemoveRange(startAnchor, lst.Count - startAnchor);
                lst.AddRange(Resources.EmptyUnlockedList.ToList());
                // save
                File.WriteAllBytes(GetSavePath(), lst.ToArray());

            } catch(Exception) {
                return false;
            }
            return true;
        }

        public bool UnlockLevels() {
            InputHandler.CheckSaveExists();
            try {
                var bytesData = File.ReadAllBytes(GetSavePath());
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
                    File.WriteAllBytes(GetSavePath(), lst.ToArray());
                    return true;
                }
            } catch(Exception) {
                return false;
            }
            return false;
        }

        // TAG:HC (WinterPatch update)
        public bool UnlockLevelsHC() {
            InputHandler.CheckSaveExists();
            try {
                var bytesData = File.ReadAllBytes(GetSavePath()).ToList();
                var bytesPreset = Resources.hundoHC.ToList();
                // find anchors on both target and preset
                int anchorStart_target = ByteUtils.Locate(bytesData.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int anchorEnd_target = ByteUtils.Locate(bytesData.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];
                int anchorStart_preset = ByteUtils.Locate(bytesPreset.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int anchorEnd_preset = ByteUtils.Locate(bytesPreset.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];
                // valid anchors?
                if(anchorStart_target > 0 && anchorEnd_target > anchorStart_target && anchorStart_preset > 0 && anchorEnd_preset > anchorStart_preset) {
                    bytesPreset = bytesPreset.Skip(anchorStart_preset).Take(anchorEnd_preset - anchorStart_preset).ToList();

                    bytesData.RemoveRange(anchorStart_target, anchorEnd_target - anchorStart_target);
                    bytesData.InsertRange(anchorStart_target, bytesPreset);

                    File.WriteAllBytes(GetSavePath(), bytesData.ToArray());
                    return true;
                }
            } catch(Exception) {
                return false;
            }
            return false;
        }

        public bool UnlockLevelsKR() {
            InputHandler.CheckSaveExists();
            try {
                var bytesData = File.ReadAllBytes(GetSavePath()).ToList();
                var bytesPreset = Resources.KRFull.ToList();
                // find anchors on both target and preset
                int anchorStart_target = ByteUtils.Locate(bytesData.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int anchorEnd_target = ByteUtils.Locate(bytesData.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];

                int anchorStart_preset = ByteUtils.Locate(bytesPreset.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int anchorEnd_preset = ByteUtils.Locate(bytesPreset.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];
                // valid anchors?
                if(anchorStart_target > 0 && anchorEnd_target > anchorStart_target && anchorStart_preset > 0 && anchorEnd_preset > anchorStart_preset) {
                    bytesPreset = bytesPreset.Skip(anchorStart_preset).Take(anchorEnd_preset - anchorStart_preset).ToList();

                    bytesData.RemoveRange(anchorStart_target, anchorEnd_target - anchorStart_target);
                    bytesData.InsertRange(anchorStart_target, bytesPreset);

                    File.WriteAllBytes(GetSavePath(), bytesData.ToArray());
                    return true;
                }
            } catch(Exception) {
                return false;
            }
            return false;
        }


        public bool UnlockAll() {
            InputHandler.CheckSaveExists();
            try {
                // replace with 100% save (all collectibles + classic levels)
                File.WriteAllBytes(GetSavePath(), Resources.Hundo);
            } catch(Exception) {
                return false;
            }
            return true;
        }

        // TAG:HC (WinterPatch update)
        public bool UnlockAllHC() {
            InputHandler.CheckSaveExists();
            try {
                // replace with 100% save (all collectibles + classic & HC levels)
                File.WriteAllBytes(GetSavePath(), Resources.hundoHC);
            } catch(Exception) {
                return false;
            }
            return true;
        }

        // KR Update
        public bool UnlockAllKR() {
            InputHandler.CheckSaveExists();
            try {
                // replace with 100% save (all collectibles, classic+HC levels, + 5 Killruns)
                File.WriteAllBytes(GetSavePath(), Resources.KRFull);
            } catch(Exception) {
                return false;
            }
            return true;
        }

        public bool NewGameKR() {
            InputHandler.CheckSaveExists();
            try {
                // replace save with NG save with KR done
                File.WriteAllBytes(GetSavePath(), Resources.NGKR);
            } catch(Exception) {
                return false;
            }
            return true;
        }

        public bool NewGameCollectiables() {
            InputHandler.CheckSaveExists();
            try {
                File.WriteAllBytes(GetSavePath(), Resources.Newgame_collectiables);
            } catch(Exception) {
                return false;
            }
            return true;
        }

        public bool ResetLevelDetails() {
            InputHandler.CheckSaveExists();
            try {
                var data = File.ReadAllBytes(GetSavePath());
                int secondPartAnchor = ByteUtils.Locate(data, Encoding.UTF8.GetBytes("PartLevelList"))[0];
                ByteUtils.ReplaceByteRange(ref data, Resources.defaultLevelDetails, "LeastDeaths", "None");
                ByteUtils.ReplaceByteRange(ref data, Resources.defaultLevelDetails_2, "Deaths", "None", secondPartAnchor);
                File.WriteAllBytes(GetSavePath(), data);
            } catch(Exception) {
                return false;
            }
            return true;
        }

        public bool UnlockUpToLevel(int level) {
            InputHandler.CheckSaveExists();
            if(level < 1 || level > 16) return false;
            try {
                var data = File.ReadAllBytes(GetSavePath()).ToList();
                var dataFiller = GetResourceLevelData(level);
                int startAnchor = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int endAnchor = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];
                int presentLevels = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("ELevelId")).Length / 2;
                Console.WriteLine(level < presentLevels ? $"-> Removed {presentLevels - level} level(s)" : $"-> Unlocked {level- presentLevels} new level(s)" );

                if(startAnchor < 1 || endAnchor < 1) return false;
                // swap bytes
                data.RemoveRange(startAnchor, endAnchor - startAnchor);
                data.InsertRange(startAnchor, dataFiller);

                File.WriteAllBytes(GetSavePath(), data.ToArray());
                return true;
            } catch(Exception) {
                return false;
            }
        }

        // TAG:HC (WinterPatch update)
        public bool UnlockUpToLevelHC(int level) {
            InputHandler.CheckSaveExists();
            if(level < 1 || level > 16) return false;
            try {
                // load data
                var data = File.ReadAllBytes(GetSavePath()).ToList();
                var dataFiller = GetResourceLevelDataHC(level).ToList();
                // find start/end index for both data sets
                int startAnchorData = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int endAnchorData = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];

                int startAnchorFiller = ByteUtils.Locate(dataFiller.ToArray(), Encoding.UTF8.GetBytes("LastLevel"))[0];
                int endAnchorFiller = ByteUtils.Locate(dataFiller.ToArray(), Encoding.UTF8.GetBytes("UnlockedList"))[0];
                // valid anchors?
                if(startAnchorData > 0 && endAnchorData > startAnchorData && startAnchorFiller > 0 && endAnchorFiller > startAnchorFiller) {
                    // get needed data and replace
                    dataFiller = dataFiller.Skip(startAnchorFiller).Take(endAnchorFiller - startAnchorFiller).ToList();
                    data.RemoveRange(startAnchorData, endAnchorData - startAnchorData);
                    data.InsertRange(startAnchorData, dataFiller);
                    // save
                    File.WriteAllBytes(GetSavePath(), data.ToArray());
                    return true;
                }
            } catch(Exception) {
                return false;
            }
            return false;
        }

        public bool Game_ToggleIntros() {
            Console.WriteLine("File dialog will open, select Ghostrunner.exe to continue (press any key to begin)");
            Console.ReadLine();
            Thread t = new Thread((ThreadStart)(() => {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Title = "Select Ghostrunner.exe";
                openFile.DefaultExt = "Ghostrunner.exe";
                openFile.Filter = "GR executable (*.exe)|*.exe";
                openFile.CheckFileExists = true;
                openFile.CheckPathExists = true;
                openFile.Multiselect = false;

                if(openFile.ShowDialog() == DialogResult.OK) {
                    selectedPath = openFile.FileName;
                }
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            if(!string.IsNullOrWhiteSpace(selectedPath) && Directory.Exists(Path.GetDirectoryName(selectedPath))) {
                return ToggleFile(Path.Combine(Path.GetDirectoryName(selectedPath), GameIntroDirectory, IntroFileName1)) &&
                    ToggleFile(Path.Combine(Path.GetDirectoryName(selectedPath), GameIntroDirectory, IntroFileName2));
            }
            return false;
        }

        string selectedPath = "";


        public bool ReplaceSelectedSword(int i) {
            InputHandler.CheckSaveExists();
            try {
                if(i < 0 || i > 14) return false;
                var data = File.ReadAllBytes(GetSavePath());
                // find anchors in save
                int anchor = ByteUtils.Locate(data, Encoding.UTF8.GetBytes("Swords"))[0] + 6;
                int firstAnchorNL = ByteUtils.GetPrevNewLineAnchor(data, anchor) + 1;
                int anchorNL = ByteUtils.Locate(data, Encoding.UTF8.GetBytes("UnlockedList"), anchor)[0] -3;
                if(anchor > 0 && firstAnchorNL > 0 && anchorNL > 0 && anchorNL > anchor && anchor > firstAnchorNL) {
                    // swap with new data
                    List<byte> dataLst = data.ToList();
                    dataLst.RemoveRange(firstAnchorNL, anchorNL - firstAnchorNL);
                    dataLst.InsertRange(firstAnchorNL, SwordData.GetSwordData(i));
                    // save
                    File.WriteAllBytes(GetSavePath(), dataLst.ToArray());
                    return true;
                }

            } catch(Exception) { return false; }
            return false;
        }

        public bool NewGameCustomSword(int i) {
            InputHandler.CheckSaveExists();
            try {
                // replace with fresh new game save
                File.WriteAllBytes(GetSavePath(), Resources.newgame);
                // update with custom sword
                return ReplaceSelectedSword(i);
            } catch(Exception) {return false;}
        }

        private bool ToggleFile(string path) {
            if(File.Exists(path)) {
                // is on, toggle to off
                File.Move(path, Path.Combine(Path.GetDirectoryName(path), (FileToggleTag + Path.GetFileName(path))));
                Console.WriteLine($"Toggled OFF: {Path.GetFileName(path)}");
                return true;
            }else if(File.Exists(Path.Combine(Path.GetDirectoryName(path), (FileToggleTag + Path.GetFileName(path))))){
                // is off, toggle to on
                File.Move(Path.Combine(Path.GetDirectoryName(path), (FileToggleTag + Path.GetFileName(path))), Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(path)));
                Console.WriteLine($"Toggled ON: {Path.GetFileName(path)}");
                return true;
            } else {
                Console.WriteLine($"Failed to locate {Path.GetFileName(path)} (meaning it's already disabled)");
            }
            return false;
        }

        private byte[] GetResourceLevelData(int i) {
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

        // TAG:HC (WinterPatch update)
        private byte[] GetResourceLevelDataHC(int i) {
            switch(i) {
                case 1:
                    return Resources.Ghostrunner_hc_1;
                case 2:
                    return Resources.Ghostrunner_hc_2;
                case 3:
                    return Resources.Ghostrunner_hc_3;
                case 4:
                    return Resources.Ghostrunner_hc_4;
                case 5:
                    return Resources.Ghostrunner_hc_5;
                case 6:
                    return Resources.Ghostrunner_hc_6;
                case 7:
                    return Resources.Ghostrunner_hc_7;
                case 8:
                    return Resources.Ghostrunner_hc_8;
                case 9:
                    return Resources.Ghostrunner_hc_9;
                case 10:
                    return Resources.Ghostrunner_hc_10;
                case 11:
                    return Resources.Ghostrunner_hc_11;
                case 12:
                    return Resources.Ghostrunner_hc_12;
                case 13:
                    return Resources.Ghostrunner_hc_13;
                case 14:
                    return Resources.Ghostrunner_hc_14;
                case 15:
                    return Resources.Ghostrunner_hc_15;
                case 16:
                    return Resources.Ghostrunner_hc_16;
            }
            return null;
        }

    }
}