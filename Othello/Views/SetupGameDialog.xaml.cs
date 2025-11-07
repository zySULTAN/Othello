using System.Windows;

namespace Othello.Views
{
    public partial class SetupGameDialog : Window
    {
        public string BlackType { get; private set; }
        public string WhiteType { get; private set; }

        // Initializes a new instance of the SetupGameDialog class
        public SetupGameDialog()
        {
            InitializeComponent();
        }

        // Handles the click event for the OK button to save selections and close the dialog
        void Ok_Click(object sender, RoutedEventArgs e)
        {
            BlackType = ((System.Windows.Controls.ComboBoxItem)CmbBlackType.SelectedItem).Content.ToString();
            WhiteType = ((System.Windows.Controls.ComboBoxItem)CmbWhiteType.SelectedItem).Content.ToString();
            DialogResult = true;
            Close();
        }

        // Handles the click event for the Cancel button to close the dialog without saving
        void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
