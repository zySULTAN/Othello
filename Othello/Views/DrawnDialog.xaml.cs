using System.Windows;

namespace Othello.Views
{
    public partial class DrawnDialog : Window
    {
        // Initializes a new instance of the DrawnDialog class
        public DrawnDialog()
        {
            InitializeComponent();
        }

        // Handles the click event for the OK button to close the dialog
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
