using System;
using System.IO;
using Types;


namespace Game{
    public class CameraController{
        //values of the class
        //vectors which are responsible for map shift
        private float _vectorX = 0;
        private float _vectorY = 0;
        private Position _renderPosition = new Position(0, 0);
        private readonly Map _map;
        
        // propertes
        public float VectorX => _vectorX;
        public float VectorY => _vectorY;


        public CameraController(Map map){
            _map = map;
        }

        //private methods
        public bool MoveRight(){
            var tmp = _vectorX - GameConfig.Config.Sensivity;
            if (_renderPosition.X < _map.ChunkCount && tmp <= 0){
                _renderPosition.X++;
                _vectorX = Chunk.PixelSize;
                return true;
            }
            _vectorX = tmp > 0 ? tmp : _vectorX;
            return false;
        }

        public bool MoveLeft(){
            var tmp = _vectorX + GameConfig.Config.Sensivity;
            if (_renderPosition.X > 0 && tmp >= Chunk.PixelSize){
                _renderPosition.X--;
                _vectorX = 0;
                return true;
            }
            _vectorX = tmp < Chunk.PixelSize ? tmp : _vectorX;
            return false;
        }

        public bool MoveUp(){
            var tmp = _vectorY + GameConfig.Config.Sensivity;
            if (_renderPosition.Y > 0 && tmp >= Chunk.PixelSize){
                _renderPosition.Y--;
                _vectorY = 0;
                return true;
            }
            _vectorY = tmp < Chunk.PixelSize ? tmp : _vectorY;
            return false;
        }

        public bool MoveDown(){
            var tmp = _vectorY - GameConfig.Config.Sensivity;
            if (_renderPosition.Y < _map.ChunkCount && tmp <= 0){
                _renderPosition.Y++;
                _vectorY = Chunk.PixelSize;
                return true;
            }
            _vectorY = tmp > 0 ? tmp : _vectorY;
            return false;
        }


        // private void MoveVectorX(float vector){
        //     _vectorX += vector;
        //     if (_vectorX < 0  ){
        //         _vectorX = Block.Size;
        //         MoveMapRight();
        //     } 
        //     if (_vectorX > Block.Size){
        //         _vectorX = 0;
        //         MoveMapLeft();
        //     }
        //     
        // }
        // private void MoveVectorY(float vector){
        //     _vectorY += vector;
        //     if (_vectorY < 0  ){
        //         _vectorY = Block.Size ;
        //         MoveMapDown();
        //     } 
        //     if (_vectorY > Block.Size){
        //         _vectorY = 0;
        //         MoveMapUp();
        //     }
        // }
        // private void InitializeViewSize(int width, int hight) {
        //     if(width > _mapWidth || hight > _mapHight) {
        //         throw new IndexOutOfRangeException();
        //     }
        //     _viewBeginningPointerX = 0;
        //     _viewBeginningPointerY = 0;
        //     _viewEndPointerX = width;
        //     _viewEndPointerY = hight;
        //
        // }
        // private void MoveMapLeft() {
        //     if (_viewBeginningPointerX > 0) {
        //         _viewBeginningPointerX--;
        //         _viewEndPointerX--;
        //
        //     }
        // }
        // private void MoveMapRight() {
        //     
        //     if (_viewEndPointerX < _mapWidth) {
        //         _viewBeginningPointerX++;
        //         _viewEndPointerX++;
        //
        //     }
        // }
        // private void MoveMapUp() {
        //     if (_viewBeginningPointerY > 0) {
        //         _viewBeginningPointerY--;
        //         _viewEndPointerY--;
        //
        //     }
        // }
        // private void MoveMapDown() {
        //     if (_viewEndPointerY < _mapHight) {
        //         _viewBeginningPointerY++;
        //         _viewEndPointerY++;
        //     }
        // }
        //public methods
        // public void MoveRight(){
        //     if (_vectorX - GameConfig.Config.Sensivity < 0 && ViewEndPointerX == _mapWidth)
        //         _vectorX = 0;
        //     else
        //         MoveVectorX(-GameConfig.Config.Sensivity);
        // }
        // public void MoveLeft(){
        //     if (_vectorX + GameConfig.Config.Sensivity > Block.Size && ViewBeginningPointerX == 0)
        //         _vectorX = Block.Size;
        //     else
        //         MoveVectorX(GameConfig.Config.Sensivity);
        // }
        //
        // public void MoveUp(){
        //     if (_vectorY + GameConfig.Config.Sensivity > Block.Size && ViewBeginningPointerY == 0)
        //         _vectorY = Block.Size;
        //     else
        //         MoveVectorY(GameConfig.Config.Sensivity);
        // }
        //
        // public void MoveDown(){
        //     if (_vectorY - GameConfig.Config.Sensivity < 0 && ViewEndPointerY == _mapHight)
        //         _vectorY = 0;
        //     else
        //         MoveVectorY(-GameConfig.Config.Sensivity);
        // }
    }
}