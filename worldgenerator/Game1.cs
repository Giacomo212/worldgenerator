using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace worldgenerator {
    
    public class Game1 : Game {
        
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Context _currentContext;

        public Game1() {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            GameConfig.config.Load();
            _currentContext = new MainUiContext();
        }


        protected override void Initialize() {
            base.Initialize();
            _currentContext.Initialize();
            Keyboard.Initialize();
            
          
            graphics.PreferredBackBufferWidth = GameConfig.config.Resolution.Width;
            graphics.PreferredBackBufferHeight = GameConfig.config.Resolution.Hight;
            graphics.IsFullScreen = GameConfig.config.Resolution.IsFullScreen;
            graphics.ApplyChanges();

        }


        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (var texture in _currentContext.TextureToLoad) {
                texture.Value = Content.Load<Texture2D>(texture.Name);
            }

            if (_currentContext.FontToLoad != null)
                foreach (var font in _currentContext.FontToLoad) {
                    font.Value = Content.Load<SpriteFont>(font.Name);
                }

           
        }

        protected override void UnloadContent() {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime) {
            Keyboard.UpdateState();
            switch (_currentContext.Update(gameTime)) {
                case 0:
                    _currentContext = new MapContext(200,200);
                    ReloadContent(); break;
                case 1:
                    _currentContext = new MapContext("./map.wg");
                    ReloadContent(); break;
                case 2:; break;
                case 3: Exit(); break;
                case 4:
                    _currentContext = new MainUiContext();
                    ReloadContent();
                    break;
                case -1: break;

            }
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
            _spriteBatch.Begin();
            _currentContext.Draw(ref _spriteBatch);
            _spriteBatch.End();
        }

        private void ReloadContent() {
            Content.Unload();
            LoadContent();
            Initialize();
        }
    }
}