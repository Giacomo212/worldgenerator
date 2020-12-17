using System;
using System.Collections.Generic;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class ChunkLoader : IDisposable{
        private List<List<Chunk>> _columns;
        private readonly Map _map;
        private readonly Position _bufferBeginningPointer; //= Position.Zero;
        private Position _bufferEndPointer;
        private readonly MapReader _mapReader;
        private Position _bufferSize;

        public ChunkLoader(Position bufferSize, Map map){
            _bufferBeginningPointer = new Position(map.ChunkCount/2,map.ChunkCount/2);
            _map = map;
            _bufferSize = bufferSize;
            _bufferEndPointer = new Position( map.ChunkCount/2 + _bufferSize.X,map.ChunkCount/2 + _bufferSize.Y);
            _mapReader = new MapReader(map);
            GetCurrentChunks();
        }


        private void GetCurrentChunks(){
            _columns = new List<List<Chunk>>(_bufferSize.X);
            
            for (int x = 0; x < _bufferSize.X; x++){
                _columns.Add(new List<Chunk>(_bufferSize.Y));
                for (int y = 0; y < _bufferSize.Y; y++){
                    _columns[x].Add(new Chunk());
                    _columns[x][y] = _mapReader.ReadChunkAt(new Position(_bufferBeginningPointer.X + x,
                        _bufferBeginningPointer.Y + y)); //=
                        
                }
            }
        }

        public ref List<List<Chunk>> Chunks => ref _columns;

        public void MoveRight(){
            if (_bufferEndPointer.X >= _map.ChunkCount) return;
            _bufferBeginningPointer.X++;
            _bufferEndPointer.X++;
            _columns.RemoveAt(0);
            var grid = new List<Chunk>(_bufferSize.Y);
            for (int i = 0; i < _bufferSize.Y; i++){
                var chunkPosition = new Position(_bufferEndPointer.X -1 , _bufferBeginningPointer.Y + i);
                grid.Add(_mapReader.ReadChunkAt(chunkPosition));
            }
            _columns.Add(grid);
            
        }

        public void MoveLeft(){
            if (_bufferBeginningPointer.X <= 0) return;
            _bufferBeginningPointer.X--;
            _bufferEndPointer.X--;
            _columns.RemoveAt(_bufferSize.X-1);
            var tmp = new List<Chunk>(_bufferSize.Y);
            for (int i = 0; i < _bufferSize.Y; i++){
                var chunkPosition = new Position(_bufferBeginningPointer.X ,_bufferBeginningPointer.Y + i);
                tmp.Add(_mapReader.ReadChunkAt(chunkPosition));
            }
            _columns.Insert(0,tmp);
            
        }

        public void MoveUp(){
            if (_bufferBeginningPointer.Y <= 0) return;
            _bufferBeginningPointer.Y--;
            _bufferEndPointer.Y--;
            
            for (int i = 0; i < _bufferSize.X; i++){
                _columns[i].RemoveAt(_bufferSize.Y-1);
                _columns[i].Insert(0,_mapReader.ReadChunkAt(new Position(_bufferBeginningPointer.X + i,_bufferBeginningPointer.Y )));
            }
        }

        public void MoveDown(){
            if (_bufferEndPointer.Y >= _map.ChunkCount) return;
            _bufferBeginningPointer.Y++;
            _bufferEndPointer.Y++;
            for (int i = 0; i < _bufferSize.X; i++){
                _columns[i].RemoveAt(0);
                _columns[i].Add(_mapReader.ReadChunkAt(new Position(_bufferBeginningPointer.X + i,_bufferEndPointer.Y - 1 )));
            }
        }
        
        public void Dispose(){
            _mapReader?.Dispose();
        }

        public void ChangeBuffer(Position bufferSize){
            _bufferSize = bufferSize;
            _columns = new List<List<Chunk>>(bufferSize.X);
            for (int i = 0; i < bufferSize.X; i++){
                _columns.Add(new List<Chunk>(bufferSize.Y));
            }
            if (bufferSize.X + _bufferBeginningPointer.X < _map.ChunkCount &&
                bufferSize.Y + _bufferBeginningPointer.Y < _map.ChunkCount){
                _bufferEndPointer = new Position(_bufferBeginningPointer.X + _bufferSize.X,
                    _bufferBeginningPointer.Y + _bufferSize.Y);
                GetCurrentChunks();
                return;
            }
            if (bufferSize.X + _bufferBeginningPointer.X > _map.ChunkCount){
                _bufferEndPointer.X = _map.ChunkCount;
                _bufferBeginningPointer.X = _map.ChunkCount - bufferSize.X;
            }
            if (bufferSize.Y + _bufferBeginningPointer.Y > _map.ChunkCount){
                _bufferEndPointer.Y = _map.ChunkCount;
                _bufferBeginningPointer.Y = _map.ChunkCount - bufferSize.Y;
            }

            GetCurrentChunks();
        }
    }
}