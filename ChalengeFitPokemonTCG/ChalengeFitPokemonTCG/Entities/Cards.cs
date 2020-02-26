using System;
using System.Collections.Generic;
using System.Net;

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

        public string ConvertAndSplitToBase64(string img)
        {
           string[] split = img.Split("\"");
           var imgLink = split[1].ToString();

           /* byte[] bytesAfterBase64 = Encoding.UTF8.GetBytes(img);
            string imgBase64 = Convert.ToBase64String(bytesAfterBase64);*/


            return imgLink;
        }

        public string convertBase64Img(string imgUrl)
        {
            var wc = new WebClient();
             byte[] vect = wc.DownloadData(imgUrl);
            string imgBase64 = Convert.ToBase64String(vect);

            return imgBase64;
        }
    }
}
