using System;
using System.Threading;
using System.Windows;
using Core.POC;

namespace Watrix
{
    class Program: Application
    {
        private static Application app;
        
        static void Main(string[] args)
        {
            // app = new Program();
            // app.Run();
            Desktop.POC_SwitchDesktopNoWPF();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Window window = new Window();
            // window.Show();
        }
    }
}