using System.IO;
using Libraries;
using Types;

namespace Generator {
    public class SurfaceMap{
        // public readonly string Name;
        // public readonly string filename; 
        private int _width;
        private int _hight;
        private WorldSize _worldType;
        public WorldSize WorldType => _worldType;

        //private Chunk _Currentchunk;
        public int Width => _width;
        public int Hight => _hight;

        
        private Block[,] _grid;
        public Block this[int x, int y] => _grid[x, y];

        public SurfaceMap(WorldSize worldWorldType){
            _worldType = worldWorldType;
            var size = (int) worldWorldType;
            _grid = new Block[size,size];
            _width = size;
            _hight = size;
            CreateNewMap();

        }
        public SurfaceMap(string filename) {
            Load(filename);
            
        }
        private void CreateNewMap() {
            //PerlinNoiseGenerator perlinNoiseGenerator = new PerlinNoiseGenerator();
            var mapGenerator = new MapGenerator();
            for (int i = 0; i < Width; i++) {
                for (int j = 0; j < Hight; j++){
                    _grid[i, j] = mapGenerator.GetBlock(i,j);
                }
            }
        }
        
        public void Save(string fileName) {
            var separator = Path.DirectorySeparatorChar;
            
            if(fileName.Contains("/") || fileName.Contains("\\"))
                throw new IOException("invalid file name");
            fileName = EnvironmentVariables.GameFiles + $"{separator}worlds{separator}" + fileName;
            using var file = new BinaryWriter(File.Open(fileName + ".wg", FileMode.Create));
            file.Write(Width);
            file.Write(Hight);
            foreach (var t in _grid) {
                file.Write((int)t.BlockType);
                file.Write((int)t.ItemType);
                file.Write((int)t.BiomeType);
            }
        }
        private void Load(string fileName) {
            var separator = Path.DirectorySeparatorChar;

            if (fileName.Contains("/") || fileName.Contains("\\"))
                throw new IOException("invalid file name");
            fileName = EnvironmentVariables.GameFiles + separator + $"worlds{separator}" + fileName;
            using (var file = new BinaryReader(File.Open(fileName, FileMode.Open))) {
                _width = file.ReadInt32();
                _hight = file.ReadInt32();
                _grid = new Block[_width, _hight];
                for (var i = 0; i < _width; i++) 
                    for(var j = 0; j < _hight; j++) 
                        _grid[i, j] = new Block((BlockType)file.ReadInt32(),(ItemType)file.ReadInt32(),(BiomeType)file.ReadInt32());
                
            }
        }

        public void SaveToImage() {
           
        }
       
        
    }
}
