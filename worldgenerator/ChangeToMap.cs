namespace worldgenerator{
    public class ChangeToNewMap : IAction{
        private int _x, _y;

        public ChangeToNewMap(int x, int y){
            _x = x;
            _y = y;
        }

        public Context ReturnNewContext(){
            return new MapContext(_x, _y);
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