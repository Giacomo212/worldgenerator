using System;
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Types;
using Game.WorldMap;
using Myra.Graphics2D.Brushes;
using Color = Microsoft.Xna.Framework.Color;

namespace Game.UI{
    public class MapCreationUI : UserInterface{
        //map creation ui
        private Grid _scrollGrid;
        private Grid _buttonGrid;
        private ScrollViewer _scrollViewer;
        private TextButton[] _worldButtons;
        //private TextButton createWorldButton;
        private TextButton _deleteWorldButton;
        private TextButton _stopDeleteWorldButton;

        public MapCreationUI() : base(){
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
            //     = new TextButton(){
            //     Text = "crate new world",
            //     Padding = new Thickness(10),
            // };
            _deleteWorldButton = CrateTextButton("Delete a world", 0, 1);
            //      new TextButton(){
            //     Text = "Delete World",
            //     GridColumn = 2,
            //     Padding = new Thickness(10),
            // };
             _stopDeleteWorldButton = new TextButton(){
                Text = "Delete a World",
                GridColumn = 1,
                Padding = new Thickness(10),
                Background = new SolidBrush(Color.Red),
                Visible = false,
                Width = 150,
                FocusedBackground = new SolidBrush(Color.Red),
                PressedBackground = new SolidBrush(Color.Red),
                OverBackground = new SolidBrush(Color.Red),
             };
             
             var cancelButton = CrateTextButton("cancel", 0, 2);
            
            _deleteWorldButton.Click += DeleteWorldButtonOnClick;
            _stopDeleteWorldButton.Click += StopDeleteWorldButtonOnClick;
            createWorldButton.Click += (sender, args) => {
                _interface = new MapCustomizerInterface();
            };

            _scrollViewer = new ScrollViewer{
                GridRow = 0,
                Content = _scrollGrid,
                Height = 400,
            };

            _widgets.Add(_scrollViewer);
            _widgets.Add(_buttonGrid);
            _buttonGrid.Widgets.Add(createWorldButton);
            _buttonGrid.Widgets.Add(_deleteWorldButton);
            _buttonGrid.Widgets.Add(_stopDeleteWorldButton);
            _buttonGrid.Widgets.Add(cancelButton);
            GetAllWorlds();
            foreach (var button in _worldButtons)
                button.Click += LoadWorldOnClick;
        }

        private void StopDeleteWorldButtonOnClick(object? sender, EventArgs e){
            _deleteWorldButton.Visible = true;
            _stopDeleteWorldButton.Visible = false;
            foreach (var button in _worldButtons){
                button.Click += LoadWorldOnClick;
                button.Click -= DeleteWorld;
            }
        }

        private void DeleteWorldButtonOnClick(object? sender1, EventArgs e){
            _deleteWorldButton.Visible = false;
            _stopDeleteWorldButton.Visible = true;
            foreach (var button in _worldButtons){
                button.Click -= LoadWorldOnClick;
                button.Click += DeleteWorld;
            }
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
        //methods to delete world
        private void LoadWorldOnClick(object? sender, EventArgs e){
            var tmp = sender as TextButton;
            _context = new MapContext(MapReader.ReadMap(tmp.Text));
        }
        private void DeleteWorld(object? sender, EventArgs e){
            var tmp = sender as TextButton;
            File.Delete(EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator + tmp.Text + ".wg");
            GetAllWorlds();
            foreach (var button in _worldButtons)
                button.Click += DeleteWorld;
        }
        
    }
}