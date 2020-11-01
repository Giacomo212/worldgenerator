using System.IO;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using WorldGenerator.EventArg;
using EnvironmentVariables = WorldGenerator.Utils.EnvironmentVariables;

namespace WorldGenerator.UI{
    public abstract class MapChooserUI : UserInterface{
        //map creation ui
        protected readonly Grid _scrollGrid;
        protected readonly Grid _buttonGrid;
        protected TextButton[] _worldButtons;
        protected readonly TextButton _deleteWorldButton;
        protected readonly TextButton _imageGeneratorButton;

        protected MapChooserUI() : base(){
            _scrollGrid = new Grid{
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            _buttonGrid = new Grid{
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                GridRow = 1,
            };
            var createWorldButton = CrateTextButton("Crate new World", 0, 0);
            _deleteWorldButton = CrateTextButton("Delete a world", 0, 1);
             _imageGeneratorButton = CrateTextButton("Generate image", 0, 2);
            var cancelButton = CrateBackButton( 1, 2);
            cancelButton.Click += (sender, args) => RequestPreviousInterface();
            createWorldButton.Click += (sender, args) => {
                RequestNewInterface(new UiChangeRequestArgs(new MapCustomizerUI()));
            };
            var scrollViewer = new ScrollViewer{
                GridRow = 0,
                Content = _scrollGrid,
                Height = 400,
            };
            _widgets.Add(scrollViewer);
            _widgets.Add(_buttonGrid);
            _buttonGrid.Widgets.Add(createWorldButton);
            _buttonGrid.Widgets.Add(_deleteWorldButton);
            _buttonGrid.Widgets.Add(cancelButton);
            _buttonGrid.Widgets.Add(_imageGeneratorButton);
            GetAllWorlds();
        }
        

        protected virtual void GetAllWorlds(){
            DirectoryInfo d =
                new DirectoryInfo(EnvironmentVariables.WorldFiles);
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