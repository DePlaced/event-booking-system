using BigDSignClientDesktop.GUI;
using Microsoft.Extensions.DependencyInjection;

namespace BigDSignClientDesktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Initialize and configure services
            ApplicationConfiguration.Initialize();
            Application.Run(new StadiumGUI());
        }

    }
}
