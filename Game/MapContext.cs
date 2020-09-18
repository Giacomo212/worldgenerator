using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Generator;
using Types;

namespace Game{
    public class MapContext : Context{
        private SurfaceMap m_surfaceMap;
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
        
        private CameraController _controller;
        

        public MapContext(WorldSize worldSize){
            m_surfaceMap = new SurfaceMap(worldSize);
            
            _controller = new CameraController((int)worldSize, (int)worldSize, (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }
        public MapContext(WorldSize worldSize, string name){
            m_surfaceMap = new SurfaceMap(worldSize);
            m_surfaceMap.Save(name);
            _controller = new CameraController((int)worldSize, (int)worldSize, (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }

        public MapContext(string filename){
            m_surfaceMap = new SurfaceMap(filename);
            _controller = new CameraController(m_surfaceMap.Width, m_surfaceMap.Hight,
                (GameConfig.Config.Resolution.Width / Block.Width) + 2,
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
            _controller = new CameraController(m_surfaceMap.Width, m_surfaceMap.Hight,
                (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }

        public override IAction Update(GameTime gameTime){
            //move map horizontally 
            if (Keyboard.IsPressed(Keys.Left))
                _controller.MoveLeft();
            else if (Keyboard.IsPressed(Keys.Right))
                _controller.MoveRight();
            // move map vertically 
            if (Keyboard.IsPressed(Keys.Up))
                _controller.MoveUp();
            else if (Keyboard.IsPressed(Keys.Down))
                _controller.MoveDown();
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
            var yZero = -_dirt.Height + _controller.VectorY;
            var tmp = new Vector2(-_dirt.Width + _controller.VectorX, yZero);

            for (int i = _controller.ViewBeginningPointerX; i < _controller.ViewEndPointerX; i++){
                for (int j = _controller.ViewBeginningPointerY; j < _controller.ViewEndPointerY; j++){
                    spriteBatch.Draw(ParseBlock(m_surfaceMap[i, j].BlockType), tmp, Color.White);
                    if (m_surfaceMap[i, j].ItemType != ItemType.None){
                        spriteBatch.Draw(ParseItem(m_surfaceMap[i, j].ItemType), tmp, Color.White);
                    }
                    tmp.Y += Block.High;
                }

                tmp.X += Block.Width;
                tmp.Y = yZero;
            }
        }

        public void MoveLeft(){
            _controller.MoveLeft();
        }
        public void MoveRight(){
            _controller.MoveRight();
        }

        public void MoveUp(){
            _controller.MoveUp();
        }

        public void MoveDown(){
            _controller.MoveDown();
        }
    }
}