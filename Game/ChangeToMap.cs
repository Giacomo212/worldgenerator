using System.IO;
using Types;
using World;

namespace Game{
    public class ChangeToNewMap : IAction{
        private Map _map;
        public ChangeToNewMap(Map map){
            _map = map;
        }
    
        public Context ReturnNewContext(){
            MapGenerator mapGenerator = new MapGenerator(_map,new SurfaceChunkGenerator(_map.Seed));
            mapGenerator.GenerateNewWorld();
            return new MapContext(_map);
        }
    }

    public class ChangeToMap : IAction{
        private string _name;
    
        public ChangeToMap(string name){
            _name = name;
        }
    
        public Context ReturnNewContext(){
            
            return new MapContext(MapReader.ReadMap(_name));
        }
    }
}