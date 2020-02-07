using System;
using System.Collections.Generic;
using System.Text;

namespace ChalengeFitPokemonTCG.Entities
{
    class Cards
    {
        public int Id { get; set; }
        public List<Pokemon> Pokemon { get; set; } = new List<Pokemon>();

        public Cards()
        {

        }

        public Cards(int id)
        {
            Id = id;
            
        }

       

        public void AddPokemon(Pokemon pokemon)
        {
            Pokemon.Add(pokemon);
        }
    }
}
