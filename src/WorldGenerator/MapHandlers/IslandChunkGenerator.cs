using FastNoise;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class IslandWorldGenerator : ChunkGenerator{
        private readonly DistanceRatioCalculator _distanceRatioCalculator;
        private readonly FastNoiseLite _biomeNoise;
        private readonly FastNoiseLite _itemNoise;

        public IslandWorldGenerator(Map map) : base(map, -0.4f){
            _biomeNoise = new FastNoiseLite(_random.Next());
            _biomeNoise.SetFrequency(0.009f);
            _biomeNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _itemNoise = new FastNoiseLite(_random.Next());
            _itemNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _itemNoise.SetFrequency(0.9f);
            _distanceRatioCalculator = new DistanceRatioCalculator(map.BlockCount, map.BlockCount);
        }
        

        protected override double CalculateBlockValue(Position position){
            var tmp = _blockNoiseCalculator.GetValue(position);

            tmp *= _distanceRatioCalculator.GetValue(position);
            return tmp;
        }

        protected override Block GetLandBlock(double noiseValue, Position position){
            if (noiseValue > -0.45f && _biomeNoise.GetNoise(position.X, position.Y) > 0){
                return new Block(BlockType.Sand, BiomeType.Beach);
            }

            if (noiseValue < -1.0f && _biomeNoise.GetNoise(position.X, position.Y) < 0)
                return new Block(BlockType.Stone, BiomeType.Mountains);
            return new Block(BlockType.Grass, BiomeType.Grassland);
        }

        protected override void GenerateItems(Position blockPosition){
            for (int i = 0; i < Chunk.BlockCount; i++){
                for (int j = 0; j < Chunk.BlockCount; j++){
                    if (_random.Next(0, 25) > 23 &&
                        _chunk[i, j].BlockType == BlockType.Grass
                    ){
                        _chunk[i, j].ItemType = ItemType.Tree;
                    }
                }
            }
        }
    }
}