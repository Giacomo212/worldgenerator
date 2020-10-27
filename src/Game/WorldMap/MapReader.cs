using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Types;

namespace Game.WorldMap{
    public class MapReader : IDisposable{
        private FileStream _fileStream;
        private MemoryMappedFile _mappedFile;
        private BinaryReader _binaryReader;
        private Types.Map _map;
        private  readonly int _sizeofMap;
        public MapReader( Types.Map map){
            
            _fileStream = File.Open(EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator + map.Name + ".wg",
                FileMode.Open);
            _map = map;
            _fileStream.Seek(sizeof(int) * 2 , SeekOrigin.Begin);
            _binaryReader = new BinaryReader(_fileStream);
            _sizeofMap = Chunk.SizeOf * (int) _map.WorldType / Chunk.BlockCount;
        }

        private Chunk ReadChunk(){
            var chunk = new Chunk();
            for (var i = 0; i < Chunk.BlockCount; i++){
                for (var j = 0; j < Chunk.BlockCount; j++){
                    chunk[i, j] = ReadBlock();
                }
            }

            return chunk;
        }

        public Chunk ReadChunkAt(Position position){
            _fileStream.Seek(sizeof(int)*2 + position.Y * Chunk.SizeOf  + position.X * _sizeofMap,SeekOrigin.Begin) ;
            return ReadChunk();
        }

        private Block ReadBlock(){
            
            return new Block(
                (BlockType) _binaryReader.ReadInt32(), 
                (BiomeType) _binaryReader.ReadInt32(),
                (ItemType) _binaryReader.ReadInt32());
            
        }

        public static Types.Map ReadMap(string mapName){
            if (mapName.Contains("/") || mapName.Contains("\\"))
                throw new IOException("invalid file name");
            using var file = new BinaryReader(File.Open(EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator  + mapName + ".wg", FileMode.Open));
            var seed = file.ReadInt32();
            WorldSize worldSize = (WorldSize)file.ReadInt32();
            //file.Close(); this shouldn't  be needed, dispose() is called at the end of the brackets 
            return new Types.Map(mapName, worldSize,seed);
        }

        public void Dispose(){
            _fileStream.Close();
            _fileStream?.Dispose();
            _binaryReader?.Dispose();
        }
    }
}