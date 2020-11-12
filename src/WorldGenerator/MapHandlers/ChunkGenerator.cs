using System;
using FastNoise;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public abstract class ChunkGenerator : IChunkGenerator{
        protected readonly Random _random;
        protected readonly Map _map;
        
        protected ChunkGenerator(Map map){
            _map = map;
            _random = new Random(_map.Seed);
        }

        public Chunk GenerateChunk(Position chunkPosition){
            var position = new Position(chunkPosition.X * Chunk.BlockCount, chunkPosition.Y * Chunk.BlockCount);
            return GenerateWorld(position);
        }

        private Chunk GenerateWorld(Position blockPosition){
            var zeroY = blockPosition.Y;
            var chunk = new Chunk();
            for (var x = 0; x < Chunk.BlockCount; x++){
                for (var y = 0; y < Chunk.BlockCount; y++){
                    var pos = new Position(x + blockPosition.X, y + blockPosition.Y);
                    chunk[x, y] = CalculateBlock(pos);
                    chunk[x, y].ItemType =
                        CalculateItem(pos, chunk[x, y]);
                }
            }

            return chunk;
        }

        protected abstract Block CalculateBlock(Position blockPosition);
        protected abstract ItemType CalculateItem(Position blockPosition, Block block);
    }
}