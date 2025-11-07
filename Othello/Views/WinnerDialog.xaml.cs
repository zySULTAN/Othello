using System.Windows;

namespace Othello.Views
{
    public partial class WinnerDialog : Window
    {
        // Initializes a new instance of the WinnerDialog class with a message
        public WinnerDialog(string message)
        {
            InitializeComponent();
            TxtMessage.Text = message;
        }

        // Handles the click event for the OK button to close the dialog
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
