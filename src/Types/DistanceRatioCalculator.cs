namespace Types{
    public class DistanceRatioCalculator{
        private  readonly Position _size;
        private readonly Position _centerOfMap;
        public double CalculationRatio{ get; set; } = 1.5;
        
        public DistanceRatioCalculator(int xSize, int ySize){
            _centerOfMap = new Position(xSize/2, ySize/2 );
            _size = new Position(xSize,ySize);
        }

        public double GetValue(Position position){
            var tmp = Position.CalculateDistance(Position.Zero, _centerOfMap);
            var distance = tmp - Position.CalculateDistance(position,_centerOfMap);
            return tmp != 0? distance / tmp * CalculationRatio:0 ;
        }
    }
}