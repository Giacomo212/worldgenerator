using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace worldgenerator {
    public class MainUi {
        public Button this[int i] {
            get { return _buttons[i]; }
        }
        private List<Button> _buttons = new List<Button>();
        private int _selectedIndex = 0;
        public MainUi() {
            AddButtons();
        }
        private void AddButtons() {
            var center = GameConfig.config.Resolution.CenterWidth;
            
                var vector2 = new Vector2((float)GameConfig.config.Resolution.CenterWidth - 60, 200);
                _buttons.Add(new Button(vector2, "Create new world"));
                vector2.Y += 40;
                _buttons.Add(new Button(vector2, "Load word"));
                vector2.Y += 40;
                //_buttons.Add(new Button(vector2, "Export to bitmap"));
                //vector2.Y += 40;
                _buttons.Add(new Button(vector2, "Exit"));
            
            
            
            
        }
        public int SeletedIndex {
            get { return _selectedIndex; }
        }
        public void IncrementIndex() {
            
            if (_selectedIndex < _buttons.Count - 1) {
                _selectedIndex++;
            }
        }
        public void DecrementIndex() {
            if (_selectedIndex >= 1) {
                _selectedIndex--;
            }
        }
        public int Count {
            get { return _buttons.Count; }
        }
        public bool IsSeleted(int index) {
            return index == _selectedIndex;
        }
    }
}
