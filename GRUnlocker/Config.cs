using System;
using System.IO;
using System.Linq;

namespace GRUnlocker {
    class Config {

        // Global settings
        public string SaveDirectory { get; private set; } = "";
        public string GameDirectory { get; private set; } = "";

        public bool IsLocalSavePath = true;
        public bool ValidGameDirectory = false;

        // Private settings
        private const string ConfigFileName = "config.json";

        // single instance
        private static Config Instance;
        public static Config getInstance() {
            if(Instance == null)
                Instance = new Config();
            return Instance;
        }

        // Load config file
        public void Load() {
            if(File.Exists(ConfigFileName)) {
                if(!ParseManually()) {
                    Console.WriteLine("Error: config.json file\n -Invalid/missing directories or corrupted config.json file");
                    InputHandler.ExitProgram();
                }
            } else {
                // no config file, local path file
                SaveDirectory = "";
                GameDirectory = "";
                IsLocalSavePath = true;
            }
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
                File.WriteAllText(ConfigFileName, str);
                return true;
            } catch(Exception) {}
            return false;
        }

        private bool ParseManually() {
            if(!File.Exists(ConfigFileName)) return false;
            var result = File.ReadAllText(ConfigFileName)
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
                            IsLocalSavePath = false;
                        }
                        // check game directory
                        if(Directory.Exists(result[3]) && GameDirectoryStructureValid(result[3])) {
                            GameDirectory = result[3];
                            ValidGameDirectory = true;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private bool GameDirectoryStructureValid(string path) {
            // 2 directories "Engine" and "Ghostrunner", 1 file "Ghostrunner.exe"
            return (Directory.Exists(Path.Combine(path, "Engine")) && Directory.Exists(Path.Combine(path, "Ghostrunner")) && File.Exists(Path.Combine(path, "Ghostrunner.exe")));
        }
    }
}
