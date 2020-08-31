using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;


namespace worldgenerator {
    public class MainUiContext : Context{
        private Action _action = Action.None;
        private Desktop _desktop;
        
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
            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
          
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            _desktop = new Desktop();
            _desktop.Root = grid;
            var button = new TextButton{
                Text = "Start a game",
            };
            button.Click += (s,a) => {
                _action = Action.ChangeToNewMap;
            };
            grid.AddChild(button);

        }
    }
}
