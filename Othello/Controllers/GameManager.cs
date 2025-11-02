using Othello.Models;
using Othello.Players;
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
        public event Action<int, int> ScoresChanged;
        public event Action<string> GameEnded;

        public void StartNewGame(Player black, Player white)
        {
            BlackPlayer = black;
            WhitePlayer = white;
            Board = new GameBoard();
            CurrentPlayer = BlackPlayer;
            IsGameOver = false;
            UpdateScores();
            BoardUpdated?.Invoke();
            CurrentPlayerChanged?.Invoke(CurrentPlayer);
        }

        public bool NextTurn()
        {
            if (IsGameOver) return false;

            var valid = Board.GetValidMoves(CurrentPlayer.Disk);
            if (valid.Count == 0)
            {
                var other = Other(CurrentPlayer);
                var otherValid = Board.GetValidMoves(other.Disk);
                if (otherValid.Count == 0)
                {
                    FinishGame();
                    return false;
                }
                CurrentPlayer = other;
                CurrentPlayerChanged?.Invoke(CurrentPlayer);
                return true;
            }

            var move = CurrentPlayer.RequestMove(Board.Copy(), valid);
            if (move == null) return false;

            if (!Board.ApplyMove(move.Value, CurrentPlayer.Disk)) return false;

            UpdateScores();
            BoardUpdated?.Invoke();

            CurrentPlayer = Other(CurrentPlayer);
            CurrentPlayerChanged?.Invoke(CurrentPlayer);

            if (IsBoardFullOrNoMoves())
            {
                FinishGame();
                return false;
            }

            return true;
        }

        private Player Other(Player p) => p == BlackPlayer ? WhitePlayer : BlackPlayer;

        private void UpdateScores()
        {
            int black = 0, white = 0;
            for (int r = 0; r < GameBoard.Size; r++)
            {
                for (int c = 0; c < GameBoard.Size; c++)
                {
                    var d = Board.GetDisk(r, c);
                    if (d == DiskColor.Black) black++;
                    else if (d == DiskColor.White) white++;
                }
            }
            ScoresChanged?.Invoke(black, white);
        }

        private bool IsBoardFullOrNoMoves()
        {
            bool hasEmpty = false;
            for (int r = 0; r < GameBoard.Size; r++)
            {
                for (int c = 0; c < GameBoard.Size; c++)
                {
                    if (Board.GetDisk(r, c) == null) { hasEmpty = true; break; }
                }
                if (hasEmpty) break;
            }
            if (!hasEmpty) return true;

            var a = Board.GetValidMoves(CurrentPlayer.Disk);
            var b = Board.GetValidMoves(Other(CurrentPlayer).Disk);
            return a.Count == 0 && b.Count == 0;
        }

        private void FinishGame()
        {
            IsGameOver = true;

            int black = 0, white = 0;
            for (int r = 0; r < GameBoard.Size; r++)
            {
                for (int c = 0; c < GameBoard.Size; c++)
                {
                    var d = Board.GetDisk(r, c);
                    if (d == DiskColor.Black) black++;
                    else if (d == DiskColor.White) white++;
                }
            }

            string result;
            if (black > white) result = $"{BlackPlayer.Name} wins ({black}–{white})";
            else if (white > black) result = $"{WhitePlayer.Name} wins ({white}–{black})";
            else result = "Draw";

            GameEnded?.Invoke(result);
        }
    }
}
