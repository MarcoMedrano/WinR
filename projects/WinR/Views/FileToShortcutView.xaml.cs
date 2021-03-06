﻿using System;
using System.Windows;
using System.Windows.Input;

namespace WinR.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.textBox.Focus();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:// the command is binded and executed before this.
                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            //if (this.IsVisible)
            //    this.Close();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsView settingsView = new SettingsView();
            this.Hide();
            settingsView.ShowDialog();
            this.Show();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
