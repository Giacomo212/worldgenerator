using Generator;

namespace Game{
    public class ChangeToNewMap : IAction{
        private WorldSize _worldSize;
        private string _name;
        public ChangeToNewMap(WorldSize worldSize, string name){
            _worldSize = worldSize;
            _name = name;
        }

        public Context ReturnNewContext(){
            return new MapContext(_worldSize,_name);
        }
    }

    public class ChangeToMap : IAction{
        private string _name;

        public ChangeToMap(string name){
            _name = name;
        }

        public Context ReturnNewContext(){
            return new MapContext(_name);
        }
    }
}