using System;
using System.IO;
using Game.GameContext;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Types;

namespace Game.UI{
    public class MapDeleteChooserUI : MapChooserUI{

        public MapDeleteChooserUI() : base(){
            _deleteWorldButton.Background = new SolidBrush(Color.Red);
            _deleteWorldButton.FocusedBackground = new SolidBrush(Color.Red);
            _deleteWorldButton.PressedBackground = new SolidBrush(Color.Red);
            _deleteWorldButton.OverBackground = new SolidBrush(Color.Red);
            _deleteWorldButton.Click += (sender, args) => RequestPreviousInterface();
            _imageGeneratorButton.Click += (sender, args) =>
                OnPreviousUiAndLoadRequest(new UiChangeRequestArgs(new MapImageGeneratorUI()));
            foreach (var button in _worldButtons){
                button.Click += DeleteWorld;
            }
        }
        
        
        
        private void DeleteWorld(object? sender, EventArgs e){
            var tmp = sender as TextButton;
            File.Delete(EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator + tmp.Text + ".wg");
            GetAllWorlds();
            foreach (var button in _worldButtons)
                button.Click += DeleteWorld;
        }
    }
}