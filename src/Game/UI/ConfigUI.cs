using System.Buffers.Text;
using Game.GameContext;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class ConfigUI : UserInterface{
        private HorizontalSlider _slider;
        public ConfigUI() : base(){
            var scrollLabel = new Label{
                Text = "Scroll speed",
                GridRow = 0,
                GridColumn = 0
            };
             _slider = new HorizontalSlider{
                Minimum = 0.3f,
                Maximum = 40f,
                Width = 150,
                GridRow = 0,
                Padding = new Thickness(2),
                GridColumn = 1,
                Value = GameConfig.Config.Sensivity,
             };
            var applyButton = CrateTextButton("Apply", 3, 0);
            applyButton.Click += (sender, args) => GameConfig.Config.Sensivity = _slider.Value;
            var cancelButton = CrateTextButton("Cancel", 3, 1);
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
            var keyboardMenuButton = CrateTextButton("Edit a key bind", 2, 0);
            keyboardMenuButton.HorizontalAlignment = HorizontalAlignment.Center;
            keyboardMenuButton.Width = 300;
            keyboardMenuButton.GridColumnSpan = 2;
            keyboardMenuButton.Click +=
                (sender, args) => RequestNewInterface(new UiChangeRequestArgs(new KeyBindCustomizerUI()));  
            _widgets.Add(keyboardMenuButton);
            comboBox.Items.Add(item);
            _widgets.Add(_slider);
            _widgets.Add(scrollLabel);
            _widgets.Add(applyButton);
            _widgets.Add(comboText);
            _widgets.Add(comboBox);
            _widgets.Add(cancelButton);
        }
    }
}