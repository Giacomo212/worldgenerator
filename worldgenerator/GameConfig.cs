using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace worldgenerator {
    public class GameConfig {
        //sigleton
        public static GameConfig config = new GameConfig();
        public float Sensivity { get; set; } //= 0.3f;
        public Resolution Resolution { get; set; } //= new Resolution();


        GameConfig() {

        }
        public void Save() {
            using (var file = new StreamWriter("./config")) {
                //var properties = Utility.GetTypeProperties(config);
                //string tmp;
                //foreach  (var t in properties) {
                //    string name = t.Name;
                //    tmp = JsonConvert.SerializeObject(t.GetValue(config));

                //    file.Write($"{name} = {tmp} \n");
                //}
                var tmp = JsonConvert.SerializeObject(this);
                file.Write(tmp);
            }

        }
        public void Load() {
            try {
                using (var file = new StreamReader("./config")) {
                    //var properties = Utility.GetTypeProperties(config);
                    //string tmp;
                    //foreach  (var t in properties) {
                    //    string name = t.Name;
                    //    tmp = JsonConvert.SerializeObject(t.GetValue(config));

                    //    file.Write($"{name} = {tmp} \n");
                    //}
                    var tmp = file.ReadLine();
                    config = JsonConvert.DeserializeObject<GameConfig>(tmp);

                }
            } catch {
                Sensivity = 0.3f;
                Resolution = new Resolution(1280, 720, false);
                Save();
            }
        }
        public void Set() {
            Sensivity = 0.3f;
            Resolution = new Resolution(1280, 720, false);
        }
    }
}
