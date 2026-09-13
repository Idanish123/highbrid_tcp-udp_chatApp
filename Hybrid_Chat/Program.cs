using System;
using System.Windows.Forms;
using Hybrid_Chat.GUI;

namespace Hybrid_Chat
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            bool clientOnly = args.Length > 0 && (args[0].Equals("client", StringComparison.OrdinalIgnoreCase) || args[0].Equals("--client", StringComparison.OrdinalIgnoreCase));
            if (clientOnly)
            {
                Application.Run(new FormClient());
            }
            else
            {
                Application.Run(new FormMain());
            }
        }
    }
}