using System;
using System.Collections;
using System.IO;
using Types;

namespace Types{
    public class Chunk : IEnumerable{
    private Block[,] _blocks = new Block[Size, Size];
    private Position _position;

    public Position Position => _position;

    public static readonly int Size = 16;
    public static int SizeOf{ get; } = Block.SizeOf * Size * Size; 
    public Chunk(Position position){
        _position = position;
    }

    public Block this[int i, int j] => _blocks[i, j];

    

    public void Save(ref BinaryWriter binaryWriter){
        foreach (var block in _blocks){
            //binaryWriter.Write();
        }
    }


    public IEnumerator GetEnumerator(){
        return new ChunkEnum(_blocks);
    }
    }
}