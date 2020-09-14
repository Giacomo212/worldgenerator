
namespace Generator {
    public class Block {
        public static int Width { get; } = 40;
        public static int High { get; } = 40;
        private BiomeType _biomeType;
        private BlockType _blockType;
        private Item _item;
        
        public BlockType BlockType => _blockType;
        public BiomeType BiomeType => _biomeType;
        public Block(BlockType blockBlockType){
            _blockType = blockBlockType;
        }
    }
}


