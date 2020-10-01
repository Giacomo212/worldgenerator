using System;
using System.IO;
using Types;


namespace Game.WorldMap{
    public class MapGenerator : IDisposable{
        private BinaryWriter _binaryWriter;
        private Types.Map _map;
        private IChunkGenerator _chunkGenerator;
        public MapGenerator(Types.Map map, IChunkGenerator generator){
            _map = map;
            _binaryWriter =
                new BinaryWriter(File.Open(
                    EnvironmentVariables.Worldfiles + Path.DirectorySeparatorChar + map.Name + ".wg",
                    FileMode.CreateNew));
            _binaryWriter.Write(map.Seed);
            _binaryWriter.Write((int) map.WorldType);
            _chunkGenerator = generator;
            GenerateNewWorld();
        }

        private void Write(Chunk chunk){
            for (int i = 0; i < Chunk.Size; i++){
                for (int j = 0; j < Chunk.Size; j++){
                    Write( chunk[i, j]);
                }
            }
        }

        private void WirteAt(Chunk chunk, Position position){
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

        private void Close(){
            _binaryWriter.Close();
        }

        private void GenerateNewWorld(){
            for (int i = 0; i < (int)_map.WorldType /Chunk.Size; i++){
                for (int j = 0; j < (int)_map.WorldType /Chunk.Size; j++){
                    Write(_chunkGenerator.GenerateChunk(new Position(i,j)));
                }
            }
            Close();
        }

        public void Dispose(){
            _binaryWriter.Close();
            _binaryWriter.Dispose();
        }
    }
}