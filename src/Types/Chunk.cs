using System;
using System.Collections;
using System.IO;
using Types;

namespace Types{
    public class Chunk : IEnumerable{
    private Block[,] _blocks = new Block[BlockCount, BlockCount];
    public const int BlockCount = 16;
    public static readonly int PixelSize = BlockCount*Block.PixelSize;
    public static readonly int SizeOf = Block.SizeInMemory * BlockCount * BlockCount;


    public Block this[int i, int j]{
        get => _blocks[i, j];
        set => _blocks[i, j] = value;
    } 

    
    public IEnumerator GetEnumerator(){
        return new ChunkEnum(_blocks);
    }
    }
}