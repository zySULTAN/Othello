using System.Windows;

namespace Othello.Views
{
    public partial class DrawnDialog : Window
    {
        public DrawnDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
