using WorldGenerator.Utils;
using WorldGenerator.WorldMap;

namespace WorldGenerator.MapHandler{
    public class ContinentChunkGenerator : ChunkGenerator{
        private readonly DistanceRatioCalculator _firstDistanceRatioCalculator; //= new DistanceRatioCalculator(400, 200);
        //private readonly DistanceRatioCalculator _secondDistanceRatioCalculator = new DistanceRatioCalculator(400, 200);
        //private readonly DistanceRatioCalculator _thirdDistanceRatioCalculator = new DistanceRatioCalculator(400, 200);
        private readonly FastNoiseLite.FastNoiseLite _biomeNoise;

        public ContinentChunkGenerator(Map map) : base(map){
            _firstDistanceRatioCalculator = new DistanceRatioCalculator(new Position(800,800), new Position(400,400));
            _firstDistanceRatioCalculator.CalculationRatio = 4.0;
            _firstDistanceRatioCalculator.CalculationRatio = 0.5;
            _biomeNoise = new FastNoiseLite.FastNoiseLite(_random.Next());
            _biomeNoise.SetFrequency(0.009f);
            _biomeNoise.SetNoiseType(FastNoiseLite.FastNoiseLite.NoiseType.OpenSimplex2S);
        }

        protected override void GenerateWorld(Position blockPosition){
            for (var x = 0; x < Chunk.BlockCount; x++){
                for (var y = 0; y < Chunk.BlockCount; y++){
                    var pos = new Position(x + blockPosition.X, y + blockPosition.Y);
                    var tmp = _MainNoise.GetNoise(x + blockPosition.X, y + blockPosition.Y) +
                              0.75f * _ScondaryNoise.GetNoise((x + blockPosition.X), (y + blockPosition.Y)) +
                              0.25 * _noise.GetNoise((x + blockPosition.X), (y + blockPosition.Y));

                    tmp *= _firstDistanceRatioCalculator.GetValue(new Position((x + blockPosition.X),
                        (y + blockPosition.Y)));
                    if (tmp < -0.2f)
                        _chunk[x, y] = GetLandBlock(tmp, pos);
                    else
                        _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        }

        private Block GetLandBlock(double noiseValue, Position position){
            if (noiseValue > -0.45f && _biomeNoise.GetNoise(position.X, position.Y) > 0){
                return new Block(BlockType.Sand, BiomeType.Beach);
            }

            if (noiseValue < -1.0f && _biomeNoise.GetNoise(position.X, position.Y) < 0)
                return new Block(BlockType.Stone, BiomeType.Mountains);
            return new Block(BlockType.Grass, BiomeType.Grassland);
        }
    }
}