using System.Collections;

namespace WorldGenerator.WorldMap{
    public class Chunk : IEnumerable{
        private readonly Block[,] _blocks = new Block[BlockCount, BlockCount];
        public const int BlockCount = 16;
        public static readonly int PixelSize = BlockCount * Block.PixelSize;
        public static readonly int MemorySize = Block.SizeInMemory * BlockCount * BlockCount;


        public Block this[int i, int j]{
            get => _blocks[i, j];
            set => _blocks[i, j] = value;
        }

        public IEnumerator GetEnumerator(){
            return new ChunkEnumerator(_blocks);
        }
    }
}