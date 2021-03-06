using WorldGenerator.MapElements;
using WorldGenerator.MapHandlers;

namespace WorldGenerator.Process{
    public class MapGenerationProcess : ICheckableProcess{
        protected readonly Map _map;
        protected readonly IChunkGenerator ChunkGenerator;
        private readonly MapGenerator _generator;

        public MapGenerationProcess(Map map, IChunkGenerator chunkGenerator){
            _map = map;
            ChunkGenerator = chunkGenerator;
            _generator = new MapGenerator(_map, ChunkGenerator);
        }

        public int CheckPercentDone(){
            return _generator.PercentDone;
        }

        public void Run(){
            _generator.GenerateNewWorld();
        }
    }
}