using System;
using System.IO;
using Types;


namespace Game.WorldMap{
    public class MapGenerator{
        private MemoryStream _memoryStream;
        private BinaryWriter _binaryWriter;
        private Map _map;
        private IChunkGenerator _chunkGenerator;
        private bool IsLocked = false;
        public MapGenerator(Types.Map map, IChunkGenerator generator){
            _map = map;
            _memoryStream = new MemoryStream(_map.ChunkCount * _map.ChunkCount * Chunk.SizeOf);
            _binaryWriter = new BinaryWriter(_memoryStream);
            _binaryWriter.Write(map.Seed);
            _binaryWriter.Write((int) map.WorldType);
            _chunkGenerator = generator;
        }

        private void Write(Chunk chunk){
            for (var i = 0; i < Chunk.BlockCount; i++){
                for (var j = 0; j < Chunk.BlockCount; j++){
                    Write( chunk[i, j]);
                }
            }
        }

        private void WriteChunkAt(Chunk chunk, Position position){
            _binaryWriter.Seek(
                sizeof(int) * 2 + Chunk.SizeOf * position.X * (int) _map.WorldType + Chunk.SizeOf * position.Y,
                SeekOrigin.Begin);
            Write(chunk);
        }

        private void Write(Block block){
            _binaryWriter.Write((int) block.BlockType);
            _binaryWriter.Write((int) block.BiomeType);
            _binaryWriter.Write((int) block.ItemType);
        }
        

        public void GenerateNewWorld(){
            for (int i = 0; i < (int)_map.WorldType /Chunk.BlockCount; i++){
                for (int j = 0; j < (int)_map.WorldType /Chunk.BlockCount; j++){
                    Write(_chunkGenerator.GenerateChunk(new Position(i,j)));
                }
            }
            _memoryStream.Flush();
            _memoryStream.Position = 0;
            using var fs = File.Open(EnvironmentVariables.Worldfiles + Path.DirectorySeparatorChar + _map.Name + ".wg", FileMode.CreateNew);
            _memoryStream.CopyTo(fs);
        }
    }
}