using System.Linq;
using Types;

namespace Game.WorldMap{
    public class IslandWorldGenerator : ChunkGenerator{
        private readonly DistanceRatioCalculator _distanceRatioCalculator;
        private readonly FastNoiseLite _biomeNoise;
        public IslandWorldGenerator(Map map) : base(map){
            
            _biomeNoise = new FastNoiseLite(_random.Next());
            _biomeNoise.SetFrequency(0.009f);
            _biomeNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            
            _distanceRatioCalculator = new DistanceRatioCalculator(map.BlockCount, map.BlockCount);
            
        }

        protected override void GenerateWorld(Position blockPosition){
           
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    var pos = new Position(x + blockPosition.X, y + blockPosition.Y);
                    var tmp = _MainNoise.GetNoise(x + blockPosition.X, y + blockPosition.Y) +
                              0.75f * _ScondaryNoise.GetNoise((x + blockPosition.X), (y + blockPosition.Y)) +
                              0.25 * _noise.GetNoise((x + blockPosition.X), (y + blockPosition.Y));

                    tmp *= _distanceRatioCalculator.GetValue(new Position((x + blockPosition.X),
                        (y + blockPosition.Y)));
                    if (tmp < -0.4f)
                        // _chunk[x, y] = new Block(BlockType.Grass, BiomeType.Grassland);
                        _chunk[x, y] = GetLandBlock(tmp,pos);
                    else
                        _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        }

        private Block GetLandBlock(double noiseValue, Position position){
            if (noiseValue > -0.45f && _biomeNoise.GetNoise(position.X,position.Y) > 0){
                return new Block(BlockType.Sand,BiomeType.Beach);
            }
            if(noiseValue < -1.0f && _biomeNoise.GetNoise(position.X,position.Y) < 0)
                return new Block(BlockType.Stone,BiomeType.Mountains);
            return new Block(BlockType.Grass ,BiomeType.Grassland);
        }
        
    }
}