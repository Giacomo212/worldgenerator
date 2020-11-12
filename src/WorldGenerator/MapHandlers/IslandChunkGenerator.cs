using FastNoise;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class IslandWorldGenerator : ChunkGenerator{
        private readonly BlockNoiseCalculator _blockNoiseCalculator;
        private readonly DistanceRatioCalculator _distanceRatioCalculator;
        private readonly FastNoiseLite _biomeNoise;

        public IslandWorldGenerator(Map map) : base(map){
            _blockNoiseCalculator = new BlockNoiseCalculator(_map.Seed);
            _biomeNoise = new FastNoiseLite(_random.Next());
            _biomeNoise.SetFrequency(0.009f);
            _biomeNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _distanceRatioCalculator = new DistanceRatioCalculator(map.BlockCount, map.BlockCount);
        }


        protected override Block CalculateBlock(Position blockPosition){
            var tmp = _blockNoiseCalculator.GetValue(blockPosition);

            tmp *= _distanceRatioCalculator.GetValue(blockPosition);
            if (tmp < -0.4f)
                return GenerateLand(blockPosition);
            return new Block(BlockType.Water, BiomeType.Ocean);
        }

        private Block GenerateLand(Position blockPosition){
            if (_blockNoiseCalculator.GetValue(blockPosition) > -0.45f &&
                _biomeNoise.GetNoise(blockPosition.X, blockPosition.Y) > 0){
                return new Block(BlockType.Sand, BiomeType.Beach);
            }

            if (_blockNoiseCalculator.GetValue(blockPosition) < -1.0f &&
                _biomeNoise.GetNoise(blockPosition.X, blockPosition.Y) < 0)
                return new Block(BlockType.Stone, BiomeType.Mountains);
            return new Block(BlockType.Grass, BiomeType.Grassland);
        }

        protected override ItemType CalculateItem(Position blockPosition, Block block){
            if (_random.Next(0, 25) > 23 &&
                block.BlockType == BlockType.Grass
            ){
                return ItemType.Tree;
            }

            return ItemType.None;
        }
    }
}