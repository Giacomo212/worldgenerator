using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using WorldGenerator.Configs;
using WorldGenerator.EventArg;
using WorldGenerator.GameScreen;
using WorldGenerator.UI;
using WorldGenerator.Utils;

namespace WorldGenerator{
    public class MainLoop : Game{
        private readonly GraphicsDeviceManager _graphics;
        private Screen _currentScreen;

        public MainLoop(){
            Screen.Game = this;
            MyraEnvironment.Game = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            _graphics.PreferredBackBufferWidth = GameConfig.Config.Resolution.Width;
            _graphics.PreferredBackBufferHeight = GameConfig.Config.Resolution.Hight;
            _graphics.IsFullScreen = GameConfig.Config.Resolution.IsFullScreen;
            _graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;
            //TargetElapsedTime = new TimeSpan(-);
            _graphics.ApplyChanges();
            
        }

        protected override void Initialize(){
            base.Initialize();
            ExtendedKeyboard.Initialize();
        }

        protected override void LoadContent(){
            _currentScreen = new StartingScreen(new MainUi());
            _currentScreen.Load();
            _currentScreen.ContextChangeRequest += CurrentScreenScreenChangeRequest;
            _currentScreen.ExitRequest += (sender, args) => Exit();
            _currentScreen.GoFullScreenRequest += (sender, args) => ScreenChangeRequest();
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void CurrentScreenScreenChangeRequest(object senderR, ContextChangeRequestedArgs e){
            Content.Unload();
            _currentScreen.Unload();
            _currentScreen = e.Screen;
            _currentScreen.Load();
            _currentScreen.Initialize();
            _currentScreen.ContextChangeRequest += CurrentScreenScreenChangeRequest;
            _currentScreen.ExitRequest += (sender, args) => Exit();
            _currentScreen.GoFullScreenRequest += (sender, args) => ScreenChangeRequest();
        }

        protected override void UnloadContent(){
            Content.Unload();
        }

        protected override void Update(GameTime gameTime){
            ExtendedKeyboard.UpdateState();
            _currentScreen.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.Black);
            _currentScreen.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e){
            GameConfig.Config.Resolution = new Resolution(Window.ClientBounds.Width, Window.ClientBounds.Height,
                _graphics.IsFullScreen);
            _currentScreen.OnWindowResize();
        }

        private void ScreenChangeRequest(){
            if (GameConfig.Config.Resolution.IsFullScreen){
                _graphics.PreferredBackBufferWidth = 1280;
                _graphics.PreferredBackBufferHeight = 920;
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