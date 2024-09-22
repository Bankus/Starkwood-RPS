using Starkwood_RPS.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.Moves
{
    internal class Rock : IMove
    {

        public string Name { get; } = "Rock";

        public string[] WinLines { get; } = {
            "Rock crushes Scissors!",
            "Rock crushes Lizard!"
        };

        public Rock() {

        }

        public bool Beats(IMove opponent) {
            return opponent is Scissors || opponent is Lizard;
        }

    }
}
