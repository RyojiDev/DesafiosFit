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

            for (int i = 1; i <= countPage; i++)
            {
                string pagina = wc.DownloadString("https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/ss-series/swsh1/" + i + "/");

                /*Console.WriteLine(pagina);*/

                var htmlDocument = new HtmlDocument();



                htmlDocument.LoadHtml(pagina);


                string name = string.Empty;
                string status = string.Empty;
                string statusSub = string.Empty;

                try
                {

                    

                    foreach (HtmlNode node in htmlDocument.DocumentNode.Descendants())
                    {

                        if(htmlDocument.DocumentNode.HasChildNodes && htmlDocument.DocumentNode != null) { 

                        Cards card;

                            if (node.Attributes.Count > 0)
                            {
                                name = node.Descendants().First(x => x.Attributes["class"] != null &&
                                x.Attributes["class"].Value.Equals("color-block color-block-gray")).InnerText;

                                status = WebUtility.HtmlDecode(node.SelectNodes("/html[1]/body[1]/div[4]/section[1]/div[2]/div[1]/ div[2]/h3/div[2]").First().InnerHtml);


                                statusSub = WebUtility.HtmlDecode(node.Descendants().First().SelectNodes("/html[1]/body[1]/div[4]/ section[1]/div[2]/div[1]/div[3]/div[4]/h3/a").First().InnerHtml);


                                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(statusSub)) { 

                                    card = new Cards(i);



                                pk.Add(new Pokemon(name, status, statusSub));

                                foreach (Pokemon pkl in pk)
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
                catch (Exception e)
                {
                    Console.WriteLine("message error" + e.Message);
                }




            }
        }
    }

}
