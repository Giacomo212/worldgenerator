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
            GenerateItems(position);
            return _chunk;
        }

        protected abstract void GenerateWorld(Position blockPosition);
            
        
        protected virtual void GenerateItems(Position blockPosition){
            
        }

        
    }

     
}