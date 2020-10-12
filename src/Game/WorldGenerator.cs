using Microsoft.Xna.Framework;
using System;
using Myra;
using System.IO;
using Game.GameContext;
using Game.UI;
using Game.WorldMap;
using Types;


namespace Game{
    public class WorldGenerator : Microsoft.Xna.Framework.Game{
        private GraphicsDeviceManager _graphics;
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
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            ImageGenerator.GenerateImage(MapReader.ReadMap("World"));
        }

        protected override void Initialize(){
            base.Initialize();
            Keyboard.Initialize();
            GameConfig.Config.Save();
            _graphics.PreferredBackBufferWidth = GameConfig.Config.Resolution.Width;
            _graphics.PreferredBackBufferHeight = GameConfig.Config.Resolution.Hight;
            _graphics.IsFullScreen = GameConfig.Config.Resolution.IsFullScreen;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent(){
            _currentContext = new StartingScreenContext(new MainUi());
            _currentContext.Load();
            _currentContext.OnContextChangeRequest += CurrentContextOnContextChangeRequest;
        }

        private void CurrentContextOnContextChangeRequest(object sender, ContextChangeRequested e){
            Content.Unload();
            _currentContext.Unload();
            _currentContext = e.Context;
            _currentContext.Load();
            _currentContext.Initialize();
            _currentContext.OnContextChangeRequest += CurrentContextOnContextChangeRequest;
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
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();
            GameConfig.Config.Resolution = new Resolution(Window.ClientBounds.Width, Window.ClientBounds.Height, false);
            _currentContext.OnWindowResize();
        }

    }
}