using System;
using System.Collections.Generic;

namespace Othello.Models
{
    public struct Move
    {
        public int I;
        public int J;
        public Move(int i, int j)
        {
            I = i;
            J = j;
        }
    }

    public class GameBoard
    {
        public const int Size = 8;
        private string[,] board;

        public GameBoard()
        {
            board = new string[Size, Size];
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    board[i, j] = null;

            int mid = Size / 2;
            board[mid - 1, mid - 1] = "White";
            board[mid, mid] = "White";
            board[mid - 1, mid] = "Black";
            board[mid, mid - 1] = "Black";
        }

        public string GetDisk(int i, int j)
        {
            if (!Inside(i, j)) return null;
            return board[i, j];
        }

        public List<Move> GetValidMoves(string color)
        {
            var moves = new List<Move>();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (board[i, j] != null) continue;

                    if (CanCapture(i, j, color))
                        moves.Add(new Move(i, j));
                }
            }

            return moves;
        }

        public bool ApplyMove(int i, int j, string color)
        {
            if (!Inside(i, j)) return false;
            if (board[i, j] != null) return false;

            bool flipped = false;

            // Gå åt alla åtta riktningar
            flipped |= FlipDirection(i, j, color, -1, 0);   // upp
            flipped |= FlipDirection(i, j, color, 1, 0);    // ned
            flipped |= FlipDirection(i, j, color, 0, -1);   // vänster
            flipped |= FlipDirection(i, j, color, 0, 1);    // höger
            flipped |= FlipDirection(i, j, color, -1, -1);  // upp-vänster
            flipped |= FlipDirection(i, j, color, -1, 1);   // upp-höger
            flipped |= FlipDirection(i, j, color, 1, -1);   // ned-vänster
            flipped |= FlipDirection(i, j, color, 1, 1);    // ned-höger

            if (!flipped) return false;

            board[i, j] = color;
            return true;
        }

        private bool CanCapture(int i, int j, string color)
        {
            // Testar alla 8 riktningar — enklare att läsa än arrayer
            if (CountFlips(i, j, color, -1, 0) > 0) return true;
            if (CountFlips(i, j, color, 1, 0) > 0) return true;
            if (CountFlips(i, j, color, 0, -1) > 0) return true;
            if (CountFlips(i, j, color, 0, 1) > 0) return true;
            if (CountFlips(i, j, color, -1, -1) > 0) return true;
            if (CountFlips(i, j, color, -1, 1) > 0) return true;
            if (CountFlips(i, j, color, 1, -1) > 0) return true;
            if (CountFlips(i, j, color, 1, 1) > 0) return true;

            return false;
        }

        private int CountFlips(int i, int j, string color, int di, int dj)
        {
            string opp = (color == "Black") ? "White" : "Black";
            int ii = i + di;
            int jj = j + dj;
            int count = 0;

            // första måste vara motståndare
            if (!Inside(ii, jj) || board[ii, jj] != opp)
                return 0;

            while (Inside(ii, jj) && board[ii, jj] == opp)
            {
                count++;
                ii += di;
                jj += dj;
            }

            if (!Inside(ii, jj)) return 0;
            if (board[ii, jj] != color) return 0;

            return count;
        }

        private bool FlipDirection(int i, int j, string color, int di, int dj)
        {
            int flips = CountFlips(i, j, color, di, dj);
            if (flips == 0) return false;

            int ii = i + di;
            int jj = j + dj;

            for (int k = 0; k < flips; k++)
            {
                board[ii, jj] = color;
                ii += di;
                jj += dj;
            }

            return true;
        }

        private bool Inside(int i, int j)
        {
            return i >= 0 && i < Size && j >= 0 && j < Size;
        }
    }
}
