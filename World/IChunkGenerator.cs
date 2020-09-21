using Types;

namespace World{
    public interface IChunkGenerator{
        public Chunk GenerateChunk(Position position);
    }
}