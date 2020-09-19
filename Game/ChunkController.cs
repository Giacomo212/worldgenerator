using Types;

namespace Game{
    public class ChunkController{
        private Chunk[,] _currentChunks;
        //private CameraController _camera;
        private Position _chunkBeginnig = Position.Zero;
        private Position _chunkEnd;
        
        public ChunkController(Position bufferSize){
            _currentChunks = new Chunk[bufferSize.X ,bufferSize.Y];
            _chunkEnd = bufferSize;
        }

        public Block this[int i, int j] {
            get{
                
                return 
            }
        }
        public void SetCurrentPosition(Position beginning){
            if (_chunkBeginnig > beginning){
                
            }
        }
            
        //public Block this[int x, int y] => _chunks[x, y].;
    }
}