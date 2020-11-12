using FastNoise;
using Myra;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;


namespace WorldGenerator.MapHandlers{
    public class LandChunkGenerator : ChunkGenerator{
        private readonly BlockNoiseCalculator _blockNoiseCalculator;
        private readonly BlockNoiseCalculator _biomeNoiseCalculator;
        private readonly BlockNoiseCalculator _itemNoiseCalculator;

        public LandChunkGenerator(Map map) : base(map){
            _blockNoiseCalculator = new BlockNoiseCalculator(_map.Seed);
            _biomeNoiseCalculator = new BlockNoiseCalculator(_random.Next());
            _itemNoiseCalculator = new BlockNoiseCalculator(_random.Next());
        }


        protected override Block CalculateBlock(Position position){
            var tmp = _blockNoiseCalculator.GetValue(position);
            if (tmp < 0.4f)
                return GenerateLand(position);
            return new Block(BlockType.Water, BiomeType.Ocean);
        }

        private Block GenerateLand(Position position){
            var tmp = _biomeNoiseCalculator.GetValue(position);
            if (_blockNoiseCalculator.GetValue(position) > 0.35f && tmp > 0){
                return new Block(BlockType.Sand, BiomeType.Beach);
            }

            if (tmp < -0.5f){
                return new Block(BlockType.Sand, BiomeType.Desert);
            }

            if (tmp > 0.5f){
                return new Block(BlockType.Snow, BiomeType.Taiga);
            }

            return new Block(BlockType.Grass, BiomeType.Forest);
        }

        protected override ItemType CalculateItem(Position blockPosition, Block block){
            if (!(_itemNoiseCalculator.GetValue(blockPosition) > 0.2) || _random.Next(0, 3) != 2) return ItemType.None;
            switch (block.BiomeType){
                case BiomeType.Desert:
                    if (_random.Next(0, 5) == 2) return ItemType.Cactus;
                    break;
                case BiomeType.Forest:
                    return ItemType.Tree;
                    break;
                case BiomeType.Taiga:
                    return ItemType.Pine;
                    break;
                default:
                    ;
                    break;
            }

            return ItemType.None;
        }
    }
}