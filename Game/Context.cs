using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Game{
    abstract public class Context{
        protected IAction _action = null;
        protected SpriteBatch _spriteBatch = null;
        public static Microsoft.Xna.Framework.Game Game;
        public abstract IAction Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Initialize();
        public abstract void Load();
        public abstract void OnWindowResize();
    }
}