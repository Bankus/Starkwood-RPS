using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.Moves
{
    public interface IMove
    {
        string Name { get; }

        string[] WinLines { get; }

        public bool Beats(IMove opponent);

    }

}