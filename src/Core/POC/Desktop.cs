using System;
using System.Windows;
using WindowsDesktop;
namespace Core.POC
{
    public static class Desktop
    {
        public static void POC_SwitchDesktop()
        {
            
            CustomApplication app = new CustomApplication();
            app.Run();
        }
    }
    

    public class CustomApplication : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Window window = new Window();
            // window.Show();
            test();
        }

        public void test()
        {
            var desktops = VirtualDesktop.GetDesktops();
            Console.Out.WriteLine($"{desktops.Length} desktops");
            // Get Virtual Desktop for specific window
            var desktop = desktops[0];

            // Get the left/right desktop
            var left  = desktop.GetLeft();
            var right = desktop.GetRight();
            right.Switch();
        }
    }
}