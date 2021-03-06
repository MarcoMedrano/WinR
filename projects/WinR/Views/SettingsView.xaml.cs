﻿namespace WinR.Views
{
    using System.Diagnostics;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Xps.Packaging;

    using WinR.Core.Configuration;
    using WinR.Core.Installation;
    using WinR.Properties;

    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            this.InitializeComponent();
            this.LoadQuickStartToDocummentViewer();
        }
        
        private void LoadQuickStartToDocummentViewer()
        {
            string fileName = @"Quick Start Guide.xps";
            XpsDocument document = new XpsDocument(fileName, FileAccess.Read);
            this.documentViewer.Document = document.GetFixedDocumentSequence();
            document.Close();

            this.documentViewer.FitToWidth();

            var contentHost = this.documentViewer.Template.FindName("PART_ContentHost", this.documentViewer) as ScrollViewer;
            var grid = contentHost?.Parent as Grid;
            grid?.Children.RemoveAt(0);
        }

        private void emailButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO investigate set Subject. Also automatically can be attached logs
            var processInfo = new ProcessStartInfo("mailto:marco.medrano.garay@gmail.com");
            new Process {StartInfo = processInfo}.Start();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateShortcutsDirectory().Execute();
            new SetOperativeSystemPath().Execute();

            Settings.Default.HasAcceptedTermsOfUse = true;
            Settings.Default.Save();

            this.Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            new InstallerGate().Update();
            //TODO say update to last version or something.
        }
    }
}
