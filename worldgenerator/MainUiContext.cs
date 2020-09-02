using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;


namespace worldgenerator{
    public class MainUiContext : Context{
        private SpriteBatch _spriteBatch;
        //private Action _action = Action.None;
        private Desktop _desktop;
        private MapContext _mapContext = new MapContext(80,80);
        private Grid _grid;
        //main buttons
        private TextButton _startGameButton;
        private TextButton _configButton;
        private TextButton _exitButton;
        //config button
        private int _direction = 3;

        //map creation buttons
        public MainUiContext(){
        }

        public override IAction Update(GameTime gameTime){
            var random = new Random();
             if(gameTime.TotalGameTime.Milliseconds%1000000 == 0) 
                 _direction = random.Next(0, 4);
            switch (_direction){
                case 0:
                    _mapContext.MoveDown(); break;
                case 1:
                    _mapContext.MoveUp(); break;
                case 2:
                    _mapContext.MoveLeft(); break;
                case 3:
                    _mapContext.MoveRight(); break;
            }
            return _action;
        }

        public override void Draw(GameTime gameTime){
            Texture2D tmp = new Texture2D(Game.GraphicsDevice, 200, 200);
            
            _spriteBatch.Begin();
            _mapContext.Draw(gameTime);
            _spriteBatch.Draw(tmp,Vector2.Zero,Color.Blue);
            _spriteBatch.End();
            _desktop.Render();
        }

        public override void Initialize(){
            _mapContext.Initialize();
        }

        public override void Load(){
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _mapContext.Load();
            _grid = new Grid {
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                GridColumn = 1,
                GridRow = 4,
            };
            _grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            _desktop = new Desktop();
            _desktop.Root = _grid;
            SetupMainButtons();
            AddMainButtons();
            
        }

        public override void OnWindowResize(){
            _mapContext.OnWindowResize();
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
            _startGameButton.Click += (s, a) => { _action = new ChangeToNewMap(200, 200); };
            _exitButton.Click += (s, a) => { System.Environment.Exit(0); };
        }

        private void AddMainButtons(){
            _grid.Widgets.Clear();
            _grid.Widgets.Add(_startGameButton);
            _grid.Widgets.Add(_configButton);
            _grid.Widgets.Add(_exitButton);
        }
    }
}