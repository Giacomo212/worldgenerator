using System;
using System.IO;

using Types;


namespace Game{
    public class CameraController{
        //values of the class
        //vectors which are responsible for map shift
        private float _vectorX = 0;
        private float _vectorY = 0;
        //view variable represent the map which part of map is seen on the screen
        // 
        private int _viewBeginningPointerX;
        private int _viewBeginningPointerY;
        private int _viewEndPointerX;
        private int _viewEndPointerY;
        //maximal values of pointers
        private int _mapWidth;
        private int _mapHight;
        
        // propertes
        public float VectorX => _vectorX;
        public float VectorY => _vectorY;
        public int ViewBeginningPointerX => _viewBeginningPointerX;
        public int ViewBeginningPointerY => _viewBeginningPointerY;
        public int ViewEndPointerX => _viewEndPointerX;
        public int ViewEndPointerY => _viewEndPointerY;
        public int XSize => _viewEndPointerX - _viewBeginningPointerX;
        public int YSize => _viewEndPointerX - _viewBeginningPointerX;
        
        
        //constructor
        public CameraController(int mapWidth, int mapHight, int viewWidth, int viewHight){
            if(mapHight < 0 || mapWidth < 0)
                throw new InvalidDataException();
            _mapHight = mapHight;
            _mapWidth = mapWidth;
            InitializeViewSize(viewWidth,viewHight);
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
        private void InitializeViewSize(int width, int hight) {
            if(width > _mapWidth || hight > _mapHight) {
                throw new IndexOutOfRangeException();
            }
            _viewBeginningPointerX = 0;
            _viewBeginningPointerY = 0;
            _viewEndPointerX = width;
            _viewEndPointerY = hight;

        }
        private void MoveMapLeft() {
            if (_viewBeginningPointerX > 0) {
                _viewBeginningPointerX--;
                _viewEndPointerX--;

            }
        }
        private void MoveMapRight() {
            
            if (_viewEndPointerX < _mapWidth) {
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
            if (_viewEndPointerY < _mapHight) {
                _viewBeginningPointerY++;
                _viewEndPointerY++;
            }
        }
        //public methods
        public void MoveRight(){
            if (_vectorX - GameConfig.Config.Sensivity < 0 && ViewEndPointerX == _mapWidth)
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
            if (_vectorY - GameConfig.Config.Sensivity < 0 && ViewEndPointerY == _mapHight)
                _vectorY = 0;
            else
                MoveVectorY(-GameConfig.Config.Sensivity);
        }
    }
}