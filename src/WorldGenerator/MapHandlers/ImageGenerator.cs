using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using WorldGenerator.MapElements;
using WorldGenerator.Utils;

namespace WorldGenerator.MapHandlers{
    public  class ImageGenerator{
        private Map _map;
        public int PercentDone{ get; private set; } = 0;
        public ImageGenerator(Map map){
            _map = map;
        }
        
        public void GenerateImage(){
            var dictionary = CrateDictionary();
            var mapSize = (int) _map.WorldType;
            using var mapReader = new MapReader(_map);
            using var image = new Image<Rgba32>(mapSize, mapSize);
            for (var i = 0; i < _map.ChunkCount; i++){
                for (var j = 0; j < _map.ChunkCount; j++){
                    var chunk = mapReader.ReadChunkAt(new Position(i, j));
                    for (var k = 0; k < Chunk.BlockCount; k++){
                        for (var l = 0; l < Chunk.BlockCount; l++){
                            dictionary.TryGetValue(chunk[k, l].BlockType, out var tmp);
                            image[Chunk.BlockCount * i + k, l + Chunk.BlockCount * j] = Rgba32.ParseHex(tmp);
                        }
                    }
                }
                PercentDone =  (int)(Convert.ToDouble(i)/ _map.ChunkCount * 100.0);
            }

            image.Save(EnvironmentVariables.WorldFiles + EnvironmentVariables.Separator + _map.Name + ".png");
        }

        private static Dictionary<BlockType, string> CrateDictionary(){
            var dictionary = new Dictionary<BlockType, string>{
                {BlockType.Dirt, "785446"},
                {BlockType.Grass, "88c047"},
                {BlockType.Sand, "ffe72f"},
                {BlockType.Snow, "ffffff"},
                {BlockType.Water, "0099f3"},
                {BlockType.Stone, "9d9d9d"}
            };
            return dictionary;
        }
    }
}