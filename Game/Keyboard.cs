using Microsoft.Xna.Framework.Input;

namespace Game {
    static class Keyboard {
        private static KeyboardState _currentKeyState;
        private static KeyboardState _previousKeyState;
        public static void Initialize() {
            
            
        }
        public static void UpdateState() {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            
        }
        public static bool IsPressed(Keys key) {
            
            return _currentKeyState.IsKeyDown(key);
        }
        public static bool HasBeenPressed(Keys key) {
            
            return _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
        }
    }
}
