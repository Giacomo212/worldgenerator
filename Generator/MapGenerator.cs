using System;

namespace Generator{
    public class MapGenerator{
        private FastNoise _blockNoise;
        private FastNoise _biomeNoise;
        private Random _random = new Random();
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
            _biomeNoise.SetNoiseType(FastNoise.NoiseType.Simplex);
            _blockNoise.SetFrequency(0.1f);
            _biomeNoise.SetFrequency(0.04f);
        }
        private Block PerlinNoseParser(int x, int y) {
            
        
           
            return null;
        }

        private BiomeType GetBiome(int x, int y){
            var noiseValue = _biomeNoise.GetValue(x, y);
            if (noiseValue < -0.6f)
                return BiomeType.Mountains;
            if (noiseValue < -0.2f)
                return BiomeType.Forest;
            if (noiseValue < 0.2f)
                return BiomeType.Grassland;
            if (noiseValue < 0.4f)
                return BiomeType.Beach;
            return BiomeType.Ocean;
        }

        public Block GetBlock(int x, int y){
            var biome = GetBiome(x, y);
            switch (biome){
                case BiomeType.Forest: return GenerateForest();
                case BiomeType.Grassland: return GenerateGrassland(); 
                case BiomeType.Ocean : return new Block(BlockType.Water,BiomeType.Ocean); 
                case BiomeType.Mountains: return GenerateMountains(); 
                case BiomeType.Taiga: return new Block(BlockType.Snow,BiomeType.Taiga); 
                case BiomeType.Beach: return new Block(BlockType.Sand,BiomeType.Beach);
            }

            return null;
        }

        private Block GenerateForest(){
            //return _random.Next() % 4 == 0
            return new Block(BlockType.Grass, ItemType.Tree, BiomeType.Forest);
            //: new Block(BlockType.Grass, BiomeType.Forest);
        }

        private Block GenerateMountains(){
            return new Block(BlockType.Stone, BiomeType.Mountains);
        }

        private Block GenerateGrassland(){
            return _random.Next() % 100 == 0
                ? new Block(BlockType.Grass, ItemType.Tree, BiomeType.Grassland)
                : new Block(BlockType.Grass, BiomeType.Grassland);
        }
        
    }
    
}