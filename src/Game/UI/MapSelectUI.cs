using System;
using Game.EventArg;
using Game.GameContext;
using Game.MapHandler;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class MapSelectUI : MapChooserUI{
        public MapSelectUI() : base(){
            _deleteWorldButton.Click += (sender, args) =>
                RequestNewInterface(new UiChangeRequestArgs(new MapDeleteChooserUI()));
            _imageGeneratorButton.Click += (sender, args) =>
                RequestNewInterface(new UiChangeRequestArgs(new MapImageGeneratorUI()));
        }


        private void LoadWorldOnClick(object sender, EventArgs e){
            var tmp = sender as TextButton;
            RequestContext(
                new ContextChangeRequestedArgs(new MapContext(MapReader.ReadMap(tmp.Text), new GameInterface())));
        }

        public override void Awake(){
            _scrollGrid.Widgets.Clear();
            GetAllWorlds();
        }

        protected override void GetAllWorlds(){
            base.GetAllWorlds();
            foreach (var button in _worldButtons){
                button.Click += LoadWorldOnClick;
            }
        }
    }
}