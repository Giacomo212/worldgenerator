using System.IO;
using Types;


namespace Libraries{
    public class MapWriter{
        private FileStream _fileStream;
        public MapWriter(FileStream fileStream){
            _fileStream = fileStream;

        }
        private void Write(Chunk chunk){
            //_fileStream _fileStream.Seek(Chunk.SizeOf *  )
            foreach (Block block in chunk){
                Write(block);
            }
        }
        
        private void Write(Block block){
            // Write((int)block.BlockType);
            // Write((int)block.BiomeType);
            // Write((int)block.ItemType);
        }
    }
}