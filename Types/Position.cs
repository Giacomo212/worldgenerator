namespace Types{
    public class Position{
        private int _x, y;

        public Position(int x, int y){
            _x = x;
            this.y = y;
        }

        public static bool operator <(Position a, Position b){
            return a.X < b.X && a.Y < b.Y;
        }
        public static bool operator >(Position a, Position b){
            return a.X > b.X && a.Y > b.Y;
        }
        public int X => _x;
        public int Y => y;
        public static Position Zero => new Position(0, 0);
        

    }
}