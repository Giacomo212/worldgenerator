#nullable enable
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using WorldGenerator.Configs;

namespace WorldGenerator.UI{
    public class KeyBindCustomizerUI : UserInterface{
        private int _selectedButton = -1;
        private readonly TextButton _upButton;
        private readonly TextButton _downButton;
        private readonly TextButton _rightButton;
        private readonly TextButton _leftButton;

        public KeyBindCustomizerUI(){
            var upLabel = CrateLabel("Move Up: ", 0, 0);
            var downLabel = CrateLabel("Move Down: ", 1, 0);
            var leftLabel = CrateLabel("Move Left: ", 2, 0);
            var rightLabel = CrateLabel("Move Right: ", 3, 0);
            _widgets.Add(upLabel);
            _widgets.Add(downLabel);
            _widgets.Add(leftLabel);
            _widgets.Add(rightLabel);
            _upButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveUp.ToString(), 0, 1);
            _downButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveDown.ToString(), 1, 1);
            _rightButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveRight.ToString(), 2, 1);
            _leftButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveLeft.ToString(), 3, 1);
            _upButton.Click += (sender, args) => _selectedButton = _selectedButton == 0 ? -1 : 0;
            _downButton.Click += (sender, args) => _selectedButton = _selectedButton == 1 ? -1 : 1;
            _leftButton.Click += (sender, args) => _selectedButton = _selectedButton == 2 ? -1 : 2;
            _rightButton.Click += (sender, args) => _selectedButton = _selectedButton == 3 ? -1 : 3;
            _widgets.Add(_upButton);
            _widgets.Add(_downButton);
            _widgets.Add(_leftButton);
            _widgets.Add(_rightButton);
            var cancelButton = CrateBackButton(4, 1);
            cancelButton.Click += (sender, args) => RequestPreviousInterface();
            _widgets.Add(cancelButton);
        }

        public override void Update(GameTime gameTime){
            var map = KeyboardMap.GetPressedKeys();
            if (!map.Any() || map[0] == Keys.Escape) return;
            switch (_selectedButton){
                case 0:
                    ChangeUpButton(map[0]);
                    break;
                case 1:
                    ChangeDownButton(map[0]);
                    break;
                case 2:
                    ChangeLeftButton(map[0]);
                    break;
                case 3:
                    ChangeRightButton(map[0]);
                    break;
            }
        }

        private void ChangeUpButton(Keys key){
            GameConfig.Config.KeyboardMap.MoveUp = key;
            _selectedButton = -1;
            _upButton.Text = key.ToString();
        }


        private void ChangeDownButton(Keys key){
            GameConfig.Config.KeyboardMap.MoveDown = key;
            _selectedButton = -1;
            _downButton.Text = key.ToString();
        }

        private void ChangeLeftButton(Keys key){
            GameConfig.Config.KeyboardMap.MoveDown = key;
            _selectedButton = -1;
            _leftButton.Text = key.ToString();
        }

        private void ChangeRightButton(Keys key){
            GameConfig.Config.KeyboardMap.MoveDown = key;
            _selectedButton = -1;
            _rightButton.Text = key.ToString();
        }
    }
}