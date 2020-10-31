using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public interface IChunkGenerator{
        public Chunk GenerateChunk(Position position);
    }
}