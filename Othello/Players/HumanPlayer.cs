using System.Collections.Generic;

namespace Othello.Models
{
    public class HumanPlayer : Player
    {
        private Move? chosen;

        public HumanPlayer(string name, string color) : base(name, color)
        {
            chosen = null;
        }

        public void SetChosenMove(Move m)
        {
            chosen = m;
        }

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
