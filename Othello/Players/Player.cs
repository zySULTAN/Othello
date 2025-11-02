using Othello.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Othello.Players
{
    public enum DiskColor
    {
        Black,
        White
    }

    public struct Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    public abstract class Player
    {
        public string Name { get; set; }
        public DiskColor Disk { get; set; }

        public Player(string name, DiskColor disk)
        {
            Name = name;
            Disk = disk;
        }

        public abstract Position? RequestMove(GameBoard boardCopy, List<Position> validMoves);
    }
}