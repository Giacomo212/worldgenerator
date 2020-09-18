namespace World{
    public class Biome{
        private BiomeType _type;
        public BiomeType Type => _type;

        public Biome(BiomeType biomeType){
            _type = biomeType;
        }
    }
}