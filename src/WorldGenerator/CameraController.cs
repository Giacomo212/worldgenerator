using WorldGenerator.Configs;
using WorldGenerator.Utils;
using WorldGenerator.WorldMap;

namespace WorldGenerator{
    public class CameraController{
        //vectors which are responsible for map shift
        private Position _renderPosition = new Position(0, 0);
        private readonly Map _map;
        
        // propertes
        public float VectorX{ get; private set; } = 0;

        public float VectorY{ get; private set; } = 0;


        public CameraController(Map map){
            _map = map;
        }

        //private methods
        public bool MoveRight(){
            var tmp = VectorX - GameConfig.Config.Sensivity;
            if (_renderPosition.X < _map.ChunkCount && tmp <= 0){
                _renderPosition.X++;
                VectorX = Chunk.PixelSize;
                return true;
            }
            VectorX = tmp > 0 ? tmp : VectorX;
            return false;
        }

        public bool MoveLeft(){
            var tmp = VectorX + GameConfig.Config.Sensivity;
            if (_renderPosition.X > 0 && tmp >= Chunk.PixelSize){
                _renderPosition.X--;
                VectorX = 0;
                return true;
            }
            VectorX = tmp < Chunk.PixelSize ? tmp : VectorX;
            return false;
        }

        public bool MoveUp(){
            var tmp = VectorY + GameConfig.Config.Sensivity;
            if (_renderPosition.Y > 0 && tmp >= Chunk.PixelSize){
                _renderPosition.Y--;
                VectorY = 0;
                return true;
            }
            VectorY = tmp < Chunk.PixelSize ? tmp : VectorY;
            return false;
        }

        public bool MoveDown(){
            var tmp = VectorY - GameConfig.Config.Sensivity;
            if (_renderPosition.Y < _map.ChunkCount && tmp <= 0){
                _renderPosition.Y++;
                VectorY = Chunk.PixelSize;
                return true;
            }
            VectorY = tmp > 0 ? tmp : VectorY;
            return false;
        }
        
    }
}