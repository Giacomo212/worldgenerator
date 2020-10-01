using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game.UI;
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
            
            _spriteBatch = new SpriteBatch(Context.Game.GraphicsDevice);
             _map = map;
             var pos = new Position(GameConfig.Config.Resolution.Width/Chunk.PixelSize + 1 ,GameConfig.Config.Resolution.Hight/Chunk.PixelSize + 2 );
             _chunkController = new ChunkController(pos,map );
             _cameraController = new CameraController(_map);
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
            _chunkController.Dispose();
            var pos = new Position(GameConfig.Config.Resolution.Width/Chunk.PixelSize + 1 ,GameConfig.Config.Resolution.Hight/Chunk.PixelSize + 2 );
            _chunkController = new ChunkController(pos,_map);
            _cameraController = new CameraController(_map);
        }

        public override void Unload(){
            _chunkController.Dispose();
        }

        public override Context Update(GameTime gameTime){
            if (Keyboard.IsPressed(Keys.Left) && _cameraController.MoveLeft())
                _chunkController.MoveLeft();
            else if (Keyboard.IsPressed(Keys.Right) && _cameraController.MoveRight())
                _chunkController.MoveRight();
            // move map vertically 
            if (Keyboard.IsPressed(Keys.Up) && _cameraController.MoveUp())
                _chunkController.MoveUp();
            else if (Keyboard.IsPressed(Keys.Down) && _cameraController.MoveDown())
                _chunkController.MoveDown();
            return Keyboard.HasBeenPressed(Keys.Escape) ? new StartingScreenContext() : null;
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
            var offset = new Vector2(_cameraController.VectorX - Chunk.PixelSize,_cameraController.VectorY - Chunk.PixelSize);
            var chunks = _chunkController.Chunks;
            for (int i = 0; i < chunks.GetLength(0); i++){
                for (int j = 0; j < chunks.GetLength(1); j++){
                    DrawChunk(ref chunks[i,j],offset);
                    offset.Y += Chunk.Size * Block.Size;
                }
                offset.Y = _cameraController.VectorY - Chunk.PixelSize;
                offset.X += Chunk.Size * Block.Size;
            }
           
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

        // public void MoveLeft(){
        //     _cameraController.MoveLeft();
        // }
        //
        // public void MoveRight(){
        //     _cameraController.MoveRight();
        // }
        //
        // public void MoveUp(){
        //     _cameraController.MoveUp();
        // }
        //
        // public void MoveDown(){
        //     _cameraController.MoveDown();
        // }
    }
}