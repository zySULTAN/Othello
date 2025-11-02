using System.Windows;

namespace Othello
{
    public partial class DrawnDialog : Window
    {
        public DrawnDialog()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
