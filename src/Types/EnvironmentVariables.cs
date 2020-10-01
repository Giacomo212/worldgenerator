using System;
using System.IO;

namespace Types{
    public static class EnvironmentVariables{
        private static string _gameFiles;
        private static char _separator;
        private static string _worldfiles;
        
        static EnvironmentVariables(){
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                _gameFiles = Environment.GetEnvironmentVariable("HOME") + "/.worldgenerator";
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                _gameFiles = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.worldgenerator";
            else{
                throw  new PlatformNotSupportedException("your platform is not supported");
            }

            _separator = Path.DirectorySeparatorChar;
            _worldfiles = _gameFiles + _separator + "worlds";
        }
        public static string GameFiles => _gameFiles;
        public static char Separator => _separator;
        public static string Worldfiles => _worldfiles;
    }
}