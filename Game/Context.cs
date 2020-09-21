using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Game{
     public abstract class Context{
        protected IChangeContext ChangeContext = null;
        protected SpriteBatch _spriteBatch = null;
        public static Microsoft.Xna.Framework.Game Game;
        public abstract IChangeContext Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Initialize();
        public abstract void Load();
        public abstract void OnWindowResize();
    }
}