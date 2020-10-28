using Game.DataContainers;
using Game.Utils;



namespace Game.WorldMap{
    public interface IChunkGenerator{
        public Chunk GenerateChunk(Position position);
    }
}