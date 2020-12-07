using System;
using WorldGenerator.GameScreen;

namespace WorldGenerator.EventArg{
    public class ContextChangeRequestedArgs : EventArgs{
        public  readonly Screen Screen;

        public ContextChangeRequestedArgs(Screen screen){
            Screen = screen;
        }
    }
}