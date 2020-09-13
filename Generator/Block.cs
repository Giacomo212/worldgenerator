
namespace Generator {
    public class Block {
        public static int Width { get; } = 40;
        public static int High { get; } = 40;
        private int _blockID;
        private Item _item;
        
        public int BlockID => _blockID;

        public Block(int blockID) {
            _blockID = blockID;
        }
    }
}


