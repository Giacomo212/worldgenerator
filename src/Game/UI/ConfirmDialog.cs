using Myra.Graphics2D.UI;

namespace Game.UI{
    public class ConfirmDialog : UserInterface{

        public ConfirmDialog(string s){
            var label = new Label(){
                Text = s,
                GridRow = 0,
                GridColumn = 0,
            };
            var okButton = CrateTextButton("ok", 0, 0);
            var cancelButton = CrateTextButton("Cancel", 0, 1);
            var buttonGrid = new Grid(){
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                GridRow = 1,
                GridColumn = 0,
            };
            buttonGrid.Widgets.Add(okButton);
            buttonGrid.Widgets.Add(cancelButton);
            _widgets.Add(buttonGrid);
            _widgets.Add(label);
        }
        
    }
}