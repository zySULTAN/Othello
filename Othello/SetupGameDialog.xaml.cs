using System.Windows;

namespace Othello
{
    public partial class SetupGameDialog : Window
    {
        public string Player1Name => Player1NameText;
        public bool Player1IsComputer => Player1IsComputerCheck;
        public string Player2Name => Player2NameText;
        public bool Player2IsComputer => Player2IsComputerCheck;

        // Backing properties used because direct binding in this minimal sample is omitted
        private string Player1NameText => Player1Name.Text;
        private bool Player1IsComputerCheck => Player1IsComputer.IsChecked == true;
        private string Player2NameText => Player2Name.Text;
        private bool Player2IsComputerCheck => Player2IsComputer.IsChecked == true;

        public SetupGameDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
