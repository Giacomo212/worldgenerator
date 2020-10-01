using Myra.Graphics2D.UI;


namespace Game.UI{
    public abstract class UserInterface : Grid{
        protected UserInterface _interface = null;

        // public UserInterface Interface => _interface;
        protected Context StartUiContext = null;

        // public IChangeContext ContextCreator => _ContextCreator;
        protected UserInterface() : base(){
            RowSpacing = 8;
            ColumnSpacing = 8;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
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