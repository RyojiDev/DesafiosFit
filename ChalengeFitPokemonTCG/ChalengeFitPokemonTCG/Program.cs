using HtmlAgilityPack;
using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using ChalengeFitPokemonTCG.Entities;

namespace ChalengeFitPokemonTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pokemon> pk = new List<Pokemon>();
            Console.WriteLine("1 - Acesse o site https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/");
            Console.WriteLine("2 - Realize uma sem preencher nenhum campo (clicando em Search)");
            Console.Write("3 - Informe abaixo a quantidade de paginas: ");

            int countPage = int.Parse(Console.ReadLine());



            var wc = new WebClient();

            for (int i=1; i <= countPage; i++) { 
            string pagina = wc.DownloadString("https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/"+i);

                /*Console.WriteLine(pagina);*/

                var htmlDocument = new HtmlDocument();

                

                htmlDocument.LoadHtml(pagina);


                string name = string.Empty;
                string status = string.Empty;
                string statusSub = string.Empty;

            foreach(HtmlNode node in htmlDocument.GetElementbyId("report-screen-name-success-modal").ChildNodes)
            {
                    
                    Cards card;

                if (node.Attributes.Count > 0)
                {
                   name = node.SelectNodes("//div/h6")
                        .First().InnerHtml;
                    status = node.SelectNodes("//div/h6")
                        .First().InnerHtml;
                    statusSub = node.SelectNodes("//div/h6")
                        .First().InnerHtml;

                         

                        card = new Cards(i);

                        

                        pk.Add(new Pokemon(name, status, statusSub));

                        foreach(Pokemon pkl in pk)
                        {
                            Console.WriteLine(pkl.Name);
                            Console.WriteLine(pkl.Status);
                            Console.WriteLine(pkl.StatusSub);


                        }







                    }
            }

            }

           

            
        }
    }
}
