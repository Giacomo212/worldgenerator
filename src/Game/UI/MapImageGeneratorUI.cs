using Game.GameContext;
using Game.WorldMap;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class MapImageGeneratorUI : MapChooserUI{
        public MapImageGeneratorUI() : base(){
            
            _imageGeneratorButton.Background = new SolidBrush(Color.Blue);
            _imageGeneratorButton.FocusedBackground = new SolidBrush(Color.Blue);
            _imageGeneratorButton.PressedBackground = new SolidBrush(Color.Blue);
            _imageGeneratorButton.OverBackground = new SolidBrush(Color.Blue);
            _imageGeneratorButton.Click += (sender, args) => RequestPreviousInterface();
            _deleteWorldButton.Click += (sender, args) =>
                OnPreviousUiAndLoadRequest(new UiChangeRequestArgs(new MapDeleteChooserUI())); 
            foreach (var button in _worldButtons){
                button.Click += (sender, args) => {
                    var tmp = sender as TextButton;
                    ImageGenerator.GenerateImage(MapReader.ReadMap(tmp.Text));
                };
            }
        }
    }
}