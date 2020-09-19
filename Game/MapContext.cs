using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Types;

namespace Game{
    public class MapContext : Context{
        private Map _map;
        private Dictionary<BlockType, Texture2D> _blockDictionary = new Dictionary<BlockType, Texture2D>();
        private Dictionary<ItemType, Texture2D> _itemDictionary = new Dictionary<ItemType, Texture2D>();
        //textures
        private Texture2D _grass;
        private Texture2D _sand;
        private Texture2D _water;
        private Texture2D _dirt;
        private Texture2D _snow;
        private Texture2D _tree;
        private Texture2D _stone;
        
        private CameraController _cameraController;
        //private ChunkController _chunkController;

        public MapContext(Map map){
            _map = map;
            
            _cameraController = new CameraController((int)map.WorldType, (int)map.WorldType, (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }
        
      

        public override void Draw(GameTime gameTime){
            _spriteBatch.Begin();
            DrawMap(ref _spriteBatch);
            _spriteBatch.End();
        }

        public override void Initialize(){
            InitializeBlockDirectory();
            InitializeItemDirectory();
        }

        public override void Load(){
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _grass = Game.Content.Load<Texture2D>("grass");
            _sand = Game.Content.Load<Texture2D>("sand");
            _water = Game.Content.Load<Texture2D>("water");
            _dirt = Game.Content.Load<Texture2D>("dirt");
            _snow = Game.Content.Load<Texture2D>("snow");
            _stone = Game.Content.Load<Texture2D>("stone");
            _tree = Game.Content.Load<Texture2D>("tree");
        }

        public override void OnWindowResize(){
            _cameraController = new CameraController((int)_map.WorldType, (int)_map.WorldType,
                (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }

        public override IAction Update(GameTime gameTime){
            //move map horizontally 
            if (Keyboard.IsPressed(Keys.Left))
                _cameraController.MoveLeft();
            else if (Keyboard.IsPressed(Keys.Right))
                _cameraController.MoveRight();
            // move map vertically 
            if (Keyboard.IsPressed(Keys.Up))
                _cameraController.MoveUp();
            else if (Keyboard.IsPressed(Keys.Down))
                _cameraController.MoveDown();
            return Keyboard.IsPressed(Keys.Escape) ? new ChangeToMainUi() : null;
        }

        private Texture2D ParseBlock(BlockType type){
            _blockDictionary.TryGetValue(type, out var tmp);
            return tmp;
        }
        private Texture2D ParseItem(ItemType type){
            _itemDictionary.TryGetValue(type, out var tmp);
            return tmp;
        }
        private void InitializeBlockDirectory(){
            _blockDictionary.Add(BlockType.Grass, _grass);
            _blockDictionary.Add(BlockType.Sand, _sand);
            _blockDictionary.Add(BlockType.Water, _water);
            _blockDictionary.Add(BlockType.Dirt, _dirt);
            _blockDictionary.Add(BlockType.Snow,_snow);
            _blockDictionary.Add(BlockType.Stone,_stone);
        }

        private void InitializeItemDirectory(){
            _itemDictionary.Add(ItemType.Tree, _tree);
        }
        private void DrawMap(ref SpriteBatch spriteBatch){
            var yZero = -_dirt.Height + _cameraController.VectorY;
            var tmp = new Vector2(-_dirt.Width + _cameraController.VectorX, yZero);

            for (int i = _cameraController.ViewBeginningPointerX; i < _cameraController.ViewEndPointerX; i++){
                for (int j = _cameraController.ViewBeginningPointerY; j < _cameraController.ViewEndPointerY; j++){
                    spriteBatch.Draw(ParseBlock(_map[i, j].BlockType), tmp, Color.White);
                    if (_map[i, j].ItemType != ItemType.None){
                        spriteBatch.Draw(ParseItem(_map[i, j].ItemType), tmp, Color.White);
                    }
                    tmp.Y += Block.High;
                }

                tmp.X += Block.Width;
                tmp.Y = yZero;
            }
        }

        public void MoveLeft(){
            _cameraController.MoveLeft();
        }
        public void MoveRight(){
            _cameraController.MoveRight();
        }

        public void MoveUp(){
            _cameraController.MoveUp();
        }

        public void MoveDown(){
            _cameraController.MoveDown();
        }
    }
}