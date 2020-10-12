using System;
using System.Collections.Generic;
using System.Linq;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;


namespace Game.GameContext{
    public abstract class Context {
        private readonly Stack<UserInterface> _userInterfaces = new Stack<UserInterface>();
        protected SpriteBatch _spriteBatch = null;
        protected readonly Desktop Desktop = new Desktop();
        //This allow to manipulate game parameters  within context 
        //needs to be set up before using this class
        public static Microsoft.Xna.Framework.Game Game;

        protected Context(UserInterface userInterface){
            AddNewUI(userInterface);
        }

        public virtual void Update(GameTime gameTime){
            _userInterfaces.Peek().Update(gameTime);
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
        
        protected void AddNewUI(UserInterface userInterface){
            _userInterfaces.Push(userInterface);
            userInterface.OnContextChangeRequest += (sender, args) => RequestContext(args.Context);
            userInterface.OnNextUIRequest += (sender, args) => AddNewUI(args.Interface);
            userInterface.OnPreviousUIRequest += (sender, args) => RemoveUI();
            Desktop.Root = userInterface;
        }

        protected void RemoveUI(){
            if (_userInterfaces.Count <= 1) return;
            _userInterfaces.Pop();
            Desktop.Root = _userInterfaces.Peek();
        }
        
        public event EventHandler<ContextChangeRequested> OnContextChangeRequest;

        protected void RequestContext(Context context){
            OnContextChangeRequest?.Invoke(this, new ContextChangeRequested(context));
        }
        
    }
}