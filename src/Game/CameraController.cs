using System;
using System.IO;
using Game.Configs;
using Types;


namespace Game{
    public class CameraController{
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
        
    }
}