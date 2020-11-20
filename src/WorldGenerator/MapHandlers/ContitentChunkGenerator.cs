using FastNoise;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class ContinentChunkGenerator : ChunkGenerator{
        // private readonly DistanceRatioCalculator _firstDistanceRatioCalculator;
        // private readonly DistanceRatioCalculator _secondDistanceRatioCalculator = new DistanceRatioCalculator(400, 200);
        // private readonly DistanceRatioCalculator _thirdDistanceRatioCalculator = new DistanceRatioCalculator(400, 200);
        private readonly FastNoiseLite _biomeNoise;
        private readonly int _continentRadius;
        

        public ContinentChunkGenerator(Map map) : base(map){
            _continentRadius = map.BlockCount / 8;
            
            // _firstDistanceRatioCalculator = new DistanceRatioCalculator(new Position(800, 800), new Position(400, 400));
            // _firstDistanceRatioCalculator.CalculationRatio = 4.0;
            // _firstDistanceRatioCalculator.CalculationRatio = 0.5;
            _biomeNoise = new FastNoiseLite(_random.Next());
            _biomeNoise.SetFrequency(0.009f);
            _biomeNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        }

        protected override Block GenerateBlock(Position blockPosition){
            throw new System.NotImplementedException();
        }

        protected override ItemType GenerateItem(Position blockPosition, Block block){
            throw new System.NotImplementedException();
        }


        protected Block GetLandBlock(double noiseValue, Position position){
            if (noiseValue > -0.45f && _biomeNoise.GetNoise(position.X, position.Y) > 0)
                return new Block(BlockType.Sand, BiomeType.Beach);
            if (noiseValue < -1.0f && _biomeNoise.GetNoise(position.X, position.Y) < 0)
                return new Block(BlockType.Stone, BiomeType.Mountains);
            return new Block(BlockType.Grass, BiomeType.Forest);
        }


        // private DistanceRatioCalculator GetClosestRatioCalculator(Position position){
        //     var nearestCalculator = _firstDistanceRatioCalculator;
        //     if (Position.CalculateDistance(nearestCalculator.CenterOfCalculator, position) >
        //         Position.CalculateDistance(_secondDistanceRatioCalculator.CenterOfCalculator, position)){
        //         nearestCalculator = _secondDistanceRatioCalculator;
        //     }
        //
        //     if (Position.CalculateDistance(nearestCalculator.CenterOfCalculator, position) >
        //         Position.CalculateDistance(_thirdDistanceRatioCalculator.CenterOfCalculator, position)){
        //         nearestCalculator = _secondDistanceRatioCalculator;
        //     }
        //
        //     return nearestCalculator;
        // }
    }
}