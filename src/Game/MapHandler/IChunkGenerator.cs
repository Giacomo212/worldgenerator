using Game.Utils;
using Game.WorldMap;

namespace Game.MapHandler{
    public interface IChunkGenerator{
        public Chunk GenerateChunk(Position position);
    }
}