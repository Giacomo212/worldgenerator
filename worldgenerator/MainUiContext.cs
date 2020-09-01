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
using Myra.Graphics2D.UI;


namespace worldgenerator {
    public class MainUiContext : Context{
        private Action _action = Action.None;
        private Desktop _desktop;
        private Grid _grid;
        //main buttons
        private TextButton _startGameButton;
        private TextButton _configButton;
        private TextButton _exitButton;
        //config button
        
        
        //map creation buttons
        public MainUiContext() {
            
        }
        public override Action Update(GameTime gameTime){
            return _action;
        }

        public override void Draw(ref SpriteBatch spriteBatch) {
            _desktop.Render();
        }

        public override void Initialize() {

        }

        public override void Load(){
             _grid = new Grid
            {
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
            _startGameButton.Click += (s,a) => {
                _action = Action.ChangeToNewMap;
            };
        }
        
        private void AddMainButtons(){
            _grid.Widgets.Clear();
            _grid.Widgets.Add(_startGameButton);
            _grid.Widgets.Add(_configButton);
            _grid.Widgets.Add(_exitButton);
        }
    }
}
