using Othello.Model;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Players
{
    public class ComputerPlayer : Player
    {
        private static Random random = new Random();

        public ComputerPlayer(string name, DiskColor disk) : base(name, disk)
        {
        }

        public override Position? RequestMove(GameBoard boardCopy, List<Position> validMoves)
        {
            if (validMoves == null || validMoves.Count == 0)
                return null;

            int index = random.Next(validMoves.Count);
            return validMoves[index];
        }
    }
}
