using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class IslandWorldGenerator : ChunkGenerator{
        private readonly DistanceRatioCalculator _distanceRatioCalculator;
        private readonly FastNoiseLite.FastNoiseLite _biomeNoise;
        private readonly FastNoiseLite.FastNoiseLite _itemNoise;

        public IslandWorldGenerator(Map map) : base(map, -0.4f){
            _biomeNoise = new FastNoiseLite.FastNoiseLite(_random.Next());
            _biomeNoise.SetFrequency(0.009f);
            _biomeNoise.SetNoiseType(FastNoiseLite.FastNoiseLite.NoiseType.OpenSimplex2S);
            _itemNoise = new FastNoiseLite.FastNoiseLite(_random.Next());
            _itemNoise.SetNoiseType(FastNoiseLite.FastNoiseLite.NoiseType.OpenSimplex2S);
            _itemNoise.SetFrequency(0.9f);
            _distanceRatioCalculator = new DistanceRatioCalculator(map.BlockCount, map.BlockCount);
        }

        // protected override void GenerateWorld(Position blockPosition){
        //     for (var x = 0; x < Chunk.BlockCount; x++){
        //         for (var y = 0; y < Chunk.BlockCount; y++){
        //             var pos = new Position(x + blockPosition.X, y + blockPosition.Y);
        //             var tmp = _MainNoise.GetNoise(x + blockPosition.X, y + blockPosition.Y) +
        //                       0.75f * _ScondaryNoise.GetNoise((x + blockPosition.X), (y + blockPosition.Y)) +
        //                       0.25 * _noise.GetNoise((x + blockPosition.X), (y + blockPosition.Y));
        //
        //             tmp *= _distanceRatioCalculator.GetValue(new Position((x + blockPosition.X),
        //                 (y + blockPosition.Y)));
        //             if (tmp < -0.4f)
        //                 _chunk[x, y] = GetLandBlock(tmp, pos);
        //             else
        //                 _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
        //         }
        //     }
        // }

        // private Block GetLandBlock(double noiseValue, Position position){
        //     if (noiseValue > -0.45f && _biomeNoise.GetNoise(position.X, position.Y) > 0){
        //         return new Block(BlockType.Sand, BiomeType.Beach);
        //     }
        //
        //     if (noiseValue < -1.0f && _biomeNoise.GetNoise(position.X, position.Y) < 0)
        //         return new Block(BlockType.Stone, BiomeType.Mountains);
        //     return new Block(BlockType.Grass, BiomeType.Grassland);
        // }

        protected override double CalculateBlockValue(Position position){
            var tmp = _MainNoise.GetNoise(position.X, position.Y) +
                      0.75f * _ScondaryNoise.GetNoise(position.X, position.Y) +
                      0.25 * _noise.GetNoise(position.X, position.Y);

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