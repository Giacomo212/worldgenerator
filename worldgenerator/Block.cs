 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace worldgenerator {
    public class Block {
        public static int Width { get; } = 40;
        public static int High { get; } = 40;
        private int _blockID;
        private Item _item;
        
        public int BlockID {
            get {
                return _blockID;
            }
        }
        public Block(int blockID) {
            _blockID = blockID;
        }
    }
}


