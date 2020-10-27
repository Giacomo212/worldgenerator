using Microsoft.Xna.Framework;
using System;
using Myra;
using System.IO;
using Game.Configs;
using Game.GameContext;
using Game.UI;
using Microsoft.Xna.Framework.Graphics;
using Types;


namespace Game{
    public class WorldGenerator : Microsoft.Xna.Framework.Game{
        private readonly GraphicsDeviceManager _graphics;
        private Context _currentContext;

        public WorldGenerator(){
            var separator = Path.DirectorySeparatorChar;
            Directory.CreateDirectory(EnvironmentVariables.GameFiles);
            Directory.CreateDirectory(EnvironmentVariables.GameFiles + $"{separator}worlds");
            Context.Game = this;
            MyraEnvironment.Game = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            _graphics.PreferredBackBufferWidth = GameConfig.Config.Resolution.Width;
            _graphics.PreferredBackBufferHeight = GameConfig.Config.Resolution.Hight;
            _graphics.IsFullScreen = GameConfig.Config.Resolution.IsFullScreen;
            _graphics.ApplyChanges();
        }

        protected override void Initialize(){
            base.Initialize();
            Keyboard.Initialize();
        }

        protected override void LoadContent(){
            _currentContext = new StartingScreenContext(new MainUi());
            _currentContext.Load();
            _currentContext.ContextChangeRequest += CurrentContextContextChangeRequest;
            _currentContext.ExitRequest += (sender, args) => Exit();
            _currentContext.GoFullScreenRequest += (sender, args) => ScreenChangeRequest();
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void CurrentContextContextChangeRequest(object senderR, ContextChangeRequestedArgs e){
            Content.Unload();
            _currentContext.Unload();
            _currentContext = e.Context;
            _currentContext.Load();
            _currentContext.Initialize();
            _currentContext.ContextChangeRequest += CurrentContextContextChangeRequest;
            _currentContext.ExitRequest += (sender, args) => Exit();
            _currentContext.GoFullScreenRequest += (sender, args) => ScreenChangeRequest();
        }

        protected override void UnloadContent(){
            Content.Unload();
        }

        protected override void Update(GameTime gameTime){
            Keyboard.UpdateState();
            _currentContext.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.Black);
            _currentContext.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e){
            
            GameConfig.Config.Resolution = new Resolution(Window.ClientBounds.Width, Window.ClientBounds.Height,
                _graphics.IsFullScreen);
            _currentContext.OnWindowResize();
        }

        private void ScreenChangeRequest(){
            if (GameConfig.Config.Resolution.IsFullScreen){
                _graphics.PreferredBackBufferWidth = 800;
                _graphics.PreferredBackBufferHeight = 600;
                _graphics.IsFullScreen = false;
                _graphics.ApplyChanges();
            }
            else{
                _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                _graphics.IsFullScreen = true;
                _graphics.ApplyChanges();
            }
                
        }
        
        protected override void OnExiting(object sender, EventArgs args){
            base.OnExiting(sender, args);
            GameConfig.Save();
        }
    }
}