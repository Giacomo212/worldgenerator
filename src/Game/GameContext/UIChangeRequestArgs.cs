using System;
using Game.UI;
namespace Game.GameContext{
    public class UiChangeRequestArgs : EventArgs{
        public readonly UserInterface Interface;

        public UiChangeRequestArgs(UserInterface userInterface){
            Interface = userInterface;
        }
    }
}