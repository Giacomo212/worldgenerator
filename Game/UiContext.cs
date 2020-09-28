using System;
using System.Collections.Generic;
using System.IO;
using World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Attributes;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Types;



namespace Game{
    public class MainUiContext : Context{
        private Desktop _desktop;
        private Stack<Desktop> _stack;
        //private MapContext _mapContext = new MapContext(WorldSize.Small);
        private Grid _grid;
        //main buttons
        private TextButton _startGameButton;
        private TextButton _configButton;
        private TextButton _exitButton;
        //config button
        private Random _random = new Random();
        //utility values
        private int _worldCount = 0;
        //map creation ui
        private ScrollViewer _scrollViewer;
        private TextButton[] _worldButtons;
        private TextButton _createWorldButton;
        private Texture2D _filler;
        public MainUiContext(){
            
            _stack = new Stack<Desktop>();
            _stack.Push(new Desktop());
            var a = _stack.Pop();

        }

        public override IChangeContext Update(GameTime gameTime){
            // var random = new Random();
            //  if(gameTime.TotalGameTime.Milliseconds%1000000 == 0) 
            //      _direction = random.Next(0, 4);
            // switch (_direction){
            //     case 0:
            //         _mapContext.MoveDown(); break;
            //     case 1:
            //         _mapContext.MoveUp(); break;
            //     case 2:
            //         _mapContext.MoveLeft(); break;
            //     case 3:
            //         _mapContext.MoveRight(); break;
            // }
            return ChangeContext;
        }

        public override void Draw(GameTime gameTime){
            var vector = new Vector2(0,0);
            _spriteBatch.Begin();
            for (; vector.X < GameConfig.Config.Resolution.Width; vector.X+=Block.Size){
                for (vector.Y = 0; vector.Y < GameConfig.Config.Resolution.Hight; vector.Y+=Block.Size){
                    _spriteBatch.Draw(_filler,vector,Color.White);
                }
                
            }
            _spriteBatch.End();
            //_mapContext.Draw(gameTime);
            _desktop.Render();
           
        }

        public override void Initialize(){
           // _mapContext.Initialize();
        }

        public override void Load(){
         
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //_mapContext.Load();
            _grid = new Grid {
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            _grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            _desktop = new Desktop();
            _desktop.Root = _grid;
            SetupMainButtons();
            AddMainButtons();
            _filler = Game.Content.Load<Texture2D>("dirt");
        }

        public override void OnWindowResize(){
            //_mapContext.OnWindowResize();
        }

        private void SetupMainButtons(){
            _startGameButton = new TextButton{
                Text = "Start a game",
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _configButton = new TextButton{
                Text = "Config",
                Padding = new Thickness(20),
                GridRow = 2,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _exitButton = new TextButton{
                Text = "Exit",
                Padding = new Thickness(20),
                GridRow = 3,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _startGameButton.Click += (s, a) => {SetupMapUi(); };//_action = new ChangeToNewMap(200, 200); };
            _exitButton.Click += (s, a) => { System.Environment.Exit(0); };
        }

        private void SetupMapUi(){
            _grid.Widgets.Clear();
            _createWorldButton = new TextButton(){
                Text = "crate new world",
                GridRow = 2,
            };
            _createWorldButton.Click += (sender, args) => {
                //_random.Next()
                
                ChangeContext = new ChangeToNewMap(new Map("world"+_worldCount,WorldSize.Large,_random.Next()));;
                _worldCount++;
            };
            var grid = new Grid(){
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            
            _scrollViewer = new ScrollViewer(){
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 200,
                MaxHeight = 200,
            };
            _scrollViewer.Content = grid;
            _grid.Widgets.Add(_scrollViewer);
            _grid.Widgets.Add(_createWorldButton);
            GetAllWorlds();
            foreach (var worldButton in _worldButtons){
                grid.Widgets.Add(worldButton);
            }
            
        }
        private void AddMainButtons(){
            _grid.Widgets.Clear();
            _grid.Widgets.Add(_startGameButton);
            _grid.Widgets.Add(_configButton);
            _grid.Widgets.Add(_exitButton);
        }

        private void GetAllWorlds(){
            
            DirectoryInfo d = new DirectoryInfo(EnvironmentVariables.GameFiles + Path.DirectorySeparatorChar + "worlds");
            FileInfo[] files = d.GetFiles("*.wg"); //Getting files
            _worldCount = files.Length;
            _worldButtons = new TextButton[files.Length];
            for(var i = 0; i < files.Length;i++ ){
                _worldButtons[i] = new TextButton(){
                    Text = files[i].Name.Replace(".wg",string.Empty),
                    Padding = new Thickness(10),
                    GridRow = i,
                };
                _worldButtons[i].Click += (sender, args) => { 
                    var tmp = sender as TextButton;
                        ChangeContext = new ChangeToMap(tmp.Text);
                     };
            }
        }
    }
}