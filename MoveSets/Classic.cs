using Starkwood_RPS.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starkwood_RPS.MoveSets
{
    internal class Classic : IMoveSet
    {
        public string Name { get; } = "Classic";

        public IMove[] Moves { get; } = {
            new Rock(), 
            new Paper(), 
            new Scissors() 
        };


    }
}
