using System.Collections;


namespace Game.DataContainers{
    public class Chunk : IEnumerable{
        private readonly Game.DataContainers.Block[,] _blocks = new Game.DataContainers.Block[BlockCount, BlockCount];
        public const int BlockCount = 16;
        public static readonly int PixelSize = BlockCount * Game.DataContainers.Block.PixelSize;
        public static readonly int MemorySize = Game.DataContainers.Block.SizeInMemory * BlockCount * BlockCount;


        public Game.DataContainers.Block this[int i, int j]{
            get => _blocks[i, j];
            set => _blocks[i, j] = value;
        }

        public IEnumerator GetEnumerator(){
            return new ChunkEnum(_blocks);
        }
    }
}