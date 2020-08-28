using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace worldgenerator {
    public class MapContext : Context {
        private Vector2[,] _displayGrid;
        private Map map;
        private Dictionary<int, Texture2D> blockDictionary = new Dictionary<int, Texture2D>();
        private Texture2D _grass;
        private Texture2D _sand;
        private Texture2D _water;
        private Texture2D _dirt;
        
        public MapContext(int x,int y) {
            map = new Map(x, y, (GameConfig.config.Resolution.Width / Block.Width) + 1, (GameConfig.config.Resolution.Hight / Block.High) + 1);
            map.Save("map.wg");
            InitializeGrid();
        }
        public MapContext(string filename) {
          
            map = new Map(filename,(GameConfig.config.Resolution.Width / Block.Width) +1, (GameConfig.config.Resolution.Hight / Block.High) + 1);
            InitializeGrid();
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

            if (Keyboard.IsPressed(Keys.Left)) {
                map.moveViewLeft();
            } else if (Keyboard.IsPressed(Keys.Right)) {
                //_mapOffSetX -= GameConfig.config.Sensivity;
                map.moveViewRight();
            }
            if (Keyboard.IsPressed(Keys.Up)) {
                map.moveViewUp();
                //_mapOffSetY += GameConfig.config.Sensivity;
            } else if (Keyboard.IsPressed(Keys.Down)) {
                map.moveViewDown();
                //_mapOffSetY -= GameConfig.config.Sensivity;
            }
            if (Keyboard.IsPressed(Keys.Escape)) {
                return Action.ChangeToMainUi;
            }

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
        private void InitializeGrid() {
            var x = (GameConfig.config.Resolution.Width / Block.Width) + 1;
            var y = (GameConfig.config.Resolution.Hight / Block.High) + 1;
            _displayGrid = new Vector2[x, y];
            for (int i = 0; i < x ; i++) {
                for (int j = 0; i < y; i++) {
                    _displayGrid[i, j] = new Vector2(i*Block.Width,j*Block.High);
                }
            }
        }
        private void DrawMap(ref SpriteBatch spriteBatch) {
            var tmp = new Vector2(0, 0);
            
            for (int i = map.ViewBeginningPointerX; i < map.ViewEndPointerX; i++) {
                for (int j = map.ViewBeginningPointerY; j < map.ViewEndPointerY; j++) {
                    spriteBatch.Draw(PraseBlock(map[i, j].BlockID), tmp, Color.White);
                    tmp.Y += Block.High;
                }
                tmp.X += Block.Width;
                tmp.Y = 0;
            }
        }

       
    }
}