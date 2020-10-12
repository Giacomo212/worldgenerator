using System;
using System.Linq;
using Game.Configs;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class KeyBindCustomizerUI : UserInterface{

        private TextButton _listeningButton = null;
        public KeyBindCustomizerUI(){
            var upLabel = CrateLabel("Move Up: ", 0, 0);
            var downLabel = CrateLabel("Move Down: ", 1, 0);
            var leftLabel = CrateLabel("Move Left: ", 2, 0);
            var rightLabel = CrateLabel("Move Right: ", 3, 0);
            _widgets.Add(upLabel);
            _widgets.Add(downLabel);
            _widgets.Add(leftLabel);
            _widgets.Add(rightLabel);
            var upButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveUp.ToString(), 0, 1);
            var downButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveDown.ToString(), 1, 1);
            var rightButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveRight.ToString(), 2, 1);
            var leftButton = CrateTextButton(GameConfig.Config.KeyboardMap.MoveLeft.ToString(), 3, 1);
            upButton.Click += ButtonOnClick;
            _widgets.Add(upButton);
            _widgets.Add(downButton);
            _widgets.Add(leftButton);
            _widgets.Add(rightButton);
            var cancelButton = CrateTextButton("Cancel", 4, 0);
        }

        private void ButtonOnClick(object sender, EventArgs e){
            _listeningButton = sender as TextButton;
            _listeningButton.Background = new SolidBrush(Color.Blue);
        }

        public override void Update(GameTime gameTime){
            if(_listeningButton == null) return;
            if (KeyboardMap.GetPressedKeys().Any()){
               // _listeningButton.Text == GameConfig.Config.KeyboardMap.MoveUp.ToString();
            }
        }

        
    }
}