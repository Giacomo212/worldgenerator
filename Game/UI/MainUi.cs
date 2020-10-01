using Myra.Graphics2D;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class MainUi : UserInterface{
        //main buttons
        
        private TextButton _startGameButton;
        private TextButton _configButton;
        private TextButton _exitButton;
        
        public MainUi() : base(){
            MainGrid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            MainGrid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            MainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            MainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            SetupMainButtons();
            MainGrid.Widgets.Add(_startGameButton);
            MainGrid.Widgets.Add(_configButton);
            MainGrid.Widgets.Add(_exitButton);
        }
        private void SetupMainButtons(){
            _startGameButton = new TextButton{
                Text = "Start a game",
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _configButton = new TextButton{
                Text = "Config",
                Padding = new Thickness(20),
                GridRow = 2,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _exitButton = new TextButton{
                Text = "Exit",
                Padding = new Thickness(20),
                GridRow = 3,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 100,
            };
            _startGameButton.Click += (s, a) => { _interface = new MapCreationUI(); };
            _configButton.Click += (sender, args) => _interface = new ConfigUI();
            _exitButton.Click += (s, a) => { System.Environment.Exit(0); };
        }

        
    }
}