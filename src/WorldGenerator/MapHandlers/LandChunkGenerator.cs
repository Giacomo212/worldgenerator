using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class LandChunkGenerator : ChunkGenerator{
        public LandChunkGenerator(Map map) : base(map){
        }

        protected override void GenerateWorld(Position blockPosition){
            for (var x = 0; x < Chunk.BlockCount; x++){
                for (var y = 0; y < Chunk.BlockCount; y++){
                    var tmp = _MainNoise.GetNoise(x + blockPosition.X , y + blockPosition.Y) +
                              0.75f * _ScondaryNoise.GetNoise((x + blockPosition.X), (y + blockPosition.Y)) +
                              0.25 * _noise.GetNoise((x + blockPosition.X), (y + blockPosition.Y));
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