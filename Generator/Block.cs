
namespace World {
    public class Block {
        public static int Width { get; } = 40;
        public static int High { get; } = 40;
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
        public Block(BlockType blockBlockType, ItemType itemType,BiomeType biomeType){
            _blockType = blockBlockType;
            _itemType = itemType;
            _biomeType = biomeType;
        }
    }
}


