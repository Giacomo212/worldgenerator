using System;
using System.IO;

namespace WorldGenerator.Utils{
    public static class EnvironmentVariables{
        public static string GameFiles{ get; }
        public static char Separator{ get; }
        public static string WorldFiles{ get; }

        static EnvironmentVariables(){
            Separator = Path.DirectorySeparatorChar;
            GameFiles = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Separator +
                        "worldgenerator";
            WorldFiles = GameFiles + Separator + "worlds";
        }
    }
}