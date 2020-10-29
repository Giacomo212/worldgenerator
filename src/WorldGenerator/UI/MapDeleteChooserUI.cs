using System;
using System.IO;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using WorldGenerator.EventArg;
using EnvironmentVariables = WorldGenerator.Utils.EnvironmentVariables;

namespace WorldGenerator.UI{
    public class MapDeleteChooserUI : MapChooserUI{
        public MapDeleteChooserUI() : base(){
            _deleteWorldButton.Background = new SolidBrush(Color.Red);
            _deleteWorldButton.FocusedBackground = new SolidBrush(Color.Red);
            _deleteWorldButton.PressedBackground = new SolidBrush(Color.Red);
            _deleteWorldButton.OverBackground = new SolidBrush(Color.Red);
            _deleteWorldButton.Click += (sender, args) => RequestPreviousInterface();
            _imageGeneratorButton.Click += (sender, args) =>
                OnPreviousUiAndLoadRequest(new UiChangeRequestArgs(new MapImageGeneratorUI()));
        }


        private void DeleteWorld(object sender, EventArgs e){
            var tmp = sender as TextButton;
            File.Delete(EnvironmentVariables.Worldfiles + EnvironmentVariables.Separator + tmp.Text + ".wg");
            GetAllWorlds();
        }

        protected override void GetAllWorlds(){
            base.GetAllWorlds();
            foreach (var button in _worldButtons){
                button.Click += DeleteWorld;
            }
        }
    }
}