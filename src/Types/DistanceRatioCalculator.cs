namespace Types{
    public class DistanceRatioCalculator{
        private readonly Position _beginning;
        private  readonly Position _size;
        private readonly Position _centerOfCalculator;
        public double CalculationRatio{ get; set; } = 1.5;
        
        public DistanceRatioCalculator(int xSize, int ySize){
            _beginning = Position.Zero;
            _centerOfCalculator = new Position(xSize/2, ySize/2 );
            _size = new Position(xSize,ySize);
        }
        public DistanceRatioCalculator(Position beginningPosition, Position size){
            _beginning = beginningPosition;
            _centerOfCalculator = new Position(beginningPosition.X + size.X/2,beginningPosition.Y + size.Y/2 );
            _size = size;
        }

        public double GetValue(Position position){
            if (position.X > _beginning.X + _size.X || position.Y > _beginning.Y + _size.Y)
                return 0;
            var tmp = Position.CalculateDistance(_beginning, _centerOfCalculator);
            var distance = tmp - Position.CalculateDistance(position,_centerOfCalculator);
            return tmp != 0? distance / tmp * CalculationRatio:0 ;
        }
    }
}