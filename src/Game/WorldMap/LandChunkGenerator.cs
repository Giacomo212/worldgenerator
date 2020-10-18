using Types;

namespace Game.WorldMap{
    public class LandChunkGenerator : ChunkGenerator{
        public LandChunkGenerator(Map map) : base(map){
        }

        protected override void GenerateWorld(Position position){
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    var tmp = _MainNoise.GetNoise(x + position.X , y + position.Y) +
                              0.75f * _ScondaryNoise.GetNoise((x + position.X), (y + position.Y)) +
                              0.25 * _noise.GetNoise((x + position.X), (y + position.Y));
                    ;
                    if (tmp < 0.4f)
                        _chunk[x, y] = new Block(BlockType.Grass, BiomeType.Grassland);
                    else
                        _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        }
    }
}