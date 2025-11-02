using Othello.Models;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Players
{
    public class HumanPlayer : Player
    {
        private Position? chosenMove;

        public HumanPlayer(string name, DiskColor disk) : base(name, disk)
        {
            chosenMove = null;
        }

        public void SetChosenMove(Position move)
        {
            chosenMove = move;
        }

        public override Position? RequestMove(GameBoard boardCopy, List<Position> validMoves)
        {
            if (chosenMove != null && validMoves.Contains(chosenMove.Value))
            {
                var move = chosenMove;
                chosenMove = null;
                return move;
            }
            return null;
        }
    }
}
