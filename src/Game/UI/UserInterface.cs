using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Types;


namespace Game.UI{
    public abstract class UserInterface : Grid{
        protected UserInterface _interface = null;

        public bool IsCanceled{ get; private set; } = false;
        // public UserInterface Interface => _interface;
        protected Context _context = null;

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
            return _context;
        }

        protected static TextButton CrateTextButton(string text, int row, int column){
            return new TextButton{
                Text = text,
                GridRow = row,
                GridColumn = column,
                Width = 150,
                Padding = new Thickness(10),
            };
        }
    }
}