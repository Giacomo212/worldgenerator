using System;
using Types;

namespace Generator{
    public class SurfaceChunkGenerator : IChunkGenerator{
        private FastNoise _blockNoise;
        private FastNoise _biomeNoise;
        private Random _random = new Random();

        public SurfaceChunkGenerator(){
            var random = new Random();
            SetupNoise(random.Next());
        }

        public SurfaceChunkGenerator(int seed){
            SetupNoise(seed);
        }

        private void SetupNoise(int seed){
            _blockNoise = new FastNoise(seed);
            _biomeNoise = new FastNoise(seed + 2137);
            _blockNoise.SetNoiseType(FastNoise.NoiseType.Perlin);
            _biomeNoise.SetNoiseType(FastNoise.NoiseType.Perlin);
            _blockNoise.SetFrequency(0.2f);
            _biomeNoise.SetFrequency(0.04f);
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

        private Block GetBlock(int x, int y){
            var biome = GetBiome(x, y);
            switch (biome){
                case BiomeType.Forest: return GenerateForest(x, y);
                case BiomeType.Grassland: return GenerateGrassland();
                case BiomeType.Ocean: return new Block(BlockType.Water, BiomeType.Ocean);
                case BiomeType.Mountains: return GenerateMountains();
                case BiomeType.Taiga: return new Block(BlockType.Snow, BiomeType.Taiga);
                case BiomeType.Beach: return new Block(BlockType.Sand, BiomeType.Beach);
            }

            return null;
        }

        private Block GenerateForest(int x, int y){
            var random = new Random();
            var noise = random.Next() % 12;
            if (noise < 3)
                return new Block(BlockType.Grass, BiomeType.Forest, ItemType.Tree);
            if (noise < 6)
                return new Block(BlockType.Grass, BiomeType.Forest);
            return new Block(BlockType.Grass, BiomeType.Forest, ItemType.Tree);
        }

        private Block GenerateMountains(){
            return new Block(BlockType.Stone, BiomeType.Mountains);
        }

        private Block GenerateGrassland(){
            return _random.Next() % 100 == 0
                ? new Block(BlockType.Grass, BiomeType.Grassland, ItemType.Tree)
                : new Block(BlockType.Grass, BiomeType.Grassland);
        }

        public Chunk GenerateChunk(){
            throw new NotImplementedException();
        }
    }
}