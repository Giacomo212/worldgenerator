using System;
using Types;
using Game.WorldMap;

namespace Game{
    public class ChunkController : IDisposable{
        private Chunk[,] _currentChunks;
        private int _chunkCount;
        private Position _chunkBeginnig = Position.Zero;
        private Position _chunkEnd;
        private MapReader _mapReader;
        private readonly Position _bufferSize;
        
        public ChunkController(Chunk[,] chunks){
            _currentChunks = chunks;
        }

        public ChunkController(Position bufferSize, Map map){
            _chunkCount = (int) map.WorldType / Chunk.BlockCount;
            _bufferSize = bufferSize;
            _currentChunks = new Chunk[bufferSize.X, bufferSize.Y];
            _chunkEnd = new Position(_bufferSize.X, _bufferSize.Y);
            _mapReader = new MapReader(map);
            GetCurrentChunks();
            var chunk = new Chunk();
            
        }

        private void GetCurrentChunks(){
            for (int x = 0; x < _bufferSize.X; x++){
                for (int y = 0; y < _bufferSize.Y; y++){
                    _currentChunks[x, y] =
                        _mapReader.ReadChunkAt(new Position(_chunkBeginnig.X + x, _chunkBeginnig.Y + y));
                }
            }
        }

        public ref Chunk[,] Chunks => ref _currentChunks;

        public void MoveRight(){
            if (_chunkEnd.X >= _chunkCount) return;
            _chunkBeginnig.X++;
            _chunkEnd.X++;
            GetCurrentChunks();
        }

        public void MoveLeft(){
            if (_chunkBeginnig.X <= 0) return;
            _chunkBeginnig.X--;
            _chunkEnd.X--;
            GetCurrentChunks();
        }

        public void MoveUp(){
            if (_chunkBeginnig.Y <= 0) return;
            _chunkBeginnig.Y--;
            _chunkEnd.Y--;
            GetCurrentChunks();
        }

        public void MoveDown(){
            if (_chunkEnd.Y >= _chunkCount) return;
            _chunkBeginnig.Y++;
            _chunkEnd.Y++;
            GetCurrentChunks();
        }


        public void Dispose(){
            _mapReader?.Dispose();
        }
    }
}