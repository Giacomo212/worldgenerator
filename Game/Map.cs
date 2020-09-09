using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace worldgenerator {
    public class Map{
        public readonly string Name;
        private int _width;
        private int _hight;
        public int Width => _width;
        public int Hight => _hight;
        
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

        }
        public Map(string filename) {
            Load(filename);
            
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
            var separator = Path.DirectorySeparatorChar;
            
            if(fileName.Contains("/") || fileName.Contains("\\"))
                throw new IOException("invalid file name");
            fileName = GameConfig.Config.GameFilesPath + separator + $"worlds{separator}" + fileName; 
            using (var file = new BinaryWriter(File.Open(fileName + ".wg", FileMode.Create))) {
                file.Write(Width);
                file.Write(Hight);
                foreach (var t in _grid) {
                    
                    file.Write(t.BlockID);
                }

            }
        }
        public void Load(string fileName) {
            var separator = Path.DirectorySeparatorChar;

            if (fileName.Contains("/") || fileName.Contains("\\"))
                throw new IOException("invalid file name");
            fileName = GameConfig.Config.GameFilesPath + separator + $"worlds{separator}" + fileName;
            using (var file = new BinaryReader(File.Open(fileName, FileMode.Open))) {
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
       
        
    }
}
