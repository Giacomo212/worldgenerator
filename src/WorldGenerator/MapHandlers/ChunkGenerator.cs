using System;
using FastNoise;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public abstract class ChunkGenerator : IChunkGenerator{
        // protected  readonly FastNoiseLite _MainNoise;
        // protected  readonly FastNoiseLite _ScondaryNoise;
        // protected readonly FastNoiseLite _noise;
        protected readonly BlockNoiseCalculator _blockNoiseCalculator;
        protected readonly Random _random;
        protected Chunk _chunk;
        protected Map _map;
        protected readonly float _maximalWaterValue;
        protected ChunkGenerator(Map  map, float maximalWaterValue){
            _map = map;
            _maximalWaterValue = maximalWaterValue;
            _chunk = new Chunk();
            _blockNoiseCalculator = new BlockNoiseCalculator(_map.Seed);
            _random = new Random(_map.Seed);
            // _MainNoise = new FastNoiseLite(_map.Seed);
            // _ScondaryNoise = new FastNoiseLite(_map.Seed);
            // _noise = new FastNoiseLite(_map.Seed);
            // _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            // _ScondaryNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            // _MainNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            // _MainNoise.SetFrequency(0.004f);
            // _ScondaryNoise.SetFrequency(0.005f);
            // _noise.SetFrequency(0.04f);
        }

        public Chunk GenerateChunk(Position chunkPosition){
            _chunk = new Chunk();
            var position = new Position(chunkPosition.X * Chunk.BlockCount, chunkPosition.Y * Chunk.BlockCount);
            GenerateWorld(position);
            GenerateItems(position);
            return _chunk;
        }

        protected virtual void GenerateWorld(Position blockPosition){
            
            for (var x = 0; x < Chunk.BlockCount; x++){
                for (var y = 0; y < Chunk.BlockCount; y++){
                    var pos = new Position(x + blockPosition.X, y + blockPosition.Y);
                    var tmp = CalculateBlockValue(pos);
                    if (tmp < _maximalWaterValue)
                        _chunk[x, y] = GetLandBlock(tmp, pos);
                    else
                        _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        
        }

        protected abstract double CalculateBlockValue(Position position);

        protected abstract Block GetLandBlock(double noiseValue, Position position);
        protected abstract void GenerateItems(Position blockPosition);

    }

     
}