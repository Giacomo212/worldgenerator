using System.Linq;
using Types;

namespace Game.WorldMap{
    public class IslandWorldGenerator : ChunkGenerator{
        private Position _islandCenter = new Position(800, 800);
        private int _islandDiameter = 620;
        private Chunk _waterChunk;
        private DistanceRatioCalculator _distanceRatioCalculator;

        public IslandWorldGenerator(Map map) : base(map){
            _distanceRatioCalculator = new DistanceRatioCalculator(map.BlockCount, map.BlockCount);
            _waterChunk = new Chunk();
            for (int i = 0; i < Chunk.Size; i++){
                for (int j = 0; j < Chunk.Size; j++){
                    _waterChunk[i, j] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        }

        protected override void GenerateWorld(Position position){
            //CheckIfContainsWater(CrateChunk(new Position(position.X + Chunk.Size, position.Y + Chunk.Size)))
            var pos = new Position(position.X, position.Y);
            // if (Position.CalculateDistance(_islandCenter, pos) > _islandDiameter){
            //     _chunk = _waterChunk;
            //     return;
            // }
            _chunk = CrateLand(new Position(position.X, position.Y));
        }

        private Chunk CrateLand(Position position){
            var chunk = new Chunk();
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    var tmp = _MainNoise.GetNoise(x + position.X, y + position.Y) +
                              0.75f * _ScondaryNoise.GetNoise((x + position.X), (y + position.Y)) +
                              0.25 * _noise.GetNoise((x + position.X), (y + position.Y));
                    tmp *= _distanceRatioCalculator.GetValue(new Position((x + position.X), (y + position.Y)));
                    if (tmp < -0.4f)
                        chunk[x, y] = new Block(BlockType.Grass, BiomeType.Grassland);
                    else
                        chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }

            return chunk;
        }
    }
}