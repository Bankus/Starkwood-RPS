using Starkwood_RPS.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.MoveSets
{
    internal class RPSSL : IMoveSet
    {
        public string Name { get; } = "RPSSL";

        public IMove[] Moves { get; } = { 
            new Rock(), 
            new Paper(), 
            new Scissors(), 
            new Spock(), 
            new Lizard() 
        };

    }
}
