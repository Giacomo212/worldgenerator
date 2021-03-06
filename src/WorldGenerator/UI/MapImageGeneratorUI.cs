using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using WorldGenerator.EventArg;
using WorldGenerator.MapHandlers;
using WorldGenerator.Process;

namespace WorldGenerator.UI{
    public class MapImageGeneratorUI : MapChooserUI{
        public MapImageGeneratorUI() : base(){
            _imageGeneratorButton.Background = new SolidBrush(Color.Blue);
            _imageGeneratorButton.FocusedBackground = new SolidBrush(Color.Blue);
            _imageGeneratorButton.PressedBackground = new SolidBrush(Color.Blue);
            _imageGeneratorButton.OverBackground = new SolidBrush(Color.Blue);
            _imageGeneratorButton.Click += (sender, args) => RequestPreviousInterface();
            _deleteWorldButton.Click += (sender, args) =>
                OnPreviousUiAndLoadRequest(new UiChangeRequestArgs(new MapDeleteChooserUI()));
        }

        protected override void GetAllWorlds(){
            base.GetAllWorlds();
            foreach (var button in _worldButtons){
                button.Click += (sender, args) => {
                    var tmp = sender as TextButton;
                    if (!MapReader.CheckMapIntegrity(tmp.Text)){
                        RequestNewInterface(new UiChangeRequestArgs(new DialogUI("Error","File is corrupted")));
                        return;
                    }
                    RequestNewInterface(new UiChangeRequestArgs(new LoadingUI(new MapImageGenerationProcess(MapReader.ReadMap(tmp.Text)),"Generating an image" )));
                };
            }
        }
    }
}