using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WorldGenerator.Configs;
using WorldGenerator.UI;
using WorldGenerator.Utils;
using Block = WorldGenerator.WorldMap.Block;
using BlockType = WorldGenerator.WorldMap.BlockType;
using Chunk = WorldGenerator.WorldMap.Chunk;
using ItemType = WorldGenerator.WorldMap.ItemType;


namespace WorldGenerator.GameContext{
    public class MapContext : Context{
        private WorldMap.Map _map;
        private Dictionary<WorldMap.BlockType, Texture2D> _blockDictionary = new Dictionary<WorldMap.BlockType, Texture2D>();

        private Dictionary<WorldMap.ItemType, Texture2D> _itemDictionary = new Dictionary<WorldMap.ItemType, Texture2D>();

        //textures
        private Texture2D _grass;
        private Texture2D _sand;
        private Texture2D _water;
        private Texture2D _dirt;
        private Texture2D _snow;
        private Texture2D _tree;
        private Texture2D _stone;

        //Map management
        private CameraController _cameraController;
        private ChunkController _chunkController;

        private Position _start = Position.Zero;

        public MapContext(WorldMap.Map map, UserInterface userInterface) : base(userInterface){
            _spriteBatch = new SpriteBatch(Context.Game.GraphicsDevice);
            _map = map;
            SetUpController();
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
            _grass = Game.Content.Load<Texture2D>("grass32");
            _sand = Game.Content.Load<Texture2D>("sand32");
            _water = Game.Content.Load<Texture2D>("water32");
            _dirt = Game.Content.Load<Texture2D>("dirt");
            _snow = Game.Content.Load<Texture2D>("snow");
            _stone = Game.Content.Load<Texture2D>("stone32");
            _tree = Game.Content.Load<Texture2D>("tree");
        }

        public override void OnWindowResize(){
            _chunkController.Dispose();
            SetUpController();
        }

        private void SetUpController(){
            var a = GameConfig.Config.Resolution.Width / Chunk.PixelSize;
            _chunkController =
                new ChunkController(
                    new Position(GameConfig.Config.Resolution.Width / Chunk.PixelSize + 2,
                        GameConfig.Config.Resolution.Hight / Chunk.PixelSize + 2), _map);
            _cameraController = new CameraController(_map);
        }

        // ~MapContext(){
        //     _chunkController.Dispose();
        // }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveLeft) && _cameraController.MoveLeft())
                _chunkController.MoveLeft();
            else if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveRight) && _cameraController.MoveRight())
                _chunkController.MoveRight();
            // move map vertically 
            if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveUp) && _cameraController.MoveUp())
                _chunkController.MoveUp();
            else if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveDown) && _cameraController.MoveDown())
                _chunkController.MoveDown();
            if (ExtendedKeyboard.HasBeenPressed(Keys.Escape)){
                RequestContext(new StartingScreenContext(new MainUi()));
            }
        }

        private Texture2D ParseBlock(WorldMap.BlockType type){
            _blockDictionary.TryGetValue(type, out var tmp);
            return tmp;
        }

        private Texture2D ParseItem(WorldMap.ItemType type){
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
            var offset = new Vector2(_cameraController.VectorX - Chunk.PixelSize,
                _cameraController.VectorY - Chunk.PixelSize);
            var chunks = _chunkController.Chunks;

            for (var i = 0; offset.X <= GameConfig.Config.Resolution.Width; i++){
                for (var j = 0; offset.Y <= GameConfig.Config.Resolution.Hight; j++){
                    DrawChunk(ref chunks[i, j], offset);
                    offset.Y += Chunk.BlockCount * Block.PixelSize;
                }

                offset.Y = _cameraController.VectorY - Chunk.PixelSize;
                offset.X += Chunk.BlockCount * Block.PixelSize;
            }
        }

        private void DrawChunk(ref WorldMap.Chunk chunk, Vector2 offset){
            var t = offset.Y;
            for (var i = 0; i < Chunk.BlockCount; i++){
                for (var j = 0; j < Chunk.BlockCount; j++){
                    _spriteBatch.Draw(ParseBlock(chunk[i, j].BlockType), offset, Color.White);
                    if (chunk[i, j].ItemType != ItemType.None)
                        _spriteBatch.Draw(_tree, offset, Color.White);
                    offset.Y += Block.PixelSize;
                }

                offset.Y = t;
                offset.X += Block.PixelSize;
            }
        }

        public override void Unload(){
            _chunkController.Dispose();
        }
    }
}