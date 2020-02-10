using System;
using System.Collections.Generic;
using System.Text;

namespace ChalengeFitPokemonTCG.Entities
{
    class Pokemon
    {
        
        public string Name { get; set; }
        public string Abilities { get; set; }

        public string Status { get; set; }

        public Pokemon()
        {

        }

        public Pokemon(string name, string abilities, string status)
        {
            Name = name;
            Abilities = abilities;
            Status = status;
        }

        
    }
}
