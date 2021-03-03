using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using WinApi.User32;
using Core.POC;
namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Desktop.POC_SwitchDesktop();
        }
    }
}