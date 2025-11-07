using System.Collections.Generic;

namespace Othello.Models
{
    public class HumanPlayer : Player
    {
        private Move? chosen;

        // Make a new human player
        public HumanPlayer(string name, string color) : base(name, color)
        {
            chosen = null;
        }

        // Save the move the human picked
        public void SetChosenMove(Move m)
        {
            chosen = m;
        }

        // Give the move that the human picked
        public override Move? RequestMove(GameBoard board, List<Move> validMoves)
        {
            if (chosen != null && validMoves.Contains(chosen.Value))
            {
                Move result = chosen.Value;
                chosen = null;
                return result;
            }

            return null;
        }
    }
}
