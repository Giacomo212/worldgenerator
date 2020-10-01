using System.Collections.Generic;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Types;


namespace Game{
    public class StartingScreenContext : Context{
        private Texture2D _filler;
        private Stack<UserInterface> _userInterfaces = new Stack<UserInterface>();
        private UserInterface _interface;

        public override Context Update(GameTime gameTime){
            if (Keyboard.HasBeenPressed(Keys.Escape) && _userInterfaces.Count > 0){
                _interface = _userInterfaces.Pop();
            }
            var tmp = _interface.CreateNewUI();
            if (tmp == null) return _interface.CrateNewContext();
            _userInterfaces.Push(_interface);
            _interface = tmp;
            return _interface.CrateNewContext();
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
            _interface.Render();
        }

        public override void Initialize(){
        }

        public override void Load(){
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _interface = new MainUi();
            _filler = Game.Content.Load<Texture2D>("dirt");
        }

        public override void OnWindowResize(){
        }

        public override void Unload(){
            
        }
    }
}