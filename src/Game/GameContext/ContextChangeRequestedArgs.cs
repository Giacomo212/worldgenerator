using System;
using Game.GameContext;

namespace Game.GameContext{
    public class ContextChangeRequestedArgs : EventArgs{
        public  readonly Context Context;

        public ContextChangeRequestedArgs(Context context){
            Context = context;
        }
    }
}