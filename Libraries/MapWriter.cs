using System;
using System.IO;
using Types;


namespace Libraries{
    public class MapWriter{
        private FileStream _fileStream;
        private Map _map;

        public MapWriter(FileStream fileStream, Map map){
            _fileStream = fileStream;
            _map = map;
            using var writer = new BinaryWriter(_fileStream);
            writer.Write(map.Seed);
            writer.Write((int)map.WorldType);
            
        }
        
        public void Write(Chunk chunk){
            
            foreach (Block block in chunk){
                Write(block);
            }
        }

        public void WirteAt(Chunk chunk, Position position){
            _fileStream.Seek(sizeof(int)*2 + Chunk.SizeOf * position.X * (int) _map.WorldType + Chunk.SizeOf * position.Y, SeekOrigin.Begin);
            Write(chunk);
            }

        private void Write(Block block){
            using var writer = new BinaryWriter(_fileStream);
            writer.Write((int) block.BlockType);
            writer.Write((int) block.BiomeType);
            writer.Write((int) block.ItemType);
        }
    }
}