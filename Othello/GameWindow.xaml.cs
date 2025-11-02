using System;
using System.Windows;
using System.Windows.Threading;

namespace Othello
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameManager _gameManager;

        public GameWindow()
        {
            InitializeComponent();
            _gameManager = new GameManager();
            _gameManager.BoardUpdated += GameManager_BoardUpdated;
            _gameManager.GameEnded += GameManager_GameEnded;
            GameGridControl.TileClicked += GameGridControl_TileClicked;
        }

        private void GameGridControl_TileClicked(object sender, TileClickedEventArgs e)
        {
            // Forward click to human player via GameManager
            _gameManager.OnHumanMoveRequested(e.Row, e.Col);
        }

        private void GameManager_BoardUpdated(object? sender, BoardUpdatedEventArgs e)
        {
            // UI must be updated on UI thread
            Dispatcher.Invoke(() =>
            {
                GameGridControl.UpdateBoard(e.Board);
                StatusText.Text = $"Black: {e.BlackCount}  White: {e.WhiteCount}  Current: {e.CurrentPlayerName}";
            });
        }

        private void GameManager_GameEnded(object? sender, GameEndedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.IsDraw)
                {
                    var dlg = new DrawnDialog();
                    dlg.Owner = this;
                    dlg.ShowDialog();
                }
                else
                {
                    var dlg = new WinnerDialog(e.WinnerName);
                    dlg.Owner = this;
                    dlg.ShowDialog();
                }
            });
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var setup = new SetupGameDialog();
            setup.Owner = this;
            if (setup.ShowDialog() == true)
            {
                // Create players based on dialog
                var p1 = setup.Player1IsComputer ? (Player)new ComputerPlayer(setup.Player1Name, Disk.Black) : new HumanPlayer(setup.Player1Name, Disk.Black);
                var p2 = setup.Player2IsComputer ? (Player)new ComputerPlayer(setup.Player2Name, Disk.White) : new HumanPlayer(setup.Player2Name, Disk.White);
                _gameManager.StartNewGame(p1, p2);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
