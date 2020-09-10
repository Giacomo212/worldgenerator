using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game {
    public class PerlinNoiseGenerator {
 
        FastNoise fastNoise;

        public double getValue(float x, float y) {

            return fastNoise.GetPerlin(x, y);
        }
        public PerlinNoiseGenerator() {
            Random random = new Random();
            fastNoise = new FastNoise(random.Next());
            fastNoise.SetNoiseType(FastNoise.NoiseType.Perlin);
            fastNoise.SetFrequency(0.1f);
        }
    }
}

