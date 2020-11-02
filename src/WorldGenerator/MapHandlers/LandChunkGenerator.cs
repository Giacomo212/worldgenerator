using FastNoise;
using Myra;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;


namespace WorldGenerator.MapHandlers{
    public class LandChunkGenerator : ChunkGenerator{
        private BlockNoiseCalculator _biomeNoiseCalculator;
        

        public LandChunkGenerator(Map map) : base(map, 0.4f){
            _biomeNoiseCalculator = new BlockNoiseCalculator(_random.Next());
        }

        protected override double CalculateBlockValue(Position position){
            return _blockNoiseCalculator.GetValue(position);
        }

        protected override Block GetLandBlock(double noiseValue, Position position){
            var tmp = _biomeNoiseCalculator.GetValue(position);
            if (noiseValue > 0.35f && tmp > 0){
                return new Block(BlockType.Sand, BiomeType.Beach);
            }

            if (tmp < -0.6f){
                return new Block(BlockType.Sand, BiomeType.Desert);
            }

            if (tmp > 0.6f){
                return new Block(BlockType.Snow, BiomeType.Taiga);
            }

            return new Block(BlockType.Grass, BiomeType.Forest);
        }

        protected override void GenerateItems(Position blockPosition){
            for (var i = 0; i < Chunk.BlockCount; i++){
                for (var j = 0; j < Chunk.BlockCount; j++){
                    if (_random.Next(0, 25) > 23){
                        switch (_chunk[i, j].BiomeType){
                            case BiomeType.Desert:
                                _chunk[i, j].ItemType = ItemType.Cactus;
                                break;
                            case BiomeType.Forest:
                                _chunk[i, j].ItemType = ItemType.Tree;
                                break;
                            case BiomeType.Taiga:
                                _chunk[i, j].ItemType = ItemType.Pine;
                                break;
                            default:
                                ;
                                break;
                        }
                    }
                }
            }
        }
    }
}