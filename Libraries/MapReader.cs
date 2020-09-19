using System;
using System.IO;
using Types;

namespace Libraries{
    public class MapReader{
        private FileStream _fileStream;
        private Map _map;
        public MapReader(FileStream fileStream, Map map){
            _fileStream = fileStream;
            _map = map;
        }

        private Chunk ReadChunk(){
            var chunk = new Chunk();
            for (int i = 0; i < Chunk.Size; i++){
                for (int j = 0; j < Chunk.Size; j++){
                    chunk[i, j] = ReadBlock();
                }
            }

            return chunk;
        }

        public Chunk ReadChunkAt(Position position){
            _fileStream.Seek(sizeof(int) * 2 + position.X * (int)_map.WorldType + position.Y * Chunk.SizeOf,SeekOrigin.Begin);
            return ReadChunk();
        }

        public Block ReadBlock(){
            using var reader = new BinaryReader(_fileStream);
            return new Block(
                (BlockType) reader.ReadInt32(), 
                (BiomeType) reader.ReadInt32(),
                (ItemType) reader.ReadInt32());
            
        }

        public static Map ReadMap(string fileName){
            var separator = Path.DirectorySeparatorChar;
            if (fileName.Contains("/") || fileName.Contains("\\"))
                throw new IOException("invalid file name");
            fileName = EnvironmentVariables.GameFiles + separator + $"worlds{separator}" + fileName;
            using var file = new BinaryReader(File.Open(fileName, FileMode.Open));
            var name = fileName.Replace(".wg", String.Empty);
            int seed = file.Read();
            WorldSize worldSize = (WorldSize)file.ReadInt32();
            return new Map(name, worldSize,seed);
        }
    }
}