using System.Threading;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;


//public delegate void Run();
namespace Game.UI{
    public class LoadingUI : UserInterface{
        private HorizontalProgressBar _progressBar;
        private ICheckableProcess _process;
        private Thread _thread;
        public LoadingUI(ICheckableProcess process, string textLabel){
            var label = new Label{
                Text =  textLabel,
                GridColumn = 0,
                GridRow = 0,
                VerticalAlignment = VerticalAlignment.Center,
            };
            _process = process;
              _thread = new Thread(_process.Run);
            _thread.Start();
            _progressBar = new HorizontalProgressBar{
                Width = 400,
                Maximum = 100,
                GridColumn = 0,
                GridRow = 1,
                Value = 0,
            };
            _widgets.Add(_progressBar);
            _widgets.Add(label);
        }

        public override void Update(GameTime gameTime){
            _progressBar.Value = _process.CheckPercentDone();
            
            if (!_thread.IsAlive){
                RequestPreviousInterface();
            }
        }
    }
}