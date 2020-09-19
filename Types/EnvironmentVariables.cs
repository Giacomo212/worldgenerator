using System;

namespace Libraries{
    public static class EnvironmentVariables{
        private static string _gameFiles;

        static EnvironmentVariables(){
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                _gameFiles = Environment.GetEnvironmentVariable("HOME") + "/.worldgenerator";
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                _gameFiles = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.worldgenerator";
            else{
                throw  new PlatformNotSupportedException("your platform is not supported");
            }
        }
        public static string GameFiles => _gameFiles;
    }
}