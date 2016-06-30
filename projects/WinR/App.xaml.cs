
namespace WinR
{
    using System;
    using System.Windows;

    using WinR.Core.Installation;
    using WinR.Properties;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static readonly InstallerGate InstallerGate = new InstallerGate();

        static App()
        {
            InstallerGate.OnInstallationAndUpdate();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            if (Environment.GetCommandLineArgs().Length <= 1)
            {
                if (Environment.GetCommandLineArgs()[0] == "--squirrel-firstrun")
                    return;
                else
                    this.StartupUri = new Uri("Views/SettingsView.xaml", UriKind.Relative);
                return;
            }

            //TODO Make it async 
            if (Settings.Default.HasAcceptedTermsOfUse == false)
            {
                MessageBox.Show("To continue you need to accept the terms of use", "Taking you to settings", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.StartupUri = new Uri("Views/SettingsView.xaml", UriKind.Relative);
            }
        }
    }
}
