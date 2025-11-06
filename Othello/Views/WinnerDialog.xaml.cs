using System.Windows;

namespace Othello.Views
{
    public partial class WinnerDialog : Window
    {
        public WinnerDialog(string message)
        {
            InitializeComponent();
            TxtMessage.Text = message;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
