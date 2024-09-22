using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.Moves
{
    internal class Scissors : IMove
    {
        public string Name { get; } = "Scissors";

        public string MoveSet { get; set; } = "";
        public string[] WinLines { get; } = {
            "Scissors cuts Paper!",
            "Scissors decapitates Lizard!"
        };
        public Scissors()
        {

        }


        public bool Beats(IMove opponent)
        {
            return opponent is Paper || opponent is Lizard;
        }
    }
}
