using System;
using System.Collections;
using System.IO;
using Types;

namespace Types{
    public class Chunk : IEnumerable{
    private Block[,] _blocks = new Block[Size, Size];
    public const int Size = 16;
    public static readonly int SizeOf = Block.SizeOf * Size * Size;


    public Block this[int i, int j]{
        get => _blocks[i, j];
        set => _blocks[i, j] = value;
    } 

    
    public IEnumerator GetEnumerator(){
        return new ChunkEnum(_blocks);
    }
    }
}