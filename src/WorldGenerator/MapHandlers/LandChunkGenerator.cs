using FastNoise;
using Myra;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;


namespace WorldGenerator.MapHandlers{
    public class LandChunkGenerator : ChunkGenerator{
        private readonly NoiseCalculator _noiseCalculator;
        private readonly NoiseCalculator _biomeNoiseCalculator;
        private readonly NoiseCalculator _itemNoiseCalculator;

        public LandChunkGenerator(Map map) : base(map){
            _noiseCalculator = new NoiseCalculator(_map.Seed);
            _biomeNoiseCalculator = new NoiseCalculator(_random.Next());
            _itemNoiseCalculator = new NoiseCalculator(_random.Next());
        }

        protected override Block GenerateBlock(Position position){
            var tmp = _noiseCalculator.GetValue(position);
            return tmp < 0.4f
                ? GenerateLand(position)
                : new Block(BlockType.Water, BiomeType.Ocean);
        }

        private Block GenerateLand(Position position){
            var tmp = _biomeNoiseCalculator.GetValue(position);
            if (_noiseCalculator.GetValue(position) > 0.35f && tmp > 0){
                return new Block(BlockType.Sand, BiomeType.Beach);
            }

            if (tmp < -0.5f){
                return new Block(BlockType.Sand, BiomeType.Desert);
            }

            return tmp > 0.5f
                ? new Block(BlockType.Snow, BiomeType.Taiga)
                : new Block(BlockType.Grass, BiomeType.Forest);
        }

        protected override ItemType GenerateItem(Position blockPosition, Block block){
            if (!(_itemNoiseCalculator.GetValue(blockPosition) > 0.2) || _random.Next(0, 3) != 2) return ItemType.None;
            switch (block.BiomeType){
                case BiomeType.Desert:
                    if (_random.Next(0, 5) == 2) return ItemType.Cactus;
                    break;
                case BiomeType.Forest:
                    return ItemType.Tree;
                case BiomeType.Taiga:
                    return ItemType.Pine;
                default:
                    ;
                    break;
            }

            return ItemType.None;
        }
    }
}