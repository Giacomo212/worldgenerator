using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using WorldGenerator.GameScreen;

namespace WorldGenerator.UI{
    public class DialogUI : UserInterface{
        public DialogUI(string header,string message){
            var errorLabel = new Label{
                TextColor = Color.Red,
                Font = Screen.HeaderFont,
                GridColumn = 0,
                GridRow = 0,
                Text = header,
            };
            var label = new Label{
                Text = message,
                GridRow = 1,
                GridColumn = 0,
                HorizontalAlignment = HorizontalAlignment.Center,
                Font = Screen.ErrorFont,

            };
            
            var button = CrateBackButton(2, 0);
            button.Click += (sender, args) => RequestPreviousInterface();
            button.HorizontalAlignment = HorizontalAlignment.Center;
            _widgets.Add(label);
            _widgets.Add(button);
            _widgets.Add(errorLabel);
        }
    }
}