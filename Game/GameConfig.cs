using System;
using System.IO;
using Libraries;
using Newtonsoft.Json;

namespace Game {
    public class GameConfig{
        
        //sigleton
        public static GameConfig Config = new GameConfig();
        public float Sensivity { get; set; } = 2.0f;
        public Resolution Resolution { get; set; } 
        //public readonly string  GameFilesPath;

        private GameConfig() {
            Resolution = new Resolution(1280, 720, false);
            
            
        }
        public void Save(){
            var separator = Path.DirectorySeparatorChar;
            Directory.CreateDirectory(EnvironmentVariables.GameFiles);
            var settingsToSave = "";
            settingsToSave += JsonConvert.SerializeObject(Config.Sensivity);
            settingsToSave += "\n";
            settingsToSave += JsonConvert.SerializeObject(Config.Resolution);
            settingsToSave += "\n";
            try{
                using var file = new StreamWriter(EnvironmentVariables.GameFiles + $"{separator}settings.json");
                file.Write(settingsToSave);
            }
            catch {
                
            }
        }
        // public void Load() {
        //     try {
        //         using (var file = new StreamReader("./config")) {
        //             //var properties = Utility.GetTypeProperties(config);
        //             //string tmp;
        //             //foreach  (var t in properties) {
        //             //    string name = t.Name;
        //             //    tmp = JsonConvert.SerializeObject(t.GetValue(config));
        //
        //             //    file.Write($"{name} = {tmp} \n");
        //             //}
        //             var tmp = file.ReadLine();
        //             config = JsonConvert.DeserializeObject<GameConfig>(tmp);
        //
        //         }
        //     } catch {
        //        
        //         Resolution = new Resolution(1280, 720, false);
        //         Save();
        //     }
        // }
        // public void Set() {
        //     
        //     Resolution = new Resolution(1280, 720, false);
        // }
    }
}
