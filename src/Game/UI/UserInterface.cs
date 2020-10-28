using System;
using Game.EventArg;
using Game.GameContext;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;



namespace Game.UI{
    public abstract class UserInterface : Grid{
        public bool CanBeCanceled{ get; protected set; } = true;
        protected UserInterface() : base(){
            RowSpacing = 8;
            ColumnSpacing = 8;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public event EventHandler RequestFullScreen;
        public event EventHandler PreviousUIRequest;
        public event EventHandler<UiChangeRequestArgs> PreviousUIAndLoadRequest;
        public event EventHandler<UiChangeRequestArgs> NextUIRequest;
        public event EventHandler<ContextChangeRequestedArgs> ContextChangeRequest;
        public event EventHandler ExitRequest;

        protected virtual void RequestContext(ContextChangeRequestedArgs contextChangeRequestedArgs){
            ContextChangeRequest?.Invoke(this, contextChangeRequestedArgs); ;
        }

        protected virtual void RequestPreviousInterface(){
            PreviousUIRequest?.Invoke(this,new EventArgs());
        }

        protected virtual void RequestNewInterface(UiChangeRequestArgs uiChangeRequestArgs){
            NextUIRequest?.Invoke(this, uiChangeRequestArgs);
        }
        protected virtual void OnExitRequest(){
            ExitRequest?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnRequestFullScreen(){
            RequestFullScreen?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnPreviousUiAndLoadRequest(UiChangeRequestArgs e){
            PreviousUIAndLoadRequest?.Invoke(this, e);
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
        protected static TextButton CrateBackButton(int row, int column){
            var a = new TextButton{
                Text = "Go back",
                GridRow = row,
                GridColumn = column,
                Width = 150,
                Padding = new Thickness(10),
            };
           
            return a;
        }

        protected static Label CrateLabel(string text, int row, int column){
            return new Label(){
                Text = text,
                GridRow = row,
                GridColumn = column,
                Width = 150,
                Padding = new Thickness(10),
            };
        }
        public virtual void Update(GameTime gameTime){
            
        }

        public virtual void Awake(){
            
        }

        
    }
}