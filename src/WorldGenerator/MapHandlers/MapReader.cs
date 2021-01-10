using System;
using System.IO;
using Force.Crc32;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public class MapReader : IDisposable{
        private FileStream _fileStream;
        private BinaryReader _binaryReader;
        private Map _map;
        private readonly int _sizeofMap;

        public MapReader(Map map){
            _fileStream = File.Open(EnvironmentVariables.WorldFiles + EnvironmentVariables.Separator + map.Name + ".wg",
                FileMode.Open);
            _map = map;
            _fileStream.Seek(sizeof(uint) + sizeof(int) * 2, SeekOrigin.Begin);
            _binaryReader = new BinaryReader(_fileStream);
            _sizeofMap = Chunk.MemorySize * (int) _map.WorldType / Chunk.BlockCount;
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
            _fileStream.Seek(sizeof(uint) +
                sizeof(int) * 2 + position.Y * Chunk.MemorySize + position.X * _sizeofMap,
                SeekOrigin.Begin);
            return ReadChunk();
        }

        private Block ReadBlock(){
            return new Block(
                (BlockType) _binaryReader.ReadInt32(),
                (BiomeType) _binaryReader.ReadInt32(),
                (ItemType) _binaryReader.ReadInt32());
        }

        public static Map ReadMap(string mapName){
            if (mapName.Contains("/") || mapName.Contains("\\"))
                throw new IOException("invalid file name");
            using var file = new BinaryReader(File.Open(
                EnvironmentVariables.WorldFiles + EnvironmentVariables.Separator + mapName + ".wg", FileMode.Open));
            file.ReadUInt32();
            var seed = file.ReadInt32();
            var worldSize = (WorldSize) file.ReadInt32();
            //file.Close(); this shouldn't  be needed, dispose() is called at the end of the brackets 
            return new Map(mapName, worldSize, seed);
        }

        public static bool CheckMapIntegrity(string mapName){
            if (mapName.Contains("/") || mapName.Contains("\\"))
                throw new IOException("invalid file name");
           
            using var fs = File.Open(
                EnvironmentVariables.WorldFiles + EnvironmentVariables.Separator + mapName + ".wg", FileMode.Open);
            using var fileReader = new BinaryReader(fs);
            var savedCheckSum = fileReader.ReadUInt32();
            var memorySteam = new MemoryStream();
            fs.CopyTo(memorySteam);
            var actualCheckSum = Crc32Algorithm.Compute(memorySteam.ToArray());
            return actualCheckSum == savedCheckSum;
        } 

        public void Dispose(){
            _fileStream?.Dispose();
            _binaryReader?.Dispose();
        }
    }
}