using WorldGenerator.Configs;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator{
    public class CameraController{
        private readonly Position _renderPosition; //= new Position(0, 0);
        private readonly Map _map;

        private Position _maximalPosition;
        public float VectorX{ get; private set; } = 0;
        public float VectorY{ get; private set; } = 0;


        public CameraController(Map map){
            _maximalPosition = new Position(  map.ChunkCount - (GameConfig.Config.Resolution.Width / Chunk.PixelSize + 2),
                map.ChunkCount - (GameConfig.Config.Resolution.Hight / Chunk.PixelSize + 2));
            _map = map;
            _renderPosition = new Position(_map.ChunkCount / 2, _map.ChunkCount / 2);
        }

        //private methods
        public bool MoveRight(){
            var tmp = VectorX - GameConfig.Config.Sensitivity;
            if (_renderPosition.X < _maximalPosition.X && tmp <= 0){
                _renderPosition.X++;
                VectorX = Chunk.PixelSize;
                return true;
            }

            VectorX = tmp > 0 ? tmp : VectorX;
            return false;
        }

        public bool MoveLeft(){
            var tmp = VectorX + GameConfig.Config.Sensitivity;
            if (_renderPosition.X > 0 && tmp >= Chunk.PixelSize){
                _renderPosition.X--;
                VectorX = 0;
                return true;
            }

            VectorX = tmp < Chunk.PixelSize ? tmp : VectorX;
            return false;
        }

        public bool MoveUp(){
            var tmp = VectorY + GameConfig.Config.Sensitivity;
            if (_renderPosition.Y > 0 && tmp >= Chunk.PixelSize){
                _renderPosition.Y--;
                VectorY = 0;
                return true;
            }

            VectorY = tmp < Chunk.PixelSize ? tmp : VectorY;
            return false;
        }

        public bool MoveDown(){
            var tmp = VectorY - GameConfig.Config.Sensitivity;
            if (_renderPosition.Y < _maximalPosition.Y && tmp <= 0){
                _renderPosition.Y++;
                VectorY = Chunk.PixelSize;
                return true;
            }

            VectorY = tmp > 0 ? tmp : VectorY;
            return false;
        }

        public void ReactOnBufferChange(){
            _maximalPosition = new Position(  _map.ChunkCount - (GameConfig.Config.Resolution.Width / Chunk.PixelSize + 2),
                _map.ChunkCount - (GameConfig.Config.Resolution.Hight / Chunk.PixelSize + 2));
        }
    }
}