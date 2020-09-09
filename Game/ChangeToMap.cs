namespace worldgenerator{
    public class ChangeToNewMap : IAction{
        private int _x, _y;
        private string _name;
        public ChangeToNewMap(int x, int y, string name){
            _x = x;
            _y = y;
            _name = name;
        }

        public Context ReturnNewContext(){
            return new MapContext(_x, _y,_name);
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