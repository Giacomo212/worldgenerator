#nullable enable
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using WorldGenerator.Configs;

namespace WorldGenerator.UI{
    public class KeyBindCustomizerUI : UserInterface{
        private TypesOfButtons _selectedButton = TypesOfButtons.None;
        private readonly ToggleableButton _upButton;
        private readonly ToggleableButton _downButton;
        private readonly ToggleableButton _rightButton;
        private readonly ToggleableButton _leftButton;

        public KeyBindCustomizerUI(){
            var upLabel = CrateLabel("Move Up: ", 0, 0);
            var downLabel = CrateLabel("Move Down: ", 1, 0);
            var leftLabel = CrateLabel("Move Left: ", 2, 0);
            var rightLabel = CrateLabel("Move Right: ", 3, 0);
            _widgets.Add(upLabel);
            _widgets.Add(downLabel);
            _widgets.Add(leftLabel);
            _widgets.Add(rightLabel);
            _upButton = ToggleableButton.CrateButton(GameConfig.Config.KeyboardMap.MoveUp.ToString(), 0, 1);
            _downButton = ToggleableButton.CrateButton(GameConfig.Config.KeyboardMap.MoveDown.ToString(), 1, 1);
            _rightButton = ToggleableButton.CrateButton(GameConfig.Config.KeyboardMap.MoveRight.ToString(), 3, 1);
            _leftButton = ToggleableButton.CrateButton(GameConfig.Config.KeyboardMap.MoveLeft.ToString(), 2, 1);
            SetupButtons();
            var cancelButton = CrateBackButton(4, 1);
            cancelButton.Click += (sender, args) => RequestPreviousInterface();
            _widgets.Add(cancelButton);
        }

        private void SetupButtons(){
            _upButton.Click += (sender, args) => {
                _selectedButton = _selectedButton == TypesOfButtons.MoveUp
                    ? TypesOfButtons.None
                    : TypesOfButtons.MoveUp;
                ((ToggleableButton) sender).IsToggled = true;
            };
            _downButton.Click += (sender, args) => {
                _selectedButton = _selectedButton == TypesOfButtons.MoveDown
                    ? TypesOfButtons.None
                    : TypesOfButtons.MoveDown;
                ((ToggleableButton) sender).IsToggled = true;
            };
            _leftButton.Click += (sender, args) => {
                _selectedButton = _selectedButton == TypesOfButtons.MoveLeft
                    ? TypesOfButtons.None
                    : TypesOfButtons.MoveLeft;
                ((ToggleableButton) sender).IsToggled = true;
            };
            _rightButton.Click += (sender, args) => {
                _selectedButton = _selectedButton == TypesOfButtons.MoveRight
                    ? TypesOfButtons.None
                    : TypesOfButtons.MoveRight;
                ((ToggleableButton) sender).IsToggled = true;
            };
            _widgets.Add(_upButton);
            _widgets.Add(_downButton);
            _widgets.Add(_leftButton);
            _widgets.Add(_rightButton);
        }

        public override void Update(GameTime gameTime){
            var map = KeyboardMap.GetPressedKeys();
            if (!map.Any() || map == null || _selectedButton == TypesOfButtons.None) return;
            if (map[0] == Keys.Escape){
                UntoggleButtons();
                return;
            }

            switch (_selectedButton){
                case TypesOfButtons.MoveUp:
                    GameConfig.Config.KeyboardMap.MoveUp = map[0];
                    _upButton.Text = map[0].ToString();
                    break;
                case TypesOfButtons.MoveDown:
                    GameConfig.Config.KeyboardMap.MoveDown = map[0];
                    _downButton.Text = map[0].ToString();
                    break;
                case TypesOfButtons.MoveLeft:
                    GameConfig.Config.KeyboardMap.MoveLeft = map[0];
                    _leftButton.Text = map[0].ToString();
                    break;
                case TypesOfButtons.MoveRight:
                    GameConfig.Config.KeyboardMap.MoveRight = map[0];
                    _rightButton.Text = map[0].ToString();
                    break;
            }

            UntoggleButtons();
        }


        private void UntoggleButtons(){
            _selectedButton = TypesOfButtons.None;
            _downButton.IsToggled = false;
            _upButton.IsToggled = false;
            _leftButton.IsToggled = false;
            _rightButton.IsToggled = false;
        }
    }


    public enum TypesOfButtons{
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        None
    }
}