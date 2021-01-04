using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace WorldGenerator.UI{
    public class ToggleableButton : TextButton{
        private bool _isToggled = false;

        public static ToggleableButton CrateButton(string text, int row, int column){
            return new ToggleableButton{
                Text = text,
                GridRow = row,
                GridColumn = column,
                Width = 150,
                Padding = new Thickness(10),
            };
        }
        public bool IsToggled{
            get => _isToggled;
            set{
                if (value){
                    
                    Background = new SolidBrush(Color.Red);
                    FocusedBackground = new SolidBrush(Color.Red);
                    OverBackground = new SolidBrush(Color.Red);
                }
                else{
                    Background = new SolidBrush(Color.Black);
                    FocusedBackground = new SolidBrush(Color.Black);
                    OverBackground = new SolidBrush(Color.Black);
                    
                }

                _isToggled = value;
            }
        }
    }
}