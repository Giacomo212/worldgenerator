﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;


namespace Game{
     public abstract class Context{
         protected Context StartUiContextCreator = null;
        protected SpriteBatch _spriteBatch = null;
        protected readonly Desktop Desktop = new Desktop();
        //This allow to manipulate game parameters  within context 
        //needs to be set up before using this class
        public static Microsoft.Xna.Framework.Game Game;
        public abstract Context Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Initialize();
        public abstract void Load();
        //this method allow to react context to windows resize event
        public abstract void OnWindowResize();
        public abstract void Unload();
     }
}