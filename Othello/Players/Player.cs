using System.Collections.Generic;

namespace Othello.Models
{
    public abstract class Player
    {
        public string Name { get; set; }
        public string Color { get; set; }

        // Make a new player with a name and color
        protected Player(string name, string color)
        {
            Name = name;
            Color = color;
        }

        // Ask the player to pick a move from the valid moves
        public abstract Move? RequestMove(GameBoard board, List<Move> validMoves);
    }
}
