using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldgenerator {
    public class Resolution {
        private int _width;
        private int _hight;
        //private double _centerWidth;
        //private double _centerHight;
        private bool _isFullScreen;
        public Resolution(int width, int hight, bool fullscreen) {
            _width = width;
            _hight = hight;
            //_centerHight = Hight / 2;
            //_centerWidth = Width / 2;
            _isFullScreen = fullscreen;
        }

        public double CenterWidth { get => _width/2;  }
        public double CenterHight { get => _hight/2;  }
        public bool IsFullScreen { get => _isFullScreen; }
        public int Width { get => _width;}
        public int Hight { get => _hight;}
    }
    }
