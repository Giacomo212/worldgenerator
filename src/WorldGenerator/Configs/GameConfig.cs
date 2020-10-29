using System.IO;
using System.Text.Json;
using WorldGenerator.Utils;

namespace WorldGenerator.Configs{
    public class GameConfig{
        //sigleton
        
        
        public static GameConfig Config;
        public float Sensivity{ get; set; }
        public Resolution Resolution{ get; set; }
        public KeyboardMap KeyboardMap{ get; set; }

       

        private static void SetupDefaultValues(){
            Config = new GameConfig();
            Config.Sensivity = 4.0f;
            Config.Resolution = new Resolution(1280, 720, false);
            Config.KeyboardMap = new KeyboardMap();
        }

        public static void Save(){
            var separator = Path.DirectorySeparatorChar;
            var settingsToSave = "";
            settingsToSave += JsonSerializer.Serialize(Config);
            try{
                using var file = new StreamWriter(EnvironmentVariables.GameFiles + $"{separator}settings.json");
                file.Write(settingsToSave);
            }
            catch{
            }
        }

        public static void Load(){
            try{
                var config = System.IO.File.ReadAllText(EnvironmentVariables.GameFiles +
                                                        $"{EnvironmentVariables.Separator}settings.json");
                Config = JsonSerializer.Deserialize<GameConfig>(config);
            }
            catch{
                SetupDefaultValues();
            }
        }
    }
}