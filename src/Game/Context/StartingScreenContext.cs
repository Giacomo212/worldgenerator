using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Types;


namespace Game{
    public class StartingScreenContext : Context{
        private Texture2D _filler;

        public override Context Update(GameTime gameTime){
            RemoveUI();
            return NewContext;
        }

        public override void Draw(GameTime gameTime){
            var vector = new Vector2(0, 0);
            _spriteBatch.Begin();
            for (; vector.X < GameConfig.Config.Resolution.Width; vector.X += Block.Size){
                for (vector.Y = 0; vector.Y < GameConfig.Config.Resolution.Hight; vector.Y += Block.Size){
                    _spriteBatch.Draw(_filler, vector, Color.White);
                }
            }

            _spriteBatch.End();
            Desktop.Render();
        }

        public override void Initialize(){
        }

        public override void Load(){
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            AddNewUI(new MainUi());
            _filler = Game.Content.Load<Texture2D>("dirt");
        }
    }
}