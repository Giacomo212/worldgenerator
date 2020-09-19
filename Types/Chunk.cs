using System;
using System.Collections;
using System.IO;
using Types;

namespace Types{
    public class Chunk : IEnumerable{
    private Block[,] _blocks = new Block[Size, Size];
    
    public static readonly int Size = 16;
    public static int SizeOf{ get; } = Block.SizeOf * Size * Size;


    public Block this[int i, int j]{
        get => _blocks[i, j];
        set => _blocks[i, j] = value;
    } 

    
    public IEnumerator GetEnumerator(){
        return new ChunkEnum(_blocks);
    }
    }
}