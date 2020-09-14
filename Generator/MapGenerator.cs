using System;

namespace Generator{
    public class MapGenerator{
        private FastNoise _blockNoise;
        private FastNoise _biomeNoise;
        public MapGenerator(){
            var random = new Random();
            SetupNoise(random.Next());
        }
        public MapGenerator(int seed){
            SetupNoise(seed);
        }

        private void SetupNoise(int seed){
            _blockNoise = new FastNoise(seed);
            _biomeNoise = new FastNoise(seed + 433916);
            _blockNoise.SetNoiseType(FastNoise.NoiseType.Perlin);
            _biomeNoise.SetNoiseType(FastNoise.NoiseType.Perlin);
            _blockNoise.SetFrequency(0.1f);
            _biomeNoise.SetFrequency(0.025f);
        }
        private Block PerlinNoseParser(int x, int y) {
            
        
           
            return null;
        }

        private BiomeType GetBiome(int x, int y){
            var noiseValue = _biomeNoise.GetValue(x, y);
            if (noiseValue < -0.66f)
                return BiomeType.Mountains;
            if (noiseValue < -0.33f)
                return BiomeType.Forest;
            if (noiseValue < 0)
                return BiomeType.Grassland;
            if (noiseValue < 0.33)
                
            return BiomeType.Ocean;
        }

        public Block GetBlock(int x, int y){
            var biome = GetBiome(x, y);
            switch (biome){
                case BiomeType.Forest: return new Block(BlockType.Grass,ItemType.Tree,BiomeType.Forest); break;
                case BiomeType.Grassland: return new Block(BlockType.Grass,BiomeType.Grassland); break;
                case BiomeType.Ocean : return new Block(BlockType.Water,BiomeType.Ocean); break;
                case BiomeType.Mountains: return new Block(BlockType.Stone,BiomeType.Mountains); break;
                case BiomeType.Taiga: return new Block(BlockType.Snow,BiomeType.Taiga); break;
            }

            return null;
        }

        private Block GenerateForest(){
            return null;
        }

        private Block GenerateMountains(){
            return null;
        }
        private Block Geme
        
    }
    
}