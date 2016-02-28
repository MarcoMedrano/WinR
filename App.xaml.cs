using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WinR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Environment.GetCommandLineArgs().Length <= 1)
                this.StartupUri = new Uri("Views/SettingsView.xaml", UriKind.Relative);
        }
    }
}
