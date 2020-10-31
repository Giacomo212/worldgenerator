using System;
using System.Collections;

namespace WorldGenerator.MapElements{
    public class ChunkEnumerator : IEnumerator {
        public int I{ get; private set; } = -1;

        public int J{ get; private set; } = 0;

        private Block[,] _blocks;
        public ChunkEnumerator(Block[,] blocks){
            _blocks = blocks;
        }
        public bool MoveNext(){
            if (++I >= Chunk.BlockCount ){
                I = 0;
                ++J;
            }
            return J < Chunk.BlockCount;
        }

        public void Reset(){
            I = -1;
            J = 0;
        }

        object IEnumerator.Current => Current;

        public Block Current
        {
            get
            {
                try
                {
                    return _blocks[I,J];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose(){
            
        }
    }
}