using System.Buffers.Text;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class ConfigUI : UserInterface{
        public ConfigUI() : base(){
            var scrollLabel = new Label{
                Text = "Scroll speed",
                GridRow = 0,
                GridColumn = 0
            };
            var slider = new HorizontalSlider{
                Minimum = 0.3f,
                Maximum = 40f,
                Width = 150,
                GridRow = 0,
                Padding = new Thickness(2),
                GridColumn = 1
            };
            var button = CrateTextButton("Apply", 2, 0);
            var cancelButton = CrateTextButton("Cancel", 2, 1);
            cancelButton.Click += (sender, args) => RequestPreviousInterface();
            var comboText = new Label{
                Text = "Resolution",
                GridRow = 1,
                GridColumn = 0,
            };
            var comboBox = new ComboBox{
                GridRow = 1,
                GridColumn = 1,
            };
            var item = new ListItem{
                Text = "Test",
            };
            var keyboardMenuButton = CrateTextButton("Edit a keybind", 2, 0);
            keyboardMenuButton.GridColumnSpan = 2;
            // keyboardMenuButton
            _widgets.Add(keyboardMenuButton);
            comboBox.Items.Add(item);
            _widgets.Add(slider);
            _widgets.Add(scrollLabel);
            _widgets.Add(button);
            _widgets.Add(comboText);
            _widgets.Add(comboBox);
            _widgets.Add(cancelButton);
        }
    }
}