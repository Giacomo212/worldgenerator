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
        //private float _mapOffSetX = 0f;
        //private float _mapOffSetY = 0f;
        
        public MapContext(int x,int y) {
            TextureToLoad = new Content<Texture2D>[4];
            TextureToLoad[0] = new Content<Texture2D>("grass");
            TextureToLoad[1] = new Content<Texture2D>("sand");
            TextureToLoad[2] = new Content<Texture2D>("water");
            TextureToLoad[3] = new Content<Texture2D>("dirt");
            map = new Map(x, y, (GameConfig.config.Resolution.Width / Block.Width) + 1, (GameConfig.config.Resolution.Hight / Block.High) + 1);
            map.Save("map.wg");
            InitializeGrid();
        }
        public MapContext(string filename) {
            TextureToLoad = new Content<Texture2D>[4];
            TextureToLoad[0] = new Content<Texture2D>("grass");
            TextureToLoad[1] = new Content<Texture2D>("sand");
            TextureToLoad[2] = new Content<Texture2D>("water");
            TextureToLoad[3] = new Content<Texture2D>("dirt");
            map = new Map(filename,(GameConfig.config.Resolution.Width / Block.Width) +1, (GameConfig.config.Resolution.Hight / Block.High) + 1);
            InitializeGrid();
        }

        public override void Draw(ref SpriteBatch spriteBatch) {
            DrawMap(ref spriteBatch);
        }

        public override void Initialize() {
            InitializeDictonary();
            
        }

        public override int Update(GameTime gameTime) {

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
                return 4;
            }
            return -1;
        }
        private Texture2D PraseBlock(int ID) {
            Texture2D tmp;
            blockDictionary.TryGetValue(ID, out tmp);
            return tmp;
        }
        private void InitializeDictonary() {
            blockDictionary.Add(0, TextureToLoad[0].Value);
            blockDictionary.Add(1, TextureToLoad[1].Value);
            blockDictionary.Add(2, TextureToLoad[2].Value);
            blockDictionary.Add(3, TextureToLoad[3].Value);
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