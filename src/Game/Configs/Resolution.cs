namespace Game.Configs {
    public class Resolution {
        public Resolution(int width, int hight, bool fullscreen) {
            Width = width;
            Hight = hight;
            //_centerHight = Hight / 2;
            //_centerWidth = Width / 2;
            IsFullScreen = fullscreen;
        }

        // public double CenterWidth => _width/2;
        // public double CenterHight => _hight/2;
        public bool IsFullScreen{ get; }

        public int Width{ get; }

        public int Hight{ get; }
    }
    }
