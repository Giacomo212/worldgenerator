using System;
using WorldGenerator.UI;

namespace WorldGenerator.EventArg{
    public class UiChangeRequestArgs : EventArgs{
        public readonly UserInterface Interface;

        public UiChangeRequestArgs(UserInterface userInterface){
            Interface = userInterface;
        }
    }
}