using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace worldgenerator {
    public class MainUiContext : Context {
        private MainUi _mainUi = new MainUi();
        public MainUiContext() {
            TextureToLoad = new Content<Texture2D>[1];
            FontToLoad = new Content<SpriteFont>[1];
            TextureToLoad[0] = new Content<Texture2D>("background");
            FontToLoad[0] = new Content<SpriteFont>("baseFont");
        }
        public override int Update(GameTime gameTime) {

            if (Keyboard.HasBeenPressed(Keys.Up)) {
                _mainUi.DecrementIndex();
            } else if (Keyboard.HasBeenPressed(Keys.Down)) {
                _mainUi.IncrementIndex();
            }
            if (Keyboard.HasBeenPressed(Keys.Enter)) {
                return _mainUi.SeletedIndex;
            } 
            return -1;
        }

        public override void Draw(ref SpriteBatch spriteBatch) {
            
            //spriteBatch.Draw(TextureToLoad[0].Value, new Vector2(0, 0), Color.White);
            for (var i = 0; i < _mainUi.Count;i++) {
                if (_mainUi.IsSeleted(i)) {
                    spriteBatch.DrawString(FontToLoad[0].Value, _mainUi[i].Text, _mainUi[i].Position, Color.Red);
                } else
                    spriteBatch.DrawString(FontToLoad[0].Value, _mainUi[i].Text, _mainUi[i].Position, Color.White);
            }

        }

        public override void Initialize() {

        }



    }
}
