using System;
using Types;

namespace Game.WorldMap{
    public abstract class ChunkGenerator : IChunkGenerator{
        protected  readonly FastNoiseLite _MainNoise;
        protected  readonly FastNoiseLite _ScondaryNoise;
        protected readonly FastNoiseLite _noise;
        protected readonly Random _random;
        protected Chunk _chunk;
        protected Map _map;

        protected ChunkGenerator(Map  map){
            _map = map;
            _chunk = new Chunk();
            _MainNoise = new FastNoiseLite(_map.Seed);
            _random = new Random(_map.Seed);
            _ScondaryNoise = new FastNoiseLite(_map.Seed);
            _noise = new FastNoiseLite(_map.Seed);
            _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _ScondaryNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _MainNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _MainNoise.SetFrequency(0.004f);
            _ScondaryNoise.SetFrequency(0.005f);
            _noise.SetFrequency(0.04f);
        }

        public Chunk GenerateChunk(Position chunkPosition){
            _chunk = new Chunk();
            var position = new Position(chunkPosition.X * Chunk.Size, chunkPosition.Y * Chunk.Size);
            GenerateWorld(position);
            //GenerateMountains(position);
            //GenerateDesert(position);
            return _chunk;
        }

        protected abstract void GenerateWorld(Position position);
            
        

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