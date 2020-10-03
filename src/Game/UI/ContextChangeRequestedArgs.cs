using System;
using Game.GameContext;

namespace Game.UI{
    public class ContextChangeRequested : EventArgs{
        public Context Context;

        public ContextChangeRequested(Context context){
            Context = context;
        }
    }
}