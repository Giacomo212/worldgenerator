﻿using Microsoft.Xna.Framework;
using System;
using Myra;
using System.IO;
using Libraries;
using Types;

namespace Game {
    
    public class Game1 : Microsoft.Xna.Framework.Game {
        
        private GraphicsDeviceManager _graphics;
        private Context _currentContext;
       
        public Game1(){
            var separator = Path.DirectorySeparatorChar;
            Directory.CreateDirectory(EnvironmentVariables.GameFiles);
            Directory.CreateDirectory(EnvironmentVariables.GameFiles+ $"{separator}worlds");
            Context.Game = this;
            MyraEnvironment.Game = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _currentContext = new MainUiContext();
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
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


        protected override void LoadContent(){
            
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
        }

       
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            _currentContext.Draw(gameTime);
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