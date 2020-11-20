using FastNoise;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class IslandWorldGenerator : ChunkGenerator{
        private readonly NoiseCalculator _noiseCalculator;
        private readonly DistanceRatioCalculator _distanceRatioCalculator;
        private readonly FastNoiseLite _biomeNoise;

        public IslandWorldGenerator(Map map) : base(map){
            _noiseCalculator = new NoiseCalculator(_map.Seed);
            _biomeNoise = new FastNoiseLite(_random.Next());
            _biomeNoise.SetFrequency(0.009f);
            _biomeNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _distanceRatioCalculator = new DistanceRatioCalculator(map.BlockCount, map.BlockCount);
        }


        protected override Block GenerateBlock(Position blockPosition){
            var tmp = _noiseCalculator.GetValue(blockPosition);
            tmp *= _distanceRatioCalculator.GetValue(blockPosition);
            return tmp < -0.4f
                ? GenerateLand(blockPosition)
                : new Block(BlockType.Water, BiomeType.Ocean);
        }

        private Block GenerateLand(Position blockPosition){
            if (_noiseCalculator.GetValue(blockPosition) > -0.45f &&
                _biomeNoise.GetNoise(blockPosition.X, blockPosition.Y) > 0){
                return new Block(BlockType.Sand, BiomeType.Beach);
            }

            if (_noiseCalculator.GetValue(blockPosition) < -1.0f &&
                _biomeNoise.GetNoise(blockPosition.X, blockPosition.Y) < 0)
                return new Block(BlockType.Stone, BiomeType.Mountains);
            return new Block(BlockType.Grass, BiomeType.Grassland);
        }

        protected override ItemType GenerateItem(Position blockPosition, Block block){
            if (_random.Next(0, 25) > 23 &&
                block.BlockType == BlockType.Grass
            ){
                return ItemType.Tree;
            }

            return ItemType.None;
        }
    }
}