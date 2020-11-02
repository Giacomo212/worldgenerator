using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WorldGenerator.Configs;
using WorldGenerator.MapElements;
using WorldGenerator.MapHandlers;
using WorldGenerator.UI;
using WorldGenerator.Utils;
using Block = WorldGenerator.MapElements.Block;
using BlockType = WorldGenerator.MapElements.BlockType;
using Chunk = WorldGenerator.MapElements.Chunk;
using ItemType = WorldGenerator.MapElements.ItemType;


namespace WorldGenerator.GameContext{
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
        private Texture2D _snowTree;
        private Texture2D _berry;
        private Texture2D _bush;
        private Texture2D _deadBush;

        private Texture2D _cactus;

        //Map management
        private CameraController _cameraController;
        private ChunkLoader _chunkLoader;

        public MapContext(Map map, UserInterface userInterface) : base(userInterface){
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
            _grass = Game.Content.Load<Texture2D>("grass");
            _sand = Game.Content.Load<Texture2D>("sand");
            _water = Game.Content.Load<Texture2D>("water");
            _dirt = Game.Content.Load<Texture2D>("dirt");
            _snow = Game.Content.Load<Texture2D>("snow");
            _stone = Game.Content.Load<Texture2D>("stone");
            _tree = Game.Content.Load<Texture2D>("tree");
            _snowTree = Game.Content.Load<Texture2D>("tree snow");;
            _berry = Game.Content.Load<Texture2D>("berry");;
            _bush = Game.Content.Load<Texture2D>("bush");;
            _deadBush = Game.Content.Load<Texture2D>("deadBush");;
            _cactus = Game.Content.Load<Texture2D>("cactus");;
        }

        public override void OnWindowResize(){
            //_chunkLoader.Dispose();
            //SetUpController();
            _chunkLoader.ChangeBuffer(new Position(GameConfig.Config.Resolution.Width / Chunk.PixelSize + 2,
                GameConfig.Config.Resolution.Hight / Chunk.PixelSize + 2));
        }

        private void SetUpController(){
            var a = GameConfig.Config.Resolution.Width / Chunk.PixelSize;
            _chunkLoader =
                new ChunkLoader(
                    new Position(GameConfig.Config.Resolution.Width / Chunk.PixelSize + 2,
                        GameConfig.Config.Resolution.Hight / Chunk.PixelSize + 2), _map);
            _cameraController = new CameraController(_map);
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveLeft) && _cameraController.MoveLeft())
                _chunkLoader.MoveLeft();
            else if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveRight) &&
                     _cameraController.MoveRight())
                _chunkLoader.MoveRight();
            // move map vertically 
            if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveUp) && _cameraController.MoveUp())
                _chunkLoader.MoveUp();
            else if (ExtendedKeyboard.IsPressed(GameConfig.Config.KeyboardMap.MoveDown) && _cameraController.MoveDown())
                _chunkLoader.MoveDown();
            if (ExtendedKeyboard.HasBeenPressed(Keys.Escape)){
                RequestContext(new StartingScreenContext(new MainUi()));
            }
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
            var offset = new Vector2(_cameraController.VectorX - Chunk.PixelSize,
                _cameraController.VectorY - Chunk.PixelSize);
            var chunks = _chunkLoader.Chunks;

            for (var i = 0; offset.X <= GameConfig.Config.Resolution.Width; i++){
                for (var j = 0; offset.Y <= GameConfig.Config.Resolution.Hight; j++){
                    DrawChunk(ref chunks[i, j], offset);
                    offset.Y += Chunk.BlockCount * Block.PixelSize;
                }

                offset.Y = _cameraController.VectorY - Chunk.PixelSize;
                offset.X += Chunk.BlockCount * Block.PixelSize;
            }
        }

        private void DrawChunk(ref Chunk chunk, Vector2 offset){
            var t = offset.Y;
            for (var i = 0; i < Chunk.BlockCount; i++){
                for (var j = 0; j < Chunk.BlockCount; j++){
                    _spriteBatch.Draw(ParseBlock(chunk[i, j].BlockType), offset, Color.White);
                    if (chunk[i, j].ItemType != ItemType.None)
                        _spriteBatch.Draw(ParseItem(chunk[i, j].ItemType), offset, Color.White);
                    offset.Y += Block.PixelSize;
                }

                offset.Y = t;
                offset.X += Block.PixelSize;
            }
        }

        public override void Unload(){
            _chunkLoader.Dispose();
        }
    }
}