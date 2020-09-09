using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Myra;
using System.IO;

namespace worldgenerator {
    
    public class Game1 : Game {
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Context _currentContext;
       
        public Game1(){
            var separator = Path.DirectorySeparatorChar;
            Directory.CreateDirectory(GameConfig.Config.GameFilesPath);
            Directory.CreateDirectory(GameConfig.Config.GameFilesPath + $"{separator}worlds");
            Context.Game = this;
            MyraEnvironment.Game = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _currentContext = new MainUiContext();
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }


        protected override void Initialize() {
            base.Initialize();
            _currentContext.Initialize();
            Keyboard.Initialize();
            GameConfig.Config.Save();
            _graphics.PreferredBackBufferWidth = GameConfig.Config.Resolution.Width;
            _graphics.PreferredBackBufferHeight = GameConfig.Config.Resolution.Hight;
            _graphics.IsFullScreen = GameConfig.Config.Resolution.IsFullScreen;
            _graphics.ApplyChanges();
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
            var action = _currentContext.Update(gameTime);
            if (action != null){
                _currentContext = action.ReturnNewContext();
                ReloadContent();
            }
                
            base.Update(gameTime);
            // switch () {
            //     case Action.ChangeToNewMap:
            //         _currentContext = new MapContext(200,200);
            //         //_currentContext.Load();
            //         ReloadContent();
            //         break;
            //     case Action.ChangeToMap:
            //         _currentContext = new MapContext("./map.wg");
            //         //_currentContext.Load();
            //         ReloadContent();
            //         break;
            //     case Action.Quit: Exit(); break;
            //     case Action.ChangeToMainUi:
            //         _currentContext = new MainUiContext();
            //         //_currentContext.Load();
            //         ReloadContent();
            //         break;
            //     case Action.None: break;
            //
            // }
            
        }

       
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _currentContext.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void ReloadContent() {
            Content.Unload();
            _currentContext.Load();
            _currentContext.Initialize();
        }
        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();
            GameConfig.Config. Resolution = new Resolution(Window.ClientBounds.Width,Window.ClientBounds.Height,false);
            _currentContext.OnWindowResize();
        }
    }
}