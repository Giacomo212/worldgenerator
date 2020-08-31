using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace worldgenerator{
    public class CameraController{
        //values of the class
        private float _vectorX = 0;
        private float _vectorY = 0;
        //view variable represent the map with is seen on screen
        // 
        private int _viewBeginningPointerX;
        private int _viewBeginningPointerY;
        private int _viewEndPointerX;
        private int _viewEndPointerY;
        private int _MapWidth;
        private int _MapHight;
        
        // propertes
        public float VectorX => _vectorX;
        public float VectorY => _vectorY;
        public int ViewBeginningPointerX => _viewBeginningPointerX;
        public int ViewBeginningPointerY => _viewBeginningPointerY;
        public int ViewEndPointerX => _viewEndPointerX;
        public int ViewEndPointerY => _viewEndPointerY;
        
        //constructor
        public CameraController(int mapWidth, int mapHight, int viewWidth, int viewHight){
            if(mapHight < 0 || mapWidth < 0)
                throw new InvalidDataException();
            _MapHight = mapHight;
            _MapWidth = mapWidth;
            ChangeCameraViewSize(viewWidth,viewHight);
        }
        
        //private methods
        private void MoveVectorX(float vector){
            _vectorX += vector;
            if (_vectorX < 0  ){
                _vectorX = Block.Width;
                MoveMapRight();
            } 
            if (_vectorX > Block.Width){
                _vectorX = 0;
                MoveMapLeft();
            }
            
        }
        private void MoveVectorY(float vector){
            _vectorY += vector;
            if (_vectorY < 0  ){
                _vectorY = Block.High ;
                MoveMapDown();
            } 
            if (_vectorY > Block.High){
                _vectorY = 0;
                MoveMapUp();
            }
        }
        private void ChangeCameraViewSize(int width, int hight) {
            if(width > _MapWidth || hight > _MapHight) {
                throw new IndexOutOfRangeException();
            }
            _viewBeginningPointerX = 0;
            _viewBeginningPointerY = 0;
            _viewEndPointerX = width - 1;
            _viewEndPointerY = hight - 1 ;
            // _viewSizeX = xSize;
            // _viewSizeY = ySize;

        }
        private void MoveMapLeft() {
            if (_viewBeginningPointerX > 0) {
                _viewBeginningPointerX--;
                _viewEndPointerX--;

            }
        }
        private void MoveMapRight() {
            
            if (_viewEndPointerX < _MapWidth) {
                _viewBeginningPointerX++;
                _viewEndPointerX++;

            }
        }
        private void MoveMapUp() {
            if (_viewBeginningPointerY > 0) {
                _viewBeginningPointerY--;
                _viewEndPointerY--;

            }
        }
        private void MoveMapDown() {
            if (_viewEndPointerY < _MapHight) {
                _viewBeginningPointerY++;
                _viewEndPointerY++;
            }
        }
        //public methods
        public void MoveRight(){
            if (_vectorX - GameConfig.Config.Sensivity < 0 && ViewEndPointerX == _MapWidth)
                _vectorX = 0;
            else
                MoveVectorX(-GameConfig.Config.Sensivity);
        }
        public void MoveLeft(){
            if (_vectorX + GameConfig.Config.Sensivity > Block.Width && ViewBeginningPointerX == 0)
                _vectorX = Block.Width;
            else
                MoveVectorX(GameConfig.Config.Sensivity);
        }

        public void MoveUp(){
            if (_vectorY + GameConfig.Config.Sensivity > Block.High && ViewBeginningPointerY == 0)
                _vectorY = Block.High;
            else
                MoveVectorY(GameConfig.Config.Sensivity);
        }

        public void MoveDown(){
            if (_vectorY - GameConfig.Config.Sensivity < 0 && ViewEndPointerY == _MapHight)
                _vectorY = 0;
            else
                MoveVectorY(-GameConfig.Config.Sensivity);
        }
    }
}