using System.Windows;
using Othello.Controllers;
using Othello.Models;

namespace Othello.Views
{
    public partial class GameWindow : Window
    {
        private GameManager gm;

        public GameWindow()
        {
            InitializeComponent();

            gm = new GameManager();

            gm.BoardUpdated += OnBoardUpdated;
            gm.CurrentPlayerChanged += OnCurrentPlayerChanged;
            gm.GameEnded += OnGameEnded;

            BoardView.TileClicked += BoardView_TileClicked;
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            SetupGameDialog dlg = new SetupGameDialog();
            dlg.Owner = this;

            bool? ok = dlg.ShowDialog();
            if (ok != true) return;

            Player black;
            Player white;

            if (dlg.BlackType == "Computer")
            {
                black = new ComputerPlayer("Black", "Black");
            }
            else
            {
                black = new HumanPlayer("Black", "Black");
            }

            if (dlg.WhiteType == "Computer")
            {
                white = new ComputerPlayer("White", "White");
            }
            else
            {
                white = new HumanPlayer("White", "White");
            }

            gm.StartNewGame(black, white);

            AutoPlayComputers();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BoardView_TileClicked(int row, int col)
        {
            if (gm == null) return;
            if (gm.IsGameOver) return;

            HumanPlayer hp = gm.CurrentPlayer as HumanPlayer;
            if (hp == null) return;

            hp.SetChosenMove(new Move(row, col));

            bool advanced = gm.NextTurn();
            if (advanced)
            {
                AutoPlayComputers();
            }
        }

        private void AutoPlayComputers()
        {
            while (!gm.IsGameOver)
            {
                ComputerPlayer cp = gm.CurrentPlayer as ComputerPlayer;
                if (cp == null) break;

                bool advanced = gm.NextTurn();
                if (!advanced) break;
            }
        }

        private void OnBoardUpdated()
        {
            if (gm != null && gm.Board != null)
            {
                BoardView.Render(gm.Board);
            }
        }

        private void OnCurrentPlayerChanged(Player p)
        {
            if (p == null) return;
            TxtCurrent.Text = p.Name;
        }

        private void OnGameEnded(string message)
        {
            if (message == "Draw")
            {
                DrawnDialog dlg = new DrawnDialog();
                dlg.Owner = this;
                dlg.ShowDialog();
            }
            else
            {
                WinnerDialog dlg = new WinnerDialog(message);
                dlg.Owner = this;
                dlg.ShowDialog();
            }
        }

    }
}
