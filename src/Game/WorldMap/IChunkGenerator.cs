using Types;

namespace Game.WorldMap{
    public interface IChunkGenerator{
        public Chunk GenerateChunk(Position position);
    }
}