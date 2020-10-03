using System;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Types;


namespace Game.UI{
    public abstract class UserInterface : Grid{
        
        // public UserInterface Interface => _interface;
        
        // public IChangeContext ContextCreator => _ContextCreator;
        protected UserInterface() : base(){
            RowSpacing = 8;
            ColumnSpacing = 8;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }
        

        public event EventHandler OnPreviousUIRequest;
        public event EventHandler<UiChangeRequestArgs> OnNextUIRequest;
        public event EventHandler<ContextChangeRequested> OnContextChangeRequest;

        protected virtual void RequestContext(ContextChangeRequested contextChangeRequested){
            OnContextChangeRequest?.Invoke(this, contextChangeRequested); ;
        }

        protected virtual void RequestPreviousInterface(){
            OnPreviousUIRequest?.Invoke(this,new EventArgs());
        }

        protected virtual void RequestNewInterface(UiChangeRequestArgs uiChangeRequestArgs){
            OnNextUIRequest?.Invoke(this, uiChangeRequestArgs);
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