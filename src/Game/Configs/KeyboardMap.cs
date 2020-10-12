using Microsoft.Xna.Framework.Input;

namespace Game.Configs{
    public class KeyboardMap{
        public Keys MoveUp{ get; set; } = Keys.W;
        public Keys MoveDown{ get; set; } = Keys.S;
        public Keys MoveLeft{ get; set; } = Keys.A;
        public Keys MoveRight{ get; set; } = Keys.D;

        public static Keys[] GetPressedKeys(){
            var state = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            return state.GetPressedKeys();
        }
    }
}