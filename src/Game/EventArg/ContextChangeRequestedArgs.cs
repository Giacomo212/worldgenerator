using System;
using Game.GameContext;

namespace Game.EventArg{
    public class ContextChangeRequestedArgs : EventArgs{
        public  readonly Context Context;

        public ContextChangeRequestedArgs(Context context){
            Context = context;
        }
    }
}