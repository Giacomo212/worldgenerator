using System;
using System.IO;
using WorldGenerator.Configs;
using WorldGenerator.Utils;

namespace WorldGenerator
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Directory.CreateDirectory(EnvironmentVariables.GameFiles);
            Directory.CreateDirectory(EnvironmentVariables.WorldFiles);
            GameConfig.Load();
            using var game = new MainLoop();
            game.Run();
            
        }
        
    }
}
