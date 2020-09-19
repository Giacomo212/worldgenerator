using System;
using System.IO;
using Libraries;
using Types;

namespace Generator{
    public class SurfaceMapCreator : MapCreator {
        
        private MapWriter _mapWriter;
        public SurfaceMapCreator(IChunkGenerator generator, Map map) : base(generator, map){
            _mapWriter = new MapWriter(File.Open(EnvironmentVariables.GameFiles + map.Name,FileMode.Create),map);
            
        }

       

        protected override void GenerateMap(){
            for (int i = 0; i < (int)_map.WorldType / Chunk.Size ; i++){
                SaveChunk();
            }
        }

        protected override void SaveChunk(){
            _mapWriter.Write(_generator.GenerateChunk());
        }
    }
}