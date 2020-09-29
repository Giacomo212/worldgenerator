using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Types;
using World;

namespace Game.UI{
    public class MapCreationUI : UserInterface{
        //utility values
        private int _worldCount = 0;
        private Random _random = new Random();

        //map creation ui
        private Grid _grid;
        private ScrollViewer _scrollViewer;
        private TextButton[] _worldButtons;
        private TextButton _createWorldButton;

        public MapCreationUI() : base(){
            _grid = new Grid{
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };
            Root = _grid;
            SetupMapUi();
        }


        private void SetupMapUi(){
            _createWorldButton = new TextButton(){
                Text = "crate new world",
                GridRow = 2,
                Padding = new Thickness(10),
            };
            _createWorldButton.Click += (sender, args) => {
                var map = new Map("world" + _worldCount, WorldSize.Large, _random.Next());
                using var mapGenerator = new MapGenerator(map, new SurfaceChunkGenerator(_random.Next()));
                mapGenerator.Dispose();
                StartUiContext = new MapContext(map);
                _worldCount++;
            };
            var grid = new Grid(){
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            _scrollViewer = new ScrollViewer{
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                GridRow = 1,
                Content = grid,
            };

            _grid.Widgets.Add(_scrollViewer);
            _grid.Widgets.Add(_createWorldButton);
            GetAllWorlds();
            foreach (var worldButton in _worldButtons){
                grid.Widgets.Add(worldButton);
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
    }
}