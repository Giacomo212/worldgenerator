


namespace Game.DataContainers {
    public class Block {
        public static int PixelSize { get; } = 32;
        public static int SizeInMemory{ get; } = sizeof(int) * 3;

        public ItemType ItemType{ get; set; } = ItemType.None;

        public BlockType BlockType{ get; }

        public Game.DataContainers.BiomeType BiomeType{ get; }

        public Block(BlockType blockBlockType, Game.DataContainers.BiomeType biomeType){
            BlockType = blockBlockType;
            BiomeType = biomeType;
        }
        public Block(BlockType blockBlockType,Game.DataContainers.BiomeType biomeType,ItemType itemType){
            BlockType = blockBlockType;
            ItemType = itemType;
            BiomeType = biomeType;
        }
    }
}


