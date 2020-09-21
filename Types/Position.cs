namespace Types{
    public class Position{
        public int X{ get; set; }

        public int Y{ get; set; }

        public Position(int x, int y){
            X = x;
            Y = y;
        }

        public static bool operator <(Position a, Position b){
            return a.X < b.X && a.Y < b.Y;
        }
        public static bool operator >(Position a, Position b){
            return a.X > b.X && a.Y > b.Y;
        }
        
        public static Position Zero => new Position(0, 0);
        

    }
}