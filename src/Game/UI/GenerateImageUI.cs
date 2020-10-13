using System.IO;
using Game.WorldMap;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Types;

namespace Game.UI{
    public class GenerateImageUI : UserInterface{
        private readonly Grid _scrollGrid;
        private TextButton[] _worldButtons;
        public GenerateImageUI(){
            _scrollGrid = new Grid{
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var scrollViewer = new ScrollViewer{
                GridRow = 0,
                Content = _scrollGrid,
                Height = 400,
            };
            GetAllWorlds();

            foreach (var button in _worldButtons){
                button.Click += (sender, args) => {
                    var tmp = sender as TextButton;
                    ImageGenerator.GenerateImage(MapReader.ReadMap(tmp.Text));
                };
            }
            _widgets.Add(scrollViewer);
            var backButton = CrateBackButton(1, 0);
            backButton.HorizontalAlignment = HorizontalAlignment.Right;
            backButton.Click += (sender, args) => RequestPreviousInterface();
            _widgets.Add(backButton);
        }

        
        
        private void GetAllWorlds(){
            DirectoryInfo d =
                new DirectoryInfo(EnvironmentVariables.Worldfiles);
            var files = d.GetFiles("*.wg");
            _worldButtons = new TextButton[files.Length];
            for (var i = 0; i < files.Length; i++){
                _worldButtons[i] = new TextButton(){
                    Text = files[i].Name.Replace(".wg", string.Empty),
                    Padding = new Thickness(10),
                    GridRow = i,
                    Width = 700,
                };
                
            }
            _scrollGrid.Widgets.Clear();
            foreach (var worldButton in _worldButtons){
                _scrollGrid.Widgets.Add(worldButton);
            }
        }
    }
}