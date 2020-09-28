using Myra.Graphics2D.UI;


namespace Game.UI{
    public abstract class UserInterface : Desktop{
        private Context _context = null;
        public Context Context => _context;
        protected UserInterface() : base(){
            
        }
    }
}