using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using WorldGenerator.EventArg;
using WorldGenerator.UI;
using WorldGenerator.Utils;

namespace WorldGenerator.GameScreen{
    public abstract class Screen {
        private readonly Stack<UserInterface> _userInterfaces = new Stack<UserInterface>();
        protected SpriteBatch _spriteBatch = null;
        protected readonly Desktop Desktop = new Desktop();
        //This allow to manipulate game parameters  within context 
        //needs to be set up before using this class
        public static Game Game;

        protected Screen(UserInterface userInterface){
            AddNewUI(userInterface);
        }

        public virtual void Update(GameTime gameTime){
            _userInterfaces.Peek().Update(gameTime);
            if (ExtendedKeyboard.HasBeenPressed(Keys.Escape) && _userInterfaces.Peek().CanBeCanceled)
                RemoveUI();
        }
        
        protected void AddNewUI(UserInterface userInterface){
            _userInterfaces.Push(userInterface);
            userInterface.ContextChangeRequest += (sender, args) => RequestContext(args.Screen);
            userInterface.NextUIRequest += (sender, args) => AddNewUI(args.Interface);
            userInterface.PreviousUIRequest += (sender, args) => RemoveUI();
            userInterface.PreviousUIAndLoadRequest += (sender, args) => {
                RemoveUI();
                AddNewUI(args.Interface);
            };
            userInterface.ExitRequest += (sender, args) => OnExitRequest();
            userInterface.RequestFullScreen += (sender, args) => OnGoFullScreenRequest(); 
            Desktop.Root = userInterface;
        }

        protected void RemoveUI(){
            if (_userInterfaces.Count <= 1) return;
            _userInterfaces.Pop();
            Desktop.Root = _userInterfaces.Peek();
            _userInterfaces.Peek().Awake();
            
        }
        
        public event EventHandler<ContextChangeRequestedArgs> ContextChangeRequest;
        public event EventHandler GoFullScreenRequest;
        public event EventHandler ExitRequest;

        protected virtual void RequestContext(Screen screen){
            ContextChangeRequest?.Invoke(this, new ContextChangeRequestedArgs(screen));
        }

        protected virtual void OnExitRequest(){
            ExitRequest?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnGoFullScreenRequest(){
            GoFullScreenRequest?.Invoke(this, EventArgs.Empty);
        }
        
        public abstract void Draw(GameTime gameTime);

        public virtual void Initialize(){
            
        }

        //this method allow to react context to windows resize event
        public virtual void OnWindowResize(){
        }

        public virtual void Load(){
        }

        public virtual void Unload(){
            
        }
    }
}