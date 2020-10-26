
namespace Types {
    public class Block {
        public static int PixelSize { get; } = 32;
        public static int SizeInMemory{ get; } = sizeof(int) * 3;
        private BiomeType _biomeType;
        private BlockType _blockType;
        private ItemType _itemType = ItemType.None;

        public ItemType ItemType => _itemType;

        public BlockType BlockType => _blockType;
        public BiomeType BiomeType => _biomeType;
        public Block(BlockType blockBlockType, BiomeType biomeType){
            _blockType = blockBlockType;
            _biomeType = biomeType;
        }
        public Block(BlockType blockBlockType,BiomeType biomeType,ItemType itemType){
            _blockType = blockBlockType;
            _itemType = itemType;
            _biomeType = biomeType;
        }
    }
}


