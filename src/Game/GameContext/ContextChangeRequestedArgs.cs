using System;
using Game.GameContext;

namespace Game.GameContext{
    public class ContextChangeRequested : EventArgs{
        public  readonly Context Context;

        public ContextChangeRequested(Context context){
            Context = context;
        }
    }
}