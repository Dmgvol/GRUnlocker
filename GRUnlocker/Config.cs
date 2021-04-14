using System;
using System.IO;
using System.Linq;

namespace GRUnlocker {
    class Config {

        // Global settings
        private string SaveDirectory;

        // save file
        public bool DisplayPath { get; private set; } = false;
        public enum SaveType { None, SteamOrGOG, EGS }
        public enum SaveLocation { None, Local, Remote, Auto }

        public SaveType saveType = SaveType.None;
        public SaveLocation saveLocation = SaveLocation.None;
        public string SaveFilePath { get; private set; } = "";


        // Private settings
        private const string FILE_NAME_Steam = "Ghostrunner.sav";
        private const string FILE_NAME_EGS = "GhostrunnerSave.sav";

        // single instance
        private static Config Instance;
        public static Config getInstance() {
            if(Instance == null)
                Instance = new Config();
            return Instance;
        }

        // Load config file
        public void Load(string[] args) {
            // ARGS
            if(args.Contains("-displaypath")) 
                DisplayPath = true;

            //local path file
                SaveDirectory = Directory.GetCurrentDirectory();
                if(DetectFile()) { // file found in same directory, flag as local
                    saveLocation = SaveLocation.Local;
                }
          

            // failed to find local + remote(config file), try auto detect
            if(saveLocation == SaveLocation.None)
                AutoDetect();
        }

        private bool AutoDetect() {
            // attempts to auto find .sav file
            string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Ghostrunner\Saved\SaveGames\");
            if(!Directory.Exists(savePath)) return false;


            // EGS VERSION
            if(File.Exists(Path.Combine(savePath, FILE_NAME_EGS))) {
                // found it - EGS
                SaveFilePath = Path.Combine(savePath, FILE_NAME_EGS);
                saveType = SaveType.EGS;
                saveLocation = SaveLocation.Auto;
                return true;
            }

            // STEAM VERSION
            var folders = Directory.EnumerateDirectories(savePath).ToList();
            for(int i = 0; i < folders.Count(); i++) {
                long steamUserID = -1;
                if(Path.GetFileName(folders[i]).Length > 15 && Path.GetFileName(folders[i]).Length < 20 && long.TryParse(Path.GetFileName(folders[i]), out steamUserID) && steamUserID > 0) {
                    savePath = Path.Combine(savePath, "" + steamUserID,  FILE_NAME_Steam);
                    if(File.Exists(savePath)) {
                        // found it - STEAM/GOG
                        SaveFilePath = savePath;
                        saveType = SaveType.SteamOrGOG;
                        saveLocation = SaveLocation.Auto;
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DetectFile() {
            if(File.Exists(Path.Combine(SaveDirectory, FILE_NAME_Steam))) {
                SaveFilePath = Path.Combine(SaveDirectory, FILE_NAME_Steam);
                saveType = SaveType.SteamOrGOG;
                return true;
            } else if(File.Exists(Path.Combine(SaveDirectory, FILE_NAME_EGS))) {
                SaveFilePath = Path.Combine(SaveDirectory, FILE_NAME_EGS);
                saveType = SaveType.SteamOrGOG;
                return true;
            }
            return false;
        }
    }
}
