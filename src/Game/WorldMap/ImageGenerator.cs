using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Types;


namespace Game.WorldMap{
    public static class ImageGenerator{
        public static void GenerateImage(Map map){
            var dictionary = CrateDictionary();
            var mapSize = (int) map.WorldType;
            using var mapReader = new MapReader(map);
            using var image = new Image<Rgba32>(mapSize, mapSize);
            for (var i = 0; i < map.ChunkCount; i++){
                for (var j = 0; j < map.ChunkCount; j++){
                    var chunk = mapReader.ReadChunkAt(new Position(i, j));
                    for (var k = 0; k < Chunk.BlockCount; k++){
                        for (var l = 0; l < Chunk.BlockCount; l++){
                            dictionary.TryGetValue(chunk[k, l].BlockType, out var tmp);
                            image[Chunk.BlockCount * i + k, l + Chunk.BlockCount * j] = Rgba32.ParseHex(tmp);
                        }
                    }
                }
            }

            image.Save(EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator + map.Name + ".jpg");
        }

        private static Dictionary<BlockType, string> CrateDictionary(){
            var dictionary = new Dictionary<BlockType, string>{
                {BlockType.Dirt, "512a09"},
                {BlockType.Grass, "235a23"},
                {BlockType.Sand, "ffcd30"},
                {BlockType.Snow, "ffffff"},
                {BlockType.Water, "2d56d8"},
                {BlockType.Stone, "717171"}
            };
            return dictionary;
        }
    }
}