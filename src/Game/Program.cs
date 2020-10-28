using System;
using Game.Configs;


namespace Game
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
            GameConfig.Load();
            using (var game = new MainLoop())
                game.Run();
            
        }
        
    }
}
