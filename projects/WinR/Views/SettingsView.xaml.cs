using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using Squirrel;
using WinR.Core.Configuration;
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
            this.InitializeComponent();
            this.LoadQuickStartToDocummentViewer();
            // Having exception "Update.exe not found, not a Squirrel-installed app?" Comment next line or just copy Squirrel.exe as Update.exe on 'bin' folder.
            this.Update();
        }

        private async void Update()
        {
            using (var mgr = new UpdateManager(@"c:\Users\marco.medrano\OneDrive\Público\MyApps\MyApp\Releases"))
            {
                await mgr.UpdateApp();
            }
        }

        private void LoadQuickStartToDocummentViewer()
        {
            string fileName = @"C:\lr.m\winR\docs\Quick Start Guide.xps";
            XpsDocument document = new XpsDocument(fileName, FileAccess.Read);
            this.documentViewer.Document = document.GetFixedDocumentSequence();
            document.Close();

            this.documentViewer.FitToWidth();

            var contentHost = this.documentViewer.Template.FindName("PART_ContentHost", this.documentViewer) as ScrollViewer;
            var grid = contentHost.Parent as Grid;
            grid.Children.RemoveAt(0);
        }

        private void emailButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO investigate set assunto. Also automatically can be attached logs
            var processInfo = new ProcessStartInfo("mailto:marco.medrano.garay@gmail.com");
            new Process {StartInfo = processInfo}.Start();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            new InstallWinRShortCut().Execute();
            this.Close();
            return;
            new SetOperativeSystemPath().Execute(shortcutPathTextBox.Text);
        }
    }
}
