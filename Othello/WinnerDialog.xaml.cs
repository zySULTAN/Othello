using System.Windows;

namespace Othello
{
    public partial class WinnerDialog : Window
    {
        public WinnerDialog(string winnerName)
        {
            InitializeComponent();
            Message.Text = $"Winner: {winnerName}";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
