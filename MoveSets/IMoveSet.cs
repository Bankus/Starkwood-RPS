using Starkwood_RPS.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.MoveSets
{
    internal interface IMoveSet
    {
        string Name { get; }

        IMove[] Moves { get; }

    }
}
