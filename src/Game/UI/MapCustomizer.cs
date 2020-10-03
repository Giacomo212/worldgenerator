using System;
using System.IO;
using System.Linq;
using Game.GameContext;
using Game.WorldMap;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Types;

namespace Game.UI{
    public class MapCustomizer : UserInterface{
        private TextBox _nameTextBox;
        private TextBox _seedTextBox;
        private ComboBox _sizeComboBox;

        private Label _infolabel = new Label{
            Text = "",
            GridColumnSpan = 2,
            Width = 400,
            GridRow = 4,
            TextAlign = TextAlign.Center,
            //Background = new SolidBrush(Color.Red);
        };

        public MapCustomizer() : base(){
            var nameLabel = new Label{
                Text = "Enter a name:",
                GridRow = 0,
                GridColumn = 0
            };
            _nameTextBox = new TextBox(){
                GridRow = 0,
                GridColumn = 1,
                Padding = new Thickness(10),
                Width = 200,
                Text = "World",
            };
            var seedLabel = new Label{
                Text = "Enter a seed",
                GridRow = 1,
                GridColumn = 0,
            };
            _seedTextBox = new TextBox(){
                GridRow = 1,
                Padding = new Thickness(10),
                GridColumn = 1,
                Width = 200,
            };
            var sizeLabel = new Label(){
                Text = "Select a world type",
                GridRow = 2,
            };
            var largeItem = new ListItem(){
                Text = "Large",
            };

            var smallItem = new ListItem(){
                Text = "Small",
            };
            var mediumItem = new ListItem(){
                Text = "Medium",
                
            };

            _sizeComboBox = new ComboBox{
                Items = {largeItem, mediumItem, smallItem},
                GridRow = 2,
                GridColumn = 1,
                Width = 200,
                SelectedIndex = 0,
            };

            var createButton = CrateTextButton("Crate a world", 3, 0);
            //     new TextButton{
            //     Text = "Crate a world",
            //     GridRow = 3,
            //     GridColumn = 0,
            //     Padding = new Thickness(10),
            //     Width = 200,
            // };
            createButton.Click += CrateNewWorld;
            var cancelButton = CrateTextButton("Cancel", 3, 1);
            //     = new TextButton{
            //     Text = "Cancel",
            //     GridRow = 3,
            //     GridColumn = 1,
            //     Padding = new Thickness(10),
            //     Width = 200,
            // };
            cancelButton.Click += (sender, args) => RequestPreviousInterface();
            _widgets.Add(nameLabel);
            _widgets.Add(_nameTextBox);
            _widgets.Add(_seedTextBox);
            _widgets.Add(seedLabel);
            _widgets.Add(sizeLabel);
            _widgets.Add(_sizeComboBox);
            _widgets.Add(createButton);
            _widgets.Add(cancelButton);
            _widgets.Add(_infolabel);
        }
        
        private static int GetNumber(string text){
            try{
                return Convert.ToInt32(text);
            }
            catch{
                return text.Sum(c => c * 2137);
            }
        }

        private static WorldSize Parse(string text){
            if (text == "Large")
                return WorldSize.Large;
            if (text == "Medium")
                return WorldSize.Medium;
            return WorldSize.Small;
        }

        private void CrateNewWorld(object? sender, EventArgs args){
            if (File.Exists(EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator + _nameTextBox.Text + ".wg")){
                var window = new Window();
                _infolabel.Text = "File already exist";
                return;
            }

            var tmp = GetNumber(_seedTextBox.Text);
            var random = new Random();
            if (tmp == 0)
                tmp = random.Next();
            var map = new Map(_nameTextBox.Text, Parse(_sizeComboBox.SelectedItem.Text), tmp);
            var generator = new MapGenerator(map, new SurfaceChunkGenerator(tmp));
            generator.Dispose();
            //_context = new MapContext(map);
            RequestContext(new ContextChangeRequested(new MapContext(map, new GameInterface())));
                
        }

      
    }
}