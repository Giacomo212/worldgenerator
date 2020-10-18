namespace Types{
    public class DistanceRatioCalculator{
        private  readonly Position _size;
        private readonly Position _centerOfMap;
        public double CalculationRatio{ get; set; } = 2;
        
        public DistanceRatioCalculator(int xSize, int ySize){
            _centerOfMap = new Position(xSize/2, ySize/2 );
            _size = new Position(xSize,ySize);
        }

        public double GetValue(Position position){
            var distance = _centerOfMap.X - Position.CalculateDistance(position,_centerOfMap);
            return distance / _centerOfMap.X * CalculationRatio  ;
        }
    }
}