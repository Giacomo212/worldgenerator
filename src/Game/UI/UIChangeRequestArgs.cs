using System;

namespace Game.UI{
    public class UiChangeRequestArgs : EventArgs{
        public readonly UserInterface Interface;

        public UiChangeRequestArgs(UserInterface userInterface){
            Interface = userInterface;
        }
    }
}