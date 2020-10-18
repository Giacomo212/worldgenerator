using System;
using Game.GameContext;
using Game.WorldMap;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class MapSelectUI : MapChooserUI{
        
        public MapSelectUI() : base(){
            _deleteWorldButton.Click += (sender, args) =>  RequestNewInterface(new UiChangeRequestArgs(new MapDeleteChooserUI()));
            foreach (var button in _worldButtons){
                button.Click += LoadWorldOnClick;
            }
            _imageGeneratorButton.Click += (sender, args) => RequestNewInterface(new UiChangeRequestArgs(new MapImageGeneratorUI()));
        }
        
        
        private void LoadWorldOnClick(object? sender, EventArgs e){
            var tmp = sender as TextButton;
            RequestContext(
                new ContextChangeRequestedArgs(new MapContext(MapReader.ReadMap(tmp.Text), new GameInterface())));
        }
    }
}