using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using World;
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
        private ChunkController _chunkController;

        public MapContext(Map map){
             _map = map;
             _chunkController = new ChunkController(new Position(4,4),map );
             _cameraController = new CameraController((int) map.WorldType, (int) map.WorldType,
                (GameConfig.Config.Resolution.Width / Block.Size) + 2,
                (GameConfig.Config.Resolution.Hight / Block.Size) + 2);
        }


        public override void Draw(GameTime gameTime){
            _spriteBatch.Begin();
            DrawMap();
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
            _cameraController = new CameraController((int) _map.WorldType, (int) _map.WorldType,
                (GameConfig.Config.Resolution.Width / Block.Size) + 2,
                (GameConfig.Config.Resolution.Hight / Block.Size) + 2);
        }

        public override IAction Update(GameTime gameTime){
            // //move map horizontally 
            // if (Keyboard.IsPressed(Keys.Left))
            //     _cameraController.MoveLeft();
            // else if (Keyboard.IsPressed(Keys.Right))
            //     _cameraController.MoveRight();
            // // move map vertically 
            // if (Keyboard.IsPressed(Keys.Up))
            //     _cameraController.MoveUp();
            // else if (Keyboard.IsPressed(Keys.Down))
            //     _cameraController.MoveDown();
            //move map horizontally 
            if (Keyboard.HasBeenPressed(Keys.Left))
                _chunkController.MoveLeft();
            else if (Keyboard.HasBeenPressed(Keys.Right))
                _chunkController.MoveRight();
            // move map vertically 
            if (Keyboard.HasBeenPressed(Keys.Up))
                _chunkController.MoveUp();
            else if (Keyboard.HasBeenPressed(Keys.Down))
                _chunkController.MoveDown();
            return Keyboard.HasBeenPressed(Keys.Escape) ? new ChangeToMainUi() : null;
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
            _blockDictionary.Add(BlockType.Snow, _snow);
            _blockDictionary.Add(BlockType.Stone, _stone);
        }

        private void InitializeItemDirectory(){
            _itemDictionary.Add(ItemType.Tree, _tree);
        }

        private void DrawMap(){
            var offset = Vector2.Zero;
            var chunks = _chunkController.Chunks;
            for (int i = 0; i < chunks.GetLength(0); i++){
                for (int j = 0; j < chunks.GetLength(1); j++){
                    DrawChunk(ref chunks[i,j],offset);
                    offset.Y += Chunk.Size * Block.Size;
                }
                offset.Y = 0;
                offset.X += Chunk.Size * Block.Size;
            }
            // var yZero = -_dirt.Height + _cameraController.VectorY;
            // var tmp = new Vector2(-_dirt.Width + _cameraController.VectorX, yZero);
            //
            // for (int i = _cameraController.ViewBeginningPointerX; i < _cameraController.ViewEndPointerX; i++){
            //     for (int j = _cameraController.ViewBeginningPointerY; j < _cameraController.ViewEndPointerY; j++){
            //         spriteBatch.Draw(ParseBlock(_map[i, j].BlockType), tmp, Color.White);
            //         if (_map[i, j].ItemType != ItemType.None){
            //             spriteBatch.Draw(ParseItem(_map[i, j].ItemType), tmp, Color.White);
            //         }
            //         tmp.Y += Block.High;
            //     }
            //
            //     tmp.X += Block.Width;
            //     tmp.Y = yZero;
            // }
        }

        private void DrawChunk(ref Chunk chunk, Vector2 offset){
            var t = offset.Y;
            for (int i = 0; i < Chunk.Size; i++){
                for (int j = 0; j < Chunk.Size; j++){
                    _spriteBatch.Draw(ParseBlock(chunk[i,j].BlockType), offset, Color.White);
                    if (chunk[i,j].ItemType != ItemType.None)
                        _spriteBatch.Draw(ParseItem(chunk[i,j].ItemType), offset, Color.White);
                    offset.Y += Block.Size;
                }

                offset.Y = t;
                offset.X += Block.Size;
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