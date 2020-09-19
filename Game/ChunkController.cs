using Types;

namespace Generator{
    public class ChunkController{
        private Chunk[,] _currentChunks;
        private Map _map;

        public ChunkController(Map map){
            _map = map;
        }

        //public Block this[int x, int y] => _chunks[x, y].;
    }
}