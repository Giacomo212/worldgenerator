using System;
using Types;

namespace Game.WorldMap{
    public class ChunkGenerator : IChunkGenerator{
        protected readonly FastNoiseLite _landNoise;
        protected FastNoiseLite _mountainNoise;
        protected readonly Random _random;
        protected Chunk _chunk;

        public ChunkGenerator(int seed){
            _chunk = new Chunk();
            _landNoise = new FastNoiseLite(seed);
            _random = new Random(seed);
            _landNoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            _landNoise.SetFractalOctaves(10);
            //_landNoise.SetFrequency(0.004f);
            _mountainNoise = new FastNoiseLite(_random.Next());
            _mountainNoise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
            //_mountainNoise.SetFrequency(0.006f);
            
        }

        public Chunk GenerateChunk(Position chunkPosition){
            _chunk = new Chunk();
            var position = new Position(chunkPosition.X * Chunk.Size, chunkPosition.Y * Chunk.Size);
            GenerateWorld(position);
            //GenerateMountains(position);
            
            //GenerateDesert(position);
            return _chunk;
        }

        protected virtual void GenerateWorld(Position position){
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    var tmp = _landNoise.GetNoise(x + position.X, y + position.Y);
                    if (tmp < 0.2)
                        _chunk[x, y] = new Block(BlockType.Grass, BiomeType.Grassland);
                    else if (tmp < 0.25)
                        _chunk[x, y] = new Block(BlockType.Sand, BiomeType.Beach);
                    else
                        _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        }

        protected virtual void GenerateMountains(Position position){
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    if (_chunk[x, y].BlockType != BlockType.Grass && _chunk[x, y].BlockType != BlockType.Stone) continue;
                    var tmp = _mountainNoise.GetNoise(x + position.X, y + position.Y);
                     if (tmp < -0.5f){
                        _chunk[x, y] = new Block(BlockType.Stone, BiomeType.Mountains);
                    }
                }
            }
        }

        protected virtual void GenerateDesert(Position position){
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    if (_chunk[x, y].BlockType != BlockType.Grass && _chunk[x, y].BlockType != BlockType.Sand) continue;
                    var tmp = _mountainNoise.GetNoise(x + position.X, y + position.Y);
                    if (tmp > 0.5f){
                        _chunk[x, y] = new Block(BlockType.Sand, BiomeType.Desert);
                    }
                }
            }
        }

        
    }
}