using Myra.Graphics2D;
using Myra.Graphics2D.UI;

namespace WorldGenerator.UI{
    public class ConfirmDialog : UserInterface{

        public ConfirmDialog(string s){
            var label = new Label(){
                Text = s,
                GridRow = 0,
                GridColumn = 0,
            };
            var okButton = CrateTextButton("Apply", 0, 0);
            var cancelButton = CrateBackButton( 0, 1);
            var buttonGrid = new Grid(){
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                GridRow = 1,
                GridColumn = 0,
                Padding = new Thickness(10)
            };
            buttonGrid.Widgets.Add(okButton);
            buttonGrid.Widgets.Add(cancelButton);
            _widgets.Add(buttonGrid);
            _widgets.Add(label);
            
        }
        
    }
}