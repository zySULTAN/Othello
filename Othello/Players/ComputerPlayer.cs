using System;
using System.Collections.Generic;

namespace Othello.Models
{
    public class ComputerPlayer : Player
    {
        private static readonly Random rng = new Random();

        public ComputerPlayer(string name, string color) : base(name, color)
        {
        }

        public override Move? RequestMove(GameBoard board, List<Move> validMoves)
        {
            if (validMoves == null) return null;
            if (validMoves.Count == 0) return null;

            int index = rng.Next(validMoves.Count);
            Move move = validMoves[index];
            return move;
        }
    }
}
