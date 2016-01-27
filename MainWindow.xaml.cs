using System;
using System.Windows;
using System.Windows.Input;
using WinR.ViewModel;

namespace WinR
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
                case Key.Enter:
                    //this.Process();
                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //this.Process();
            //this.Close();
        }

        private void Process()
        {
            var commandLineArgs = Environment.GetCommandLineArgs();
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
    }
}
