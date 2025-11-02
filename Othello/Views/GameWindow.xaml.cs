using Othello.Controllers;
using Othello.Models;
using Othello.Players;
using System.Windows;

namespace Othello.Views
{
    public partial class GameWindow : Window
    {
        GameManager gm;

        public GameWindow()
        {
            InitializeComponent();
            gm = new GameManager();
            gm.BoardUpdated += () => BoardView.Render(gm.Board);
            gm.CurrentPlayerChanged += p => TxtCurrent.Text = p.Name;
            gm.GameEnded += msg => MessageBox.Show(msg, "Game Over");
            BoardView.TileClicked += BoardView_TileClicked;
        }

        void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SetupGameDialog { Owner = this };
            if (dlg.ShowDialog() != true) return;

            Player black = dlg.BlackType == "Computer"
                ? new ComputerPlayer("Black", DiskColor.Black)
                : new HumanPlayer("Black", DiskColor.Black);

            Player white = dlg.WhiteType == "Computer"
                ? new ComputerPlayer("White", DiskColor.White)
                : new HumanPlayer("White", DiskColor.White);

            gm.StartNewGame(black, white);
            AutoPlayComputers();
        }

        void BoardView_TileClicked(int row, int col)
        {
            if (gm == null || gm.IsGameOver) return;
            if (gm.CurrentPlayer is HumanPlayer hp)
            {
                hp.SetChosenMove(new Position(row, col));
                if (gm.NextTurn()) AutoPlayComputers();
            }
        }

        void AutoPlayComputers()
        {
            while (!gm.IsGameOver && gm.CurrentPlayer is ComputerPlayer)
            {
                if (!gm.NextTurn()) break;
            }
        }

        void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
