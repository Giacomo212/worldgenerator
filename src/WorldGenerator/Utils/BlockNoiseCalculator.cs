using System;
using FastNoise;

namespace WorldGenerator.Utils{
    public class NoiseCalculator{
        private readonly FastNoiseLite _firstNoise;
        private readonly FastNoiseLite _secondNose;
        private readonly FastNoiseLite _thirdNoise;

        public NoiseCalculator(int seed){
            var random = new Random(seed);
            _firstNoise = new FastNoiseLite(seed);
            _secondNose = new FastNoiseLite(seed);
            _thirdNoise = new FastNoiseLite(seed);
            _firstNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _secondNose.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _thirdNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            _firstNoise.SetFrequency(0.004f);
            _secondNose.SetFrequency(0.005f);
            _thirdNoise.SetFrequency(0.04f);
        }

        public double GetValue(Position position){
            return _firstNoise.GetNoise(position.X, position.Y) +
                   0.75f * _secondNose.GetNoise(position.X, position.Y) +
                   0.25 * _thirdNoise.GetNoise(position.X, position.Y);
        }

        public void SetFirstFrequency(float value){
            _firstNoise.SetFrequency(value);
        }

        public void SetSecondFrequency(float value){
            _secondNose.SetFrequency(value);
        }

        public void SetThirdFrequency(float value){
            _thirdNoise.SetFrequency(value);
        }
    }
}