using System;
using System.Collections.Generic;
using System.Text;

namespace ChalengeFitPokemonTCG.Entities
{
    class Pokemon
    {
        public string Name { get; set; }
        public string Status { get; set; }

        public string StatusSub { get; set; }

        public Pokemon()
        {

        }

        public Pokemon(string name, string status, string statusSub)
        {
            Name = name;
            Status = status;
            StatusSub = statusSub;
        }

        
    }
}
