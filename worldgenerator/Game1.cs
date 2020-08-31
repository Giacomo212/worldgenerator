using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Myra;

namespace worldgenerator {
    
    public class Game1 : Game {
        
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Context _currentContext;
       
        public Game1(){
            Context.Game = this;
            MyraEnvironment.Game = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _currentContext = new MainUiContext();
            
        }


        protected override void Initialize() {
            base.Initialize();
            _currentContext.Initialize();
            Keyboard.Initialize();
            
          
            graphics.PreferredBackBufferWidth = GameConfig.Config.Resolution.Width;
            graphics.PreferredBackBufferHeight = GameConfig.Config.Resolution.Hight;
            graphics.IsFullScreen = GameConfig.Config.Resolution.IsFullScreen;
            graphics.ApplyChanges();

        }


        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentContext.Load();
        }

        protected override void UnloadContent() {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime) {
            Keyboard.UpdateState();
            switch (_currentContext.Update(gameTime)) {
                case Action.ChangeToNewMap:
                    _currentContext = new MapContext(200,200);
                    //_currentContext.Load();
                    ReloadContent();
                    break;
                case Action.ChangeToMap:
                    _currentContext = new MapContext("./map.wg");
                    //_currentContext.Load();
                    ReloadContent();
                    break;
                case Action.Quit: Exit(); break;
                case Action.ChangeToMainUi:
                    _currentContext = new MainUiContext();
                    //_currentContext.Load();
                    ReloadContent();
                    break;
                case Action.None: break;

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