using System;
using System.IO;
using System.Linq;
using Game.DataContainers;
using Game.EventArg;
using Game.GameContext;
using Game.Runners;
using Game.Utils;
using Game.WorldMap;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;



namespace Game.UI{
    public class MapCustomizerUI : UserInterface{
        private readonly TextBox _nameTextBox;
        private readonly TextBox _seedTextBox;
        private readonly ComboBox _sizeComboBox;
        private readonly ComboBox _typeComboBox;
        private Map _map;

        private Label _infolabel = new Label{
            Text = "",
            GridColumnSpan = 2,
            Width = 400,
            GridRow = 4,
            TextAlign = TextAlign.Center,
            //Background = new SolidBrush(Color.Red);
        };

        public MapCustomizerUI() : base(){
            var nameLabel = new Label{
                Text = "Enter a name:",
                GridRow = 0,
                GridColumn = 0
            };
            var typeLabel = new Label{
                Text = "Select a type",
                GridRow = 3,
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
                Text = "Select a world size",
                GridRow = 2,
            };
            var largeItem = new ListItem{
                Text = "Large",
            };
            var smallItem = new ListItem{
                Text = "Small",
            };
            var mediumItem = new ListItem{
                Text = "Medium",
            };
            var archipelagoItem = new ListItem{
                Text = "Archipelago",
            };
            var continentItem = new ListItem{
                Text = "Continents",
            };
            var landItem = new ListItem{
                Text = "Land",
            };
            _sizeComboBox = new ComboBox{
                Items = {largeItem, mediumItem, smallItem},
                GridRow = 2,
                GridColumn = 1,
                Width = 200,
                SelectedIndex = 0,
            };
            _typeComboBox = new ComboBox{
                Items = {archipelagoItem, continentItem, landItem},
                GridRow = 3,
                GridColumn = 1,
                Width = 200,
                SelectedIndex = 0,
            };

            var createButton = CrateTextButton("Crate a world", 4, 0);
            createButton.Click += CrateNewWorld;
            var cancelButton = CrateBackButton(4, 1);
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
            _widgets.Add(_typeComboBox);
            _widgets.Add(typeLabel);
        }

        private static int GetNumber(string text){
            try{
                return Convert.ToInt32(text);
            }
            catch{
                return text.Sum(c => c * 2137);
            }
        }

        private static WorldSize ParseWorldSize(string text){
            return text.ToLower() switch{
                "large" => WorldSize.Large,
                "medium" => WorldSize.Medium,
                "small" => WorldSize.Small
            };
        }

        private static IChunkGenerator ParseWorldType(string text, Map map){
            return text.ToLower() switch{
                "archipelago" => new IslandWorldGenerator(map),
                "land" => new LandChunkGenerator(map),
                "continents" => new ContinentChunkGenerator(map),
            };
        }

        private void CrateNewWorld(object sender, EventArgs args){
            if (File.Exists(
                EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator + _nameTextBox.Text + ".wg")){
                var window = new Window();
                _infolabel.Text = "File already exist";
                return;
            }

            var tmp = GetNumber(_seedTextBox.Text);
            var random = new Random();
            if (tmp == 0)
                tmp = random.Next();
            _map = new Map(_nameTextBox.Text, ParseWorldSize(_sizeComboBox.SelectedItem.Text), tmp);
            var process = new MapGenerationRunner(_map,ParseWorldType(_typeComboBox.SelectedItem.Text, _map)); 
            RequestNewInterface(new UiChangeRequestArgs(new LoadingUI(process,"Generating a world")));
            // var generator = new MapGenerator(_map, ParseWorldType(_typeComboBox.SelectedItem.Text, _map));
            // generator.GenerateNewWorld();
        }

        public override void Awake(){
            //Thread.Sleep(1000);
            RequestContext(new ContextChangeRequestedArgs(new MapContext(_map, new GameInterface())));
        }
    }
}