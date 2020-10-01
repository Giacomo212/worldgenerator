using System.Buffers.Text;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class ConfigUI : UserInterface{
        public ConfigUI() : base(){
            MainGrid.RowSpacing = 0;
            var scrollLabel = new Label{
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = "Scroll speed",
                GridRow = 0
            };
            var slider = new HorizontalSlider{
                Minimum = 0.3f,
                Maximum = 40f,
                Width = 150,
                GridRow = 1,
                Padding = new Thickness(2),
            };
            var button = new TextButton(){
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = "Apply",
                GridRow = 4,
            };
            var comboText = new Label{
                Text = "Resolution",
                GridRow = 2,
                Padding = new Thickness(2),
            };
            var comboBox = new ComboBox{
                GridRow = 3,
                Padding = new Thickness(2),
            };
            var item = new ListItem{
                Text = "Test",
            };
            comboBox.Items.Add(item);
            MainGrid.Widgets.Add(slider);
            MainGrid.Widgets.Add(scrollLabel);
            MainGrid.Widgets.Add(button);
            MainGrid.Widgets.Add(comboText);
            MainGrid.Widgets.Add(comboBox);
            
        }
    }
}