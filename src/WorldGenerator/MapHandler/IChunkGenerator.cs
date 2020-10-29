using WorldGenerator.Utils;
using WorldGenerator.WorldMap;

namespace WorldGenerator.MapHandler{
    public interface IChunkGenerator{
        public Chunk GenerateChunk(Position position);
    }
}