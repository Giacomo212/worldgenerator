using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace worldgenerator {
    public class Map {

        private int _width;
        private int _hight;
        // private int _viewBeginningPointerX;
        // private int _viewEndPointerX;
        // private int _viewBeginningPointerY;
        // private int _viewEndPointerY;
        // private int _viewSizeX;
        // private int _viewSizeY;

        public int Width => _width;
        public int Hight => _hight;
        // public int ViewSizeX { get => _viewSizeX; }
        // public int ViewSizeY { get => _viewSizeY; }
        // public int ViewBeginningPointerX { get => _viewBeginningPointerX;}
        // public int ViewEndPointerX { get => _viewEndPointerX; }
        // public int ViewBeginningPointerY { get => _viewBeginningPointerY;}
        // public int ViewEndPointerY { get => _viewEndPointerY;}

        Block[,] _grid;
        public Block this[int x, int y]
        {
            get { return _grid[x, y]; }
        }
        public Map(int width, int hight)
        {
            _grid = new Block[width, hight];
            _width = width;
            _hight = hight;
            CreateNewMap(_width, _hight);
            //setViewSize(viewSizeX, viewsizeY);

        }
        public Map(string filename) {
            Load(filename);
            //setViewSize(viewSizeX, viewsizeY);
        }
        void CreateNewMap(int width, int hight) {
            
            PerlinNoiseGenerator perlinNoiseGenerator = new PerlinNoiseGenerator();
            for (int i = 0; i < Width; i++) {
                for (int j = 0; j < Hight; j++) {
                    _grid[i, j] = PerlinNoseParser( perlinNoiseGenerator, i, j);
                }
            }
        }
        Block PerlinNoseParser(PerlinNoiseGenerator  perlinNoise, int x, int y) {
            var tmp = perlinNoise.getValue(x, y);

            if (tmp <= 0)
                return new Block(0);
            else if (tmp <= 0.5)
                return new Block(1);
            else if (tmp <= 1)
                return new Block(2);
            return null;
        }
        public void Save(string fileName) {
            using (var file = new BinaryWriter(File.Open(fileName, FileMode.Create))) {
                file.Write(Width);
                file.Write(Hight);
                foreach (var t in _grid) {
                    
                    file.Write(t.BlockID);
                }

            }
        }
        public void Load(string FileName) {
            
            using (var file = new BinaryReader(File.Open(FileName, FileMode.Open))) {
                _width = file.ReadInt32();
                _hight = file.ReadInt32();
                _grid = new Block[_width, _hight];
                for (var i = 0; i < _width; i++) 
                    for(var j = 0; j < _hight; j++) 
                        _grid[i, j] = new Block(file.ReadInt32());
                
            }
        }

        public void SaveToImage() {
           
        }
        // public void setViewSize(int xSize, int ySize) {
        //     if(xSize > Width || ySize > Width) {
        //         throw new IndexOutOfRangeException();
        //     }
        //     _viewBeginningPointerX = 0;
        //     _viewBeginningPointerY = 0;
        //     _viewEndPointerX = xSize;
        //     _viewEndPointerY = ySize;
        //     _viewSizeX = xSize;
        //     _viewSizeY = ySize;
        //
        // }
        // public void moveViewLeft() {
        //     if (_viewBeginningPointerX > 0) {
        //         _viewBeginningPointerX--;
        //         _viewEndPointerX--;
        //
        //     }
        // }
        // public void moveViewRight() {
        //     
        //     if (_viewEndPointerX < Width) {
        //         _viewBeginningPointerX++;
        //         _viewEndPointerX++;
        //
        //     }
        // }
        // public void moveViewUp() {
        //     if (_viewBeginningPointerY > 0) {
        //         _viewBeginningPointerY--;
        //         _viewEndPointerY--;
        //
        //     }
        // }
        // public void moveViewDown() {
        //     if (_viewEndPointerY < Hight) {
        //         _viewBeginningPointerY++;
        //         _viewEndPointerY++;
        //
        //     }
        // }
        
    }
}
