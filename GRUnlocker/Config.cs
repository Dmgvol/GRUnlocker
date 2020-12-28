using System;
using System.IO;
using System.Linq;

namespace GRUnlocker {
    class Config {

        // Global settings
        private string SaveDirectory;
        public string GameDirectory { get; private set; } = "";

        // save file
        public bool DisplayPath { get; private set; } = false;
        public enum SaveType { None, SteamOrGOG, EGS }
        public enum SaveLocation { None, Local, Remote, Auto }

        public SaveType saveType = SaveType.None;
        public SaveLocation saveLocation = SaveLocation.None;
        public string SaveFilePath { get; private set; } = "";


        // Private settings
        private const string FILE_NAME_CONFIG = "config.json";
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

            // config file found?
            if(File.Exists(FILE_NAME_CONFIG)) {
                if(!ParseManually()) {
                    Console.WriteLine("Error: config.json file\n -Invalid/missing directories or corrupted config.json file");
                    InputHandler.ExitProgram();
                }
            } else {
                // no config file, local path file
                SaveDirectory = Directory.GetCurrentDirectory();
                if(DetectFile()) { // file found in same directory, flag as local
                    saveLocation = SaveLocation.Local;
                }
            }


            // failed to find local + remote(config file), try auto detect
            if(saveLocation == SaveLocation.None)
                AutoDetect();
        }

        // Note:    creating and parsing manually to avoid 3'rd party .dll dependencies like Newtonsoft-Json,
        //          so the user can drag a single exe without copying bunch of dlls with it,
        //          just like the previous versions if they don't like the config file (just cleaner)
        public bool SaveManually() {
            try {
                string str = "{\n" +
                    $"  \"{nameof(SaveDirectory)}\": \"\",\n" +
                    $"  \"{nameof(GameDirectory)}\": \"\"\n" +
                    "}";
                File.WriteAllText(FILE_NAME_CONFIG, str);
                return true;
            } catch(Exception) {}
            return false;
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

        private bool ParseManually() {
            if(!File.Exists(FILE_NAME_CONFIG)) return false;
            var result = File.ReadAllText(FILE_NAME_CONFIG)
                    .Split(new[] { '\n', '\r', ',', '\"' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();

            result = result.Where(x => x != ": ").ToArray();

            if(result.Length == 6) {
                if(result[0] == "{" && result[result.Length - 1] == "}") {
                    result = result.Where(x => x != "{" && x != "}").ToArray();
                    if(result[0] == nameof(SaveDirectory) && result[2] == nameof(GameDirectory)) {
                        // check save directory
                        if(Directory.Exists(result[1])) {
                            SaveDirectory = result[1];
                            saveLocation = SaveLocation.Remote;
                            DetectFile();
                        }
                        // check game directory
                        if(Directory.Exists(result[3]) && GameDirectoryStructureValid(result[3])) {
                            GameDirectory = result[3];
                        }
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

        private bool GameDirectoryStructureValid(string path) {
            // 2 directories "Engine" and "Ghostrunner", 1 file "Ghostrunner.exe"
            return (Directory.Exists(Path.Combine(path, "Engine")) && Directory.Exists(Path.Combine(path, "Ghostrunner")) && File.Exists(Path.Combine(path, "Ghostrunner.exe")));
        }
    }
}
