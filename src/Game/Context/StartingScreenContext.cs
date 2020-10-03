using System.Collections.Generic;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Types;


namespace Game.GameContext{
    public class StartingScreenContext : Context{
        protected Stack<UserInterface> _userInterfaces = new Stack<UserInterface>();
        private Texture2D _filler;
        public override Context Update(GameTime gameTime){
            if (Keyboard.IsPressed(Keys.Escape))
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
        protected void AddNewUI(UserInterface userInterface){
            _userInterfaces.Push(userInterface);
            userInterface.OnContextChangeRequest += (sender, args) => NewContext = args.Context;
            userInterface.OnNextUIRequest += (sender, args) => AddNewUI(args.Interface);
            userInterface.OnPreviousUIRequest += (sender, args) => RemoveUI();
            Desktop.Root = userInterface;
        }

        protected void RemoveUI(){
            if (_userInterfaces.Count <= 1) return;
            _userInterfaces.Pop();
            Desktop.Root = _userInterfaces.Peek();
        }
    }
}