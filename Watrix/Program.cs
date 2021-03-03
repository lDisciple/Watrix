using System;
using System.Windows;

namespace Watrix
{
    class Program: Application
    {
        private static Application app;
        
        static void Main(string[] args)
        {
            app = new Program();
            app.Run();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Window window = new Window();
            // window.Show();
        }
    }
}