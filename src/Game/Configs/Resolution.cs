namespace Game.Configs {
    public class Resolution {
        public Resolution(int width, int hight, bool fullscreen) {
            Width = width;
            Hight = hight;
            IsFullScreen = fullscreen;
        }
        public Resolution() {
            Width = 800;
            Hight = 600;
            IsFullScreen = false;
        }
        public bool IsFullScreen{ get; set; }

        public int Width{ get; set; }

        public int Hight{ get; set; }
    }
    }
