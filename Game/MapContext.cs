using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Generator;
namespace Game{
    public class MapContext : Context{
        private Map _map;
        private Dictionary<int, Texture2D> _blockDictionary = new Dictionary<int, Texture2D>();
        private Texture2D _grass;
        private Texture2D _sand;
        private Texture2D _water;
        private Texture2D _dirt;
        private Texture2D _snow;
        private CameraController _controller;

        public MapContext(int x, int y){
            _map = new Map(x, y);
            
            _controller = new CameraController(x, y, (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }
        public MapContext(int x, int y, string name){
            _map = new Map(x, y);
            _map.Save(name);
            _controller = new CameraController(x, y, (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }

        public MapContext(string filename){
            _map = new Map(filename);
            _controller = new CameraController(_map.Width, _map.Hight,
                (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }

        public override void Draw(GameTime gameTime){
            _spriteBatch.Begin();
            DrawMap(ref _spriteBatch);
            _spriteBatch.End();
        }

        public override void Initialize(){
            InitializeDictonary();
        }

        public override void Load(){
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _grass = Game.Content.Load<Texture2D>("grass");
            _sand = Game.Content.Load<Texture2D>("sand");
            _water = Game.Content.Load<Texture2D>("water");
            _dirt = Game.Content.Load<Texture2D>("dirt");
            _snow = Game.Content.Load<Texture2D>("snow");
        }

        public override void OnWindowResize(){
            _controller = new CameraController(_map.Width, _map.Hight,
                (GameConfig.Config.Resolution.Width / Block.Width) + 2,
                (GameConfig.Config.Resolution.Hight / Block.High) + 2);
        }

        public override IAction Update(GameTime gameTime){
            if (Keyboard.IsPressed(Keys.Left))
                _controller.MoveLeft();
            else if (Keyboard.IsPressed(Keys.Right))
                _controller.MoveRight();


            if (Keyboard.IsPressed(Keys.Up))
                _controller.MoveUp();
            else if (Keyboard.IsPressed(Keys.Down))
                _controller.MoveDown();

            if (Keyboard.IsPressed(Keys.Escape))
                return new ChangeToMainUi();


            return null;
        }

        private Texture2D PraseBlock(int id){
            _blockDictionary.TryGetValue(id, out var tmp);
            return tmp;
        }

        private void InitializeDictonary(){
            _blockDictionary.Add(0, _grass);
            _blockDictionary.Add(1, _sand);
            _blockDictionary.Add(2, _water);
            _blockDictionary.Add(3, _dirt);
        }

        private void DrawMap(ref SpriteBatch spriteBatch){
            var yZero = -_dirt.Height + _controller.VectorY;
            var tmp = new Vector2(-_dirt.Width + _controller.VectorX, yZero);

            for (int i = _controller.ViewBeginningPointerX; i < _controller.ViewEndPointerX; i++){
                for (int j = _controller.ViewBeginningPointerY; j < _controller.ViewEndPointerY; j++){
                    spriteBatch.Draw(PraseBlock(_map[i, j].BlockID), tmp, Color.White);
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