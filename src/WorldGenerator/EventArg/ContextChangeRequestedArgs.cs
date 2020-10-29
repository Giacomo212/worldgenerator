using System;
using WorldGenerator.GameContext;

namespace WorldGenerator.EventArg{
    public class ContextChangeRequestedArgs : EventArgs{
        public  readonly Context Context;

        public ContextChangeRequestedArgs(Context context){
            Context = context;
        }
    }
}