using System;
using Types;

namespace Game.WorldMap{
    public class ChunkGenerator : IChunkGenerator{
        protected  readonly FastNoiseLite _MainNoise;
        protected  readonly FastNoiseLite _ScondaryNoise;
        protected readonly Random _random;
        protected Chunk _chunk;

        public ChunkGenerator(int seed){
            _chunk = new Chunk();
            _MainNoise = new FastNoiseLite(seed);
            _random = new Random(seed);
            _ScondaryNoise = new FastNoiseLite(seed);
            _ScondaryNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _MainNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            
            _MainNoise.SetFrequency(0.004f);
           
            
            _ScondaryNoise.SetFrequency(0.01f);
            
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
                    var tmp =  _MainNoise.GetNoise(x + position.X, y + position.Y)  +  0.25 * _ScondaryNoise.GetNoise(2* (x + position.X), 2*(y + position.Y)) ;
                    if (tmp < 0.2)
                        _chunk[x, y] = new Block(BlockType.Grass, BiomeType.Grassland);
                    else
                        _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        }

        protected virtual void GenerateMountains(Position position){
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    if (_chunk[x, y].BlockType != BlockType.Grass && _chunk[x, y].BlockType != BlockType.Stone) continue;
                    var tmp = _ScondaryNoise.GetNoise(x + position.X, y + position.Y);
                     if (tmp < -0.7f){
                        _chunk[x, y] = new Block(BlockType.Stone, BiomeType.Mountains);
                    }
                }
            }
        }

        protected virtual void GenerateDesert(Position position){
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    if (_chunk[x, y].BlockType != BlockType.Grass && _chunk[x, y].BlockType != BlockType.Sand) continue;
                    var tmp = _ScondaryNoise.GetNoise(x + position.X, y + position.Y);
                    if (tmp < -0.4f){
                        _chunk[x, y] = new Block(BlockType.Sand, BiomeType.Desert);
                    }
                }
            }
        }

        
    }
}