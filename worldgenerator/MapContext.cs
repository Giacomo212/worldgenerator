using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace worldgenerator {
    public class MapContext : Context {
        private Map _map;
        private Dictionary<int, Texture2D> blockDictionary = new Dictionary<int, Texture2D>();
        private Texture2D _grass;
        private Texture2D _sand;
        private Texture2D _water;
        private Texture2D _dirt;
        private CameraController _controller;
        public MapContext(int x,int y) {
            _map = new Map(x, y);
            _map.Save("map.wg");
            _controller = new CameraController(x,y, (GameConfig.Config.Resolution.Width / Block.Width) + 3, (GameConfig.Config.Resolution.Hight / Block.High) + 3);
            
        }
        public MapContext(string filename) {
            _map = new Map(filename);
            _controller = new CameraController(_map.Width,_map.Hight, (GameConfig.Config.Resolution.Width / Block.Width) + 3, (GameConfig.Config.Resolution.Hight / Block.High) + 3);
            
        }

        public override void Draw(ref SpriteBatch spriteBatch) {
            DrawMap(ref spriteBatch);
        }

        public override void Initialize() {
            InitializeDictonary();
            
        }

        public override void Load(){
            _grass = Game.Content.Load<Texture2D>("grass");
            _sand = Game.Content.Load<Texture2D>("sand");
            _water = Game.Content.Load<Texture2D>("water");
            _dirt = Game.Content.Load<Texture2D>("dirt");
        }

        public override Action Update(GameTime gameTime) {

            if (Keyboard.IsPressed(Keys.Left)) 
                _controller.MoveLeft();
            else if (Keyboard.IsPressed(Keys.Right))
                _controller.MoveRight();


            if (Keyboard.IsPressed(Keys.Up))
                _controller.MoveUp();
            else if (Keyboard.IsPressed(Keys.Down))
                _controller.MoveDown();
            
            if (Keyboard.IsPressed(Keys.Escape)) 
                return Action.ChangeToMainUi;
            

            return Action.None;
        }
        private Texture2D PraseBlock(int ID) {
            Texture2D tmp;
            blockDictionary.TryGetValue(ID, out tmp);
            return tmp;
        }
        private void InitializeDictonary() {
            blockDictionary.Add(0, _grass);
            blockDictionary.Add(1, _sand);
            blockDictionary.Add(2, _water);
            blockDictionary.Add(3, _dirt);
        }
        private void DrawMap(ref SpriteBatch spriteBatch){
            var yZero = -_dirt.Height + _controller.VectorY;
            var tmp = new Vector2(-_dirt.Width + _controller.VectorX, yZero);
            
             for (int i = _controller.ViewBeginningPointerX; i < _controller.ViewEndPointerX; i++) {
                 for (int j = _controller.ViewBeginningPointerY; j < _controller.ViewEndPointerY; j++) {
                     spriteBatch.Draw(PraseBlock(_map[i, j].BlockID), tmp, Color.White);
                     tmp.Y += Block.High;
                 }
                 tmp.X += Block.Width;
                 tmp.Y = yZero;
             }
        }

       
    }
}