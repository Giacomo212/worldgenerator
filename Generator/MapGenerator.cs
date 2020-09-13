using System;

namespace Generator{
    public class MapGenerator{
        private FastNoise _fastNoise;
        private Map _map;
        public MapGenerator(WorldSize size){
            var random = new Random();
            _fastNoise = new FastNoise(random.Next());
        }
        public MapGenerator(WorldSize size, int seed){
            _fastNoise = new FastNoise(seed);
        }
    }
}