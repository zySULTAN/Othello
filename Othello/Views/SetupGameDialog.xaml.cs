using System.Windows;

namespace Othello.Views
{
    public partial class SetupGameDialog : Window
    {
        public string BlackType { get; private set; }
        public string WhiteType { get; private set; }

        public SetupGameDialog()
        {
            InitializeComponent();
        }

        void Ok_Click(object sender, RoutedEventArgs e)
        {
            BlackType = ((System.Windows.Controls.ComboBoxItem)CmbBlackType.SelectedItem).Content.ToString();
            WhiteType = ((System.Windows.Controls.ComboBoxItem)CmbWhiteType.SelectedItem).Content.ToString();
            DialogResult = true;
            Close();
        }

        void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
