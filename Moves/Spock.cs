using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.Moves
{
    internal class Spock : IMove
    {
        public string Name { get; } = "Spock";
        public string[] WinLines { get; } = {
            "Spock smashes Scissors!",
            "Spock vaporizes Rock!"
        };

        public Spock()
        {

        }


        public bool Beats(IMove opponent)
        {
            return opponent is Scissors || opponent is Rock;
        }
    }
}
