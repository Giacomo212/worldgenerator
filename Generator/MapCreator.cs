using Libraries;
using Types;

namespace Generator{
    public abstract class MapCreator{
        protected IChunkGenerator _generator;
        protected Map _map;
        private MapWriter _mapWriter;
        public MapCreator(IChunkGenerator generator, Map map){
            _generator = generator;
            _map = map;
        }

        protected abstract void GenerateMap();
        protected abstract void SaveChunk();

    }
}