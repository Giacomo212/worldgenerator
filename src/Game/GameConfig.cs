﻿using System;
using System.IO;
using Newtonsoft.Json;
using Types;

namespace Game {
    public class GameConfig{
        
        //sigleton
        public static GameConfig Config = new GameConfig();
        public float Sensivity { get; set; } = 4.0f;
        public Resolution Resolution { get; set; } 
        

        private GameConfig() {
            Resolution = new Resolution(1280, 720, false);
            
            
        }
        public void Save(){
            var separator = Path.DirectorySeparatorChar;
            var settingsToSave = "";
            settingsToSave += JsonConvert.SerializeObject(Config);
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