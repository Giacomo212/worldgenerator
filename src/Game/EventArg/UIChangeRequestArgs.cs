using System;
using Game.UI;

namespace Game.EventArg{
    public class UiChangeRequestArgs : EventArgs{
        public readonly UserInterface Interface;

        public UiChangeRequestArgs(UserInterface userInterface){
            Interface = userInterface;
        }
    }
}