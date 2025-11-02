using Othello.Players;
using System;
using System.Collections.Generic;

namespace Othello.Models
{
    public class GameBoard
    {
        public const int Size = 8;
        private DiskColor?[,] board;

        private static readonly (int dr, int dc)[] Dirs = new (int, int)[]
        {
            (-1,-1), (-1,0), (-1,1),
            (0,-1),          (0,1),
            (1,-1),  (1,0),  (1,1)
        };

        public GameBoard()
        {
            board = new DiskColor?[Size, Size];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    board[r, c] = null;

            int m = Size / 2;
            board[m - 1, m - 1] = DiskColor.White;
            board[m, m] = DiskColor.White;
            board[m - 1, m] = DiskColor.Black;
            board[m, m - 1] = DiskColor.Black;
        }

        public DiskColor? GetDisk(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) return null;
            return board[row, col];
        }

        public List<Position> GetValidMoves(DiskColor color)
        {
            var list = new List<Position>();
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (board[r, c] != null) continue;
                    if (CapturesInAnyDir(r, c, color))
                        list.Add(new Position(r, c));
                }
            }
            return list;
        }

        public bool ApplyMove(Position p, DiskColor color)
        {
            if (p.Row < 0 || p.Row >= Size || p.Col < 0 || p.Col >= Size) return false;
            if (board[p.Row, p.Col] != null) return false;
            bool any = false;
            foreach (var dir in Dirs)
            {
                var toFlip = CollectFlipsInDir(p.Row, p.Col, color, dir.dr, dir.dc);
                if (toFlip.Count > 0)
                {
                    any = true;
                    foreach (var (rr, cc) in toFlip)
                        board[rr, cc] = color;
                }
            }
            if (!any) return false;
            board[p.Row, p.Col] = color;
            return true;
        }

        public GameBoard Copy()
        {
            var copy = new GameBoard();
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    copy.board[r, c] = board[r, c];
            return copy;
        }

        private bool CapturesInAnyDir(int r, int c, DiskColor me)
        {
            foreach (var (dr, dc) in Dirs)
                if (CollectFlipsInDir(r, c, me, dr, dc).Count > 0)
                    return true;
            return false;
        }

        private List<(int r, int c)> CollectFlipsInDir(int r, int c, DiskColor me, int dr, int dc)
        {
            var flips = new List<(int, int)>();
            int rr = r + dr, cc = c + dc;
            var opp = me == DiskColor.Black ? DiskColor.White : DiskColor.Black;

            if (!In(rr, cc) || board[rr, cc] != opp) return flips;

            while (In(rr, cc) && board[rr, cc] == opp)
            {
                flips.Add((rr, cc));
                rr += dr; cc += dc;
            }

            if (!In(rr, cc)) return new List<(int, int)>();
            if (board[rr, cc] != me) return new List<(int, int)>();
            return flips;
        }

        private bool In(int r, int c) => r >= 0 && r < Size && c >= 0 && c < Size;
    }
}
