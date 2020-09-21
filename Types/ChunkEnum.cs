using System;
using System.Collections;

namespace Types{
    public class ChunkEnum : IEnumerator {
        private int _i = -1, _j = 0;
        private Block[,] _blocks;
        public ChunkEnum(Block[,] blocks){
            _blocks = blocks;
        }
        public bool MoveNext(){
            if (++_i >= Chunk.Size ){
                _i = 0;
                ++_j;
            }
            return _j < Chunk.Size;
        }

        public void Reset(){
            _i = -1;
            _j = 0;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Block Current
        {
            get
            {
                try
                {
                    return _blocks[_i,_j];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}