using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace worldgenerator {
    abstract public class Context {


        public Content<Texture2D>[] TextureToLoad { get; set; }
        public Content<SpriteFont>[] FontToLoad { get; set; }
        public abstract int Update(GameTime gameTime);
        public abstract void Draw(ref SpriteBatch spriteBatch);
        public abstract void Initialize();
        
    }
}