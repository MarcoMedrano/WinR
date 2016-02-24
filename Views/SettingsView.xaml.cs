using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using WinR.Properties;

namespace WinR.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void emailButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO investigate set assunto.
            var processInfo = new ProcessStartInfo("mailto:marco.medrano.garay@gmail.com");
            new Process {StartInfo = processInfo}.Start();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Validations!!! ???? UnitTests
            var newPath = shortcutPathTextBox.Text;
            var oldPath = Settings.Default.ShortcutsPath;
            var allPaths = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            string[] paths  = allPaths.Split(';');

            // Used GetFullPath to normalize strings from '/' to '\'
            string oldShortcutPathFound = paths.FirstOrDefault(x => Path.GetFullPath(x) == Path.GetFullPath(oldPath));

            if (oldShortcutPathFound != null)
            {
                allPaths.Replace(oldShortcutPathFound, newPath);
            }
            else
            {
                allPaths += allPaths.TrimEnd().EndsWith(";") ? newPath : ";" + newPath;
            }

            Environment.SetEnvironmentVariable("PATH", allPaths, EnvironmentVariableTarget.User);

        }
    }
}
