using Game.EventArg;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Game.GameContext;

namespace Game.UI{
    public class MainUi : UserInterface{
        //main buttons
        private TextButton _startGameButton;
        private TextButton _configButton;
        private TextButton _exitButton;
        
        public MainUi() : base(){
           
            SetupMainButtons();
        }
        private void SetupMainButtons(){
            _startGameButton = new TextButton{
                Text = "Start a game",
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
                GridRow = 0,
            };
            _configButton = new TextButton{
                Text = "Config",
                Padding = new Thickness(20),
                GridRow = 1,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _exitButton = new TextButton{
                Text = "Exit",
                Padding = new Thickness(20),
                GridRow = 2,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _startGameButton.Click += (s, a) => { RequestNewInterface(new UiChangeRequestArgs(new MapSelectUI())); };
            _configButton.Click += (sender, args) => RequestNewInterface(new UiChangeRequestArgs(new ConfigUI()));
            _exitButton.Click += (s, a) => { OnExitRequest(); };
            _widgets.Add(_startGameButton);
            _widgets.Add(_configButton);
            _widgets.Add(_exitButton);
            
        }

        
    }
}