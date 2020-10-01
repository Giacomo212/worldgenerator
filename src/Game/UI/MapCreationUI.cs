using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Types;
using Game.WorldMap;

namespace Game.UI{
    public class MapCreationUI : UserInterface{
        //utility values
        private int _worldCount = 0;
        private Random _random = new Random();

        //map creation ui
        private Grid _scrollGrid;
        private Grid _buttonGrid;
        private ScrollViewer _scrollViewer;
        private TextButton[] _worldButtons;
        private TextButton _createWorldButton;
        private TextButton _deleteWorldButton;

        public MapCreationUI() : base(){
            
            SetupMapUi();
        }


        private void SetupMapUi(){
            _scrollGrid = new Grid{
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            _buttonGrid = new Grid{
                RowSpacing = 0,
                ColumnSpacing = 0,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                GridRow = 2,
            };
            _createWorldButton = new TextButton(){
                Text = "crate new world",
                Padding = new Thickness(10),
            };
            _deleteWorldButton = new TextButton(){
                Text = "Delete World",
                GridColumn = 2,
                Padding = new Thickness(10),
            };
            _deleteWorldButton.Click += (sender, args) => SetDelete();
            _createWorldButton.Click += (sender, args) => {
                var map = new Map("world" + _worldCount, WorldSize.Large, _random.Next());
                var mapGenerator = new MapGenerator(map, new SurfaceChunkGenerator(_random.Next()));
                mapGenerator.Dispose();
                StartUiContext = new MapContext(map);
                _worldCount++;
            };

            _scrollViewer = new ScrollViewer{
                
                GridRow = 1,
                Content = _scrollGrid,
            };

            Widgets.Add(_scrollViewer);
            Widgets.Add(_buttonGrid);
            _buttonGrid.Widgets.Add(_createWorldButton);
            _buttonGrid.Widgets.Add(_deleteWorldButton);
            GetAllWorlds();
            foreach (var worldButton in _worldButtons){
                _scrollGrid.Widgets.Add(worldButton);
            }
        }

        private void GetAllWorlds(){
            DirectoryInfo d =
                new DirectoryInfo(EnvironmentVariables.GameFiles + Path.DirectorySeparatorChar + "worlds");
            FileInfo[] files = d.GetFiles("*.wg"); //Getting files
            _worldCount = files.Length;
            _worldButtons = new TextButton[files.Length];
            for (var i = 0; i < files.Length; i++){
                _worldButtons[i] = new TextButton(){
                    Text = files[i].Name.Replace(".wg", string.Empty),
                    Padding = new Thickness(10),
                    GridRow = i,
                    Width = 700,
                };
                _worldButtons[i].Click += (sender, args) => {
                    var tmp = sender as TextButton;
                    StartUiContext = new MapContext(MapReader.ReadMap(tmp.Text));
                };
            }
        }

        private void SetDelete(){
            foreach (var button in _worldButtons){
                button.Click += (sender, args) => {
                    var tmp = sender as TextButton;
                    tmp.Text = "asdf";
                };
            }
        }
        
    }
}