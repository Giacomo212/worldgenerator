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
        
        private Block PerlinNoseParser(PerlinNoiseGenerator  perlinNoise, int x, int y) {
            var tmp = perlinNoise.getValue(x, y);

            if (tmp <= 0)
                return new Block(BlockType.Grass);
            else if (tmp <= 0.5)
                return new Block(BlockType.Sand);
            else if (tmp <= 1)
                return new Block(BlockType.Water);
            return null;
        }
    }
}