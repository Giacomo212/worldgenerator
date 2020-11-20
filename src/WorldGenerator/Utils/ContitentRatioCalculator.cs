using WorldGenerator.MapElements;

namespace WorldGenerator.Utils{
    public class ContinentRatioCalculator{
        private readonly Map _map;
        private readonly Position _firstContinent;
        private readonly Position _secondContinent;
        private int _continentRadius;
        public ContinentRatioCalculator(Map map, Position firstContinent, Position secondContinent){
            _map = map;
            _continentRadius = _map.BlockCount / 8;
            _firstContinent = firstContinent;
            _secondContinent = secondContinent;
        }

        //returns positive value close to 0 if blockPosition is close to 
        public double GetValue(Position blockPosition, double noiseValue){
            
            if (Position.CalculateDistance(blockPosition, _firstContinent) <
                Position.CalculateDistance(blockPosition, _secondContinent))
                return CalculateRatio(blockPosition, _firstContinent,noiseValue);
            return CalculateRatio(blockPosition, _secondContinent,noiseValue);
        }

        private double CalculateRatio(Position blockPosition, Position continentPosition, double noiseValue){
            if (noiseValue < 0 && Position.CalculateDistance(blockPosition,continentPosition) < _continentRadius){
                noiseValue *= -1;
            }

            return noiseValue;
        }
    }
}