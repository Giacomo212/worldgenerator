using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace worldgenerator {
    public class Button {
       
        
        public string Text { get;}
        public Vector2 Position { get; }
        public int Width { get; }
        public int Hight { get; }

        public Button(Vector2 position,string text) {
            Position = position;
            Text = text;
        }
    }
}
