using Types;

namespace Game.WorldMap{
    public class ContinentChunkGenerator : ChunkGenerator{
        public ContinentChunkGenerator(Map map) : base(map){
        }

        protected override void GenerateWorld(Position position){
            throw new System.NotImplementedException();
        }
    }
}