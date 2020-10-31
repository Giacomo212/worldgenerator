using System;
using System.IO;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class MapGenerator{
        private MemoryStream _memoryStream;
        private BinaryWriter _binaryWriter;
        public int PercentDone{ get; private set; } = 0;
        private Map _map;
        private IChunkGenerator _chunkGenerator;

        public MapGenerator(Map map, IChunkGenerator generator){
            _map = map;
            _memoryStream = new MemoryStream(_map.ChunkCount * _map.ChunkCount * Chunk.MemorySize);
            _binaryWriter = new BinaryWriter(_memoryStream);
            _binaryWriter.Write(map.Seed);
            _binaryWriter.Write((int) map.WorldType);
            _chunkGenerator = generator;
        }

        private void WriteChunk(Chunk chunk){
            for (var i = 0; i < Chunk.BlockCount; i++){
                for (var j = 0; j < Chunk.BlockCount; j++){
                    WriteBlock(chunk[i, j]);
                }
            }
        }

        private void WriteChunkAt(Chunk chunk, Position position){
            _binaryWriter.Seek(
                sizeof(int) * 2 + Chunk.MemorySize * position.X * (int) _map.WorldType + Chunk.MemorySize * position.Y,
                SeekOrigin.Begin);
            WriteChunk(chunk);
        }

        private void WriteBlock(Block block){
            _binaryWriter.Write((int) block.BlockType);
            _binaryWriter.Write((int) block.BiomeType);
            _binaryWriter.Write((int) block.ItemType);
        }
        
        public void GenerateNewWorld(){
            for (var i = 0; i < _map.ChunkCount; i++){
                for (var j = 0; j < _map.ChunkCount; j++){
                    WriteChunk(_chunkGenerator.GenerateChunk(new Position(i, j)));
                }

                PercentDone = (int) (Convert.ToDouble(i) / _map.ChunkCount * 100.0);
            }

            _memoryStream.Flush();
            _memoryStream.Position = 0;
            using var fs = File.Open(EnvironmentVariables.Worldfiles + Path.DirectorySeparatorChar + _map.Name + ".wg",
                FileMode.CreateNew);
            _memoryStream.CopyTo(fs);
        }

        // public MemoryStream GeneratePartOfWorld(Position startChunk, Position endChunk){
        //     var memoryStream = new MemoryStream();
        //     
        //     for (var i = startChunk.X; i < endChunk.X; i++){
        //         for (var j = startChunk.Y; j < endChunk.Y; j++){
        //             WriteChunk(_chunkGenerator.GenerateChunk(new Position(i, j)));
        //         }
        //
        //         PercentDone = (int) (Convert.ToDouble(i) / _map.ChunkCount * 100.0);
        //     }
        //
        //     return memoryStream;
        // }
    }
}