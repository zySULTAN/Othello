using System.Collections.Generic;

namespace Othello.Models
{
    public abstract class Player
    {
        public string Name { get; set; }
        public string Color { get; set; }

        protected Player(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public abstract Move? RequestMove(GameBoard board, List<Move> validMoves);
    }
}
