﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldGenerator.Configs;
using WorldGenerator.UI;
using Block = WorldGenerator.WorldMap.Block;


namespace WorldGenerator.GameContext{
    public class StartingScreenContext : Context{
        
        private Texture2D _filler;
        
        public override void Draw(GameTime gameTime){
            var vector = new Vector2(0, 0);
            _spriteBatch.Begin();
            for (; vector.X < GameConfig.Config.Resolution.Width; vector.X += Block.PixelSize){
                for (vector.Y = 0; vector.Y < GameConfig.Config.Resolution.Hight; vector.Y += Block.PixelSize){
                    _spriteBatch.Draw(_filler, vector, Color.White);
                }
            }
            _spriteBatch.End();
            Desktop.Render();
        }
        
        public override void Load(){
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _filler = Game.Content.Load<Texture2D>("dirt");
        }

        public StartingScreenContext(UserInterface userInterface) : base(userInterface){
            
        }
    }
}