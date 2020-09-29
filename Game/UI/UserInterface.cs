using Myra.Graphics2D.UI;


namespace Game.UI{
    public abstract class UserInterface : Desktop{
        protected UserInterface _interface = null;

        // public UserInterface Interface => _interface;
        protected Context StartUiContext = null;
        // public IChangeContext ContextCreator => _ContextCreator;
        protected UserInterface() : base(){
            
        }

        public UserInterface CreateNewUI(){
            var tmp = _interface;
            _interface = null;
            return tmp;
        }

        public Context CrateNewContext(){
            return StartUiContext;
        }
    }
}