using Types;

namespace Game.WorldMap{
    public class IslandWorldGenerator : ChunkGenerator{
        
        private Position _islandCenter = new Position(50,50);
        private int _islandDiameter = 40;
        
        private Chunk _waterChunk;
        public IslandWorldGenerator(Map map) : base(map){
            _waterChunk = new Chunk();
            for (int i = 0; i < Chunk.Size; i++){
                for (int j = 0; j < Chunk.Size; j++){
                    _waterChunk[i,j] = new Block(BlockType.Water,BiomeType.Ocean);
                }
                
            }
        }

        protected override void GenerateWorld(Position position){
            var pos = new Position(position.X/Chunk.Size,position.Y/Chunk.Size);
            if (Position.CalculateDistance(_islandCenter, pos) > _islandDiameter){
                _chunk = _waterChunk;
                return;
            }
            
            for (var x = 0; x < Chunk.Size; x++){
                for (var y = 0; y < Chunk.Size; y++){
                    var tmp = _MainNoise.GetNoise(x + position.X, y + position.Y) +
                              0.75f * _ScondaryNoise.GetNoise((x + position.X), (y + position.Y)) +
                              0.25 * _noise.GetNoise((x + position.X), (y + position.Y));
                    ;
                    if (tmp < -0.4f)
                        _chunk[x, y] = new Block(BlockType.Grass, BiomeType.Grassland);
                    else
                        _chunk[x, y] = new Block(BlockType.Water, BiomeType.Ocean);
                }
            }
        }
    }
}