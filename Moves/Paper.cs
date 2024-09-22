using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.Moves
{
    internal class Paper : IMove
    {
        public string Name { get; } = "Paper";
        public string[] WinLines { get; } = {
            "Paper covers Rock!",
            "Paper disproves Spock!"
        };

        public Paper()
        {
        }


        public bool Beats(IMove opponent)
        {
            return opponent is Rock || opponent is Spock;
        }

    }
}
