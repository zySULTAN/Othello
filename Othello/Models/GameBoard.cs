using Othello.Players;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public class GameBoard
    {
        public const int Size = 8;
        private DiskColor?[,] board;

        public GameBoard()
        {
            board = new DiskColor?[Size, Size];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                    board[r, c] = null;
            }

            int mid = Size / 2;
            board[mid - 1, mid - 1] = DiskColor.White;
            board[mid, mid] = DiskColor.White;
            board[mid - 1, mid] = DiskColor.Black;
            board[mid, mid - 1] = DiskColor.Black;
        }

        public DiskColor? GetDisk(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return null;
            return board[row, col];
        }

        public void PlaceDisk(int row, int col, DiskColor color)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return;
            board[row, col] = color;
        }

        public List<Position> GetValidMoves(DiskColor currentPlayer)
        {
            var moves = new List<Position>();

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (board[r, c] == null)
                        moves.Add(new Position(r, c));
                }
            }

            return moves;
        }

        public GameBoard Copy()
        {
            var copy = new GameBoard();
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    copy.board[r, c] = board[r, c];
                }
            }
            return copy;
        }
    }
}
