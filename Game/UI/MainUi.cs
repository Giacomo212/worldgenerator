using Myra.Graphics2D;
using Myra.Graphics2D.UI;

namespace Game.UI{
    public class MainUi : UserInterface{
        //main buttons
        private Grid _grid;
        private TextButton _startGameButton;
        private TextButton _configButton;
        private TextButton _exitButton;
        
        public MainUi() : base(){
            _grid = new Grid {
                RowSpacing = 8,
                ColumnSpacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Root = _grid;
            _grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            _grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            SetupMainButtons();
            _grid.Widgets.Add(_startGameButton);
            _grid.Widgets.Add(_configButton);
            _grid.Widgets.Add(_exitButton);
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
            _exitButton.Click += (s, a) => { System.Environment.Exit(0); };
        }

        
    }
}