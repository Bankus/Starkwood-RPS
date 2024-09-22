using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.Moves
{
    internal class Lizard : IMove
    {
        public string Name { get; } = "Lizard";
        public string[] WinLines { get; } = {
            "Lizard poisons Spock!",
            "Lizard eats Paper!"
        };

        public Lizard()
        {
        }


        public bool Beats(IMove opponent)
        {
            return opponent is Spock || opponent is Paper;
        }
    }
}
