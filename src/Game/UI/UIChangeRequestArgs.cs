using System;

namespace Game.UI{
    public class UiChangeRequestArgs : EventArgs{
        public UserInterface Interface;

        public UiChangeRequestArgs(UserInterface userInterface){
            Interface = userInterface;
        }
    }
}