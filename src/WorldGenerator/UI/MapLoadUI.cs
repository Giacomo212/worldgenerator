using System;
using Myra.Graphics2D.UI;
using WorldGenerator.EventArg;
using WorldGenerator.GameScreen;
using WorldGenerator.MapHandlers;

namespace WorldGenerator.UI{
    public class MapLoadUI : MapChooserUI{
        public MapLoadUI() : base(){
            _deleteWorldButton.Click += (sender, args) =>
                RequestNewInterface(new UiChangeRequestArgs(new MapDeleteChooserUI()));
            _imageGeneratorButton.Click += (sender, args) =>
                RequestNewInterface(new UiChangeRequestArgs(new MapImageGeneratorUI()));
        }


        private void LoadWorldOnClick(object sender, EventArgs e){
            var tmp = sender as TextButton;
            try{
                if (!MapReader.CheckMapIntegrity(tmp.Text)){
                    RequestNewInterface(new UiChangeRequestArgs(new DialogUI("Error", "File is corrupted")));
                    return;
                }

                RequestContext(
                    new ContextChangeRequestedArgs(new MapScreen(MapReader.ReadMap(tmp.Text), new GameInterface())));
            }
            catch (Exception exception){
                RequestNewInterface(new UiChangeRequestArgs(new DialogUI("Error", "File is corrupted")));
            }
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