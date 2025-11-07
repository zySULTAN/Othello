using Othello.Models;
using System;

namespace Othello.Controllers
{
    public class GameManager
    {
        public GameBoard Board { get; private set; }
        public Player BlackPlayer { get; private set; }
        public Player WhitePlayer { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }

        public event Action BoardUpdated;
        public event Action<Player> CurrentPlayerChanged;
        public event Action<string> GameEnded;

        // Start a new game with 2 players
        public void StartNewGame(Player black, Player white)
        {
            BlackPlayer = black;
            WhitePlayer = white;

            Board = new GameBoard();
            CurrentPlayer = BlackPlayer;
            IsGameOver = false;

            OnBoardUpdated();
            OnCurrentPlayerChanged(CurrentPlayer);
        }

        // Do the next move in the game
        public bool NextTurn()
        {
            if (IsGameOver) return false;

            var valid = Board.GetValidMoves(CurrentPlayer.Color);

            if (valid.Count == 0)
            {
                Player other = Other(CurrentPlayer);
                var otherValid = Board.GetValidMoves(other.Color);

                if (otherValid.Count == 0)
                {
                    FinishGame();
                    return false;
                }

                CurrentPlayer = other;
                OnCurrentPlayerChanged(CurrentPlayer);
                return true;
            }

            Move? move = CurrentPlayer.RequestMove(Board, valid);
            if (move == null) return false;

            Move m = move.Value;

            bool ok = Board.ApplyMove(m.I, m.J, CurrentPlayer.Color);
            if (!ok) return false;

            OnBoardUpdated();

            CurrentPlayer = Other(CurrentPlayer);
            OnCurrentPlayerChanged(CurrentPlayer);

            if (IsBoardFullOrNoMoves())
            {
                FinishGame();
                return false;
            }

            return true;
        }

        // Get the player who isn't current
        private Player Other(Player p)
        {
            if (p == BlackPlayer) return WhitePlayer;
            return BlackPlayer;
        }

        // Check if board is full or nobody can move
        private bool IsBoardFullOrNoMoves()
        {
            bool hasEmpty = false;

            for (int i = 0; i < GameBoard.Size; i++)
            {
                for (int j = 0; j < GameBoard.Size; j++)
                {
                    if (Board.GetDisk(i, j) == null)
                    {
                        hasEmpty = true;
                        break;
                    }
                }

                if (hasEmpty) break;
            }

            if (!hasEmpty) return true;

            var a = Board.GetValidMoves(CurrentPlayer.Color);
            var b = Board.GetValidMoves(Other(CurrentPlayer).Color);

            if (a.Count == 0 && b.Count == 0) return true;

            return false;
        }

        // Count pieces and see who won
        private void FinishGame()
        {
            IsGameOver = true;

            int black = 0;
            int white = 0;

            for (int i = 0; i < GameBoard.Size; i++)
            {
                for (int j = 0; j < GameBoard.Size; j++)
                {
                    string cell = Board.GetDisk(i, j);

                    if (cell == "Black") black++;
                    else if (cell == "White") white++;
                }
            }

            string result;

            if (black > white)
            {
                result = BlackPlayer.Name + " wins (" + black + "–" + white + ")";
            }
            else if (white > black)
            {
                result = WhitePlayer.Name + " wins (" + white + "–" + black + ")";
            }
            else
            {
                result = "Draw";
            }

            OnGameEnded(result);
        }

        // Tell everyone the board changed
        private void OnBoardUpdated()
        {
            if (BoardUpdated != null) BoardUpdated();
        }

        // Tell everyone it's a new player's turn
        private void OnCurrentPlayerChanged(Player p)
        {
            if (CurrentPlayerChanged != null) CurrentPlayerChanged(p);
        }

        // Tell everyone the game ended
        private void OnGameEnded(string message)
        {
            if (GameEnded != null) GameEnded(message);
        }
    }
}
