using WorldGenerator.Utils;
using WorldGenerator.WorldMap;

namespace WorldGenerator.MapHandlers{
    public interface IChunkGenerator{
        public Chunk GenerateChunk(Position position);
    }
}