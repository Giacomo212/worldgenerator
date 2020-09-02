using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace worldgenerator{
    abstract public class Context{
        protected IAction _action = null;
        public static Game Game;
        public abstract IAction Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Initialize();
        public abstract void Load();
        public abstract void OnWindowResize();
    }
}