using System;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class ChunkLoader : IDisposable{
        private Chunk[,] _currentChunks;
        private readonly int _chunkCount;
        private readonly Position _bufferBeginningPointer; //= Position.Zero;
        private Position _bufferEndPointer;
        private readonly MapReader _mapReader;
        private Position _bufferSize;

        public ChunkLoader(Position bufferSize, Map map){
            var tmp = map.ChunkCount/2;
            _bufferBeginningPointer = new Position(map.ChunkCount/2,map.ChunkCount/2);
            _chunkCount = (int) map.WorldType / Chunk.BlockCount;
            _bufferSize = bufferSize;
            _currentChunks = new Chunk[ bufferSize.X, bufferSize.Y];
            _bufferEndPointer = new Position( map.ChunkCount/2 + _bufferSize.X,map.ChunkCount/2 + _bufferSize.Y);
            _mapReader = new MapReader(map);
            GetCurrentChunks();
        }


        private void GetCurrentChunks(){
            for (int x = 0; x < _bufferSize.X; x++){
                for (int y = 0; y < _bufferSize.Y; y++){
                    _currentChunks[x, y] =
                        _mapReader.ReadChunkAt(new Position(_bufferBeginningPointer.X + x,
                            _bufferBeginningPointer.Y + y));
                }
            }
        }

        public ref Chunk[,] Chunks => ref _currentChunks;

        public void MoveRight(){
            if (_bufferEndPointer.X >= _chunkCount) return;
            _bufferBeginningPointer.X++;
            _bufferEndPointer.X++;
            GetCurrentChunks();
        }

        public void MoveLeft(){
            if (_bufferBeginningPointer.X <= 0) return;
            _bufferBeginningPointer.X--;
            _bufferEndPointer.X--;
            GetCurrentChunks();
        }

        public void MoveUp(){
            if (_bufferBeginningPointer.Y <= 0) return;
            _bufferBeginningPointer.Y--;
            _bufferEndPointer.Y--;
            GetCurrentChunks();
        }

        public void MoveDown(){
            if (_bufferEndPointer.Y >= _chunkCount) return;
            _bufferBeginningPointer.Y++;
            _bufferEndPointer.Y++;
            GetCurrentChunks();
        }


        public void Dispose(){
            _mapReader?.Dispose();
        }

        public void ChangeBuffer(Position bufferSize){
            _bufferSize = bufferSize;
            _currentChunks = new Chunk[_bufferSize.X, _bufferSize.Y];
            if (bufferSize.X + _bufferBeginningPointer.X < _chunkCount &&
                bufferSize.Y + _bufferBeginningPointer.Y < _chunkCount){
                _bufferEndPointer = new Position(_bufferBeginningPointer.X + _bufferSize.X,
                    _bufferBeginningPointer.Y + _bufferSize.Y);
                GetCurrentChunks();
                return;
            }

            if (bufferSize.X + _bufferBeginningPointer.X > _chunkCount){
                _bufferEndPointer.X = _chunkCount;
                _bufferBeginningPointer.X = _chunkCount - bufferSize.X;
            }

            if (bufferSize.Y + _bufferBeginningPointer.Y > _chunkCount){
                _bufferEndPointer.Y = _chunkCount;
                _bufferBeginningPointer.Y = _chunkCount - bufferSize.Y;
            }

            GetCurrentChunks();
        }
    }
}