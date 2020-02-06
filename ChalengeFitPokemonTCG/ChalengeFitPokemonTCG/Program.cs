using HtmlAgilityPack;
using System;
using System.Net;
using System.Linq;

namespace ChalengeFitPokemonTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var wc = new WebClient();

            for (int i=1; i <= 900; i++) { 
            string pagina = wc.DownloadString("https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/"+i);

                Console.WriteLine(pagina);

                var htmlDocument = new HtmlDocument();

                htmlDocument.LoadHtml(pagina);


                string name = string.Empty;
            string dado1 = string.Empty;

            foreach(HtmlNode node in htmlDocument.GetElementbyId("report-screen-name-success-modal").ChildNodes)
            {
                if(node.Attributes.Count > 0)
                {
                    Console.WriteLine(node.SelectNodes("//div/h6")
                        .First().InnerHtml);
                }
            }

            }

           

            
        }
    }
}
