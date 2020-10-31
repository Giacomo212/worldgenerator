using System.Drawing;

namespace WorldGenerator.Utils{
    public class DistanceRatioCalculator{
        private readonly Position _beginning;

        public Position CenterOfCalculator{ get; }

        private  readonly Position _size;
        public double CalculationRatio{ get; set; } = 1.5;
        private readonly double _maxDistance;
        
        public DistanceRatioCalculator(int xSize, int ySize){
            _beginning = Position.Zero;
            CenterOfCalculator = new Position(xSize/2, ySize/2 );
            _size = new Position(xSize,ySize);
            _maxDistance = Position.CalculateDistance(_size, CenterOfCalculator);
        }
        public DistanceRatioCalculator(Position beginningPosition, Position size){
            _beginning = beginningPosition;
            CenterOfCalculator = new Position(beginningPosition.X + size.X/2,beginningPosition.Y + size.Y/2 );
            _size = size;
            _maxDistance = Position.CalculateDistance(_size, CenterOfCalculator);
        }

        public double GetValue(Position position){
            // if (CheckIfOutOfRange(position))
            //     return ;
            var tmp = Position.CalculateDistance(_beginning, CenterOfCalculator);
            var distance = tmp - Position.CalculateDistance(position,CenterOfCalculator);
            return tmp != 0? distance / tmp * CalculationRatio:0 ;
        }

        private bool CheckIfOutOfRange(Position position){
            return Position.CalculateDistance(position, CenterOfCalculator) > _maxDistance;
        }
    }
}