using System;
using System.Collections;

namespace Game.DataContainers{
    public class ChunkEnum : IEnumerator {
        public int I{ get; private set; } = -1;

        public int J{ get; private set; } = 0;

        private Game.DataContainers.Block[,] _blocks;
        public ChunkEnum(Game.DataContainers.Block[,] blocks){
            _blocks = blocks;
        }
        public bool MoveNext(){
            if (++I >= Game.DataContainers.Chunk.BlockCount ){
                I = 0;
                ++J;
            }
            return J < Game.DataContainers.Chunk.BlockCount;
        }

        public void Reset(){
            I = -1;
            J = 0;
        }

        object IEnumerator.Current => Current;

        public Game.DataContainers.Block Current
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