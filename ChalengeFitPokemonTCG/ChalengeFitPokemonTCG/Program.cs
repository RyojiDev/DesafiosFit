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
            var wc2 = new WebClient();

            for (int i = 1; i <= countPage; i++)
            {
                string pagina = wc.DownloadString("https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/" + i +"?cardName=&cardText=&evolvesFrom=&simpleSubmit=&format=unlimited&hitPointsMin=0&hitPointsMax=340&retreatCostMin=0&retreatCostMax=5&totalAttackCostMin=0&totalAttackCostMax=5&particularArtist=&sort=number&sort=number");

      

                var htmlDocument = new HtmlDocument();

                List<string> link = new List<string>();
                


                htmlDocument.LoadHtml(pagina);


                string name = string.Empty;
                string abilities = string.Empty;
                string status = string.Empty;

                var nodeCollection = htmlDocument.GetElementbyId("cardResults").ChildNodes;


                foreach (HtmlNode node in htmlDocument.GetElementbyId("cardResults").ChildNodes)
                {
                    string linkpage;


                         linkpage = node.InnerHtml;
                       


                        if (linkpage.Contains("<a href="))
                        {

                            linkpage = linkpage.Substring(linkpage.IndexOf("<a href="), linkpage.IndexOf(">")).Replace("<a href=", "").Replace(">", "").Replace("\"", ""); // node.Descendants().Select

                            link.Add(linkpage);

                        }


                }

                foreach (string li in link)
                {

                
                string cards = wc2.DownloadString("https://www.pokemon.com" + li);

                    var htmldocumentLink = new HtmlDocument();

                    htmldocumentLink.LoadHtml(cards);


                    try
                    {


                        foreach (HtmlNode node in htmldocumentLink.DocumentNode.SelectNodes("/html[1]/body[1]"))
                        {

                            var teste = htmldocumentLink.DocumentNode.Descendants().First(x => x.Attributes["class"].Value.Equals("pokemon-abilities")).XPath;

                            Cards card;

                               if (node.Attributes.Count > 0)
                                {

                                    name = node.Descendants().First(x => x.Attributes["class"] != null &&
                                    x.Attributes["class"].Value.Equals("color-block color-block-gray")).XPath;

                                    abilities = WebUtility.HtmlDecode(node.SelectNodes("/html[1]/body[1]/div[4]/section[1]/div[2]/div[1]/div[2]/h3/div[2]").First().InnerHtml);


                                    status = WebUtility.HtmlDecode(node.Descendants().First().SelectNodes("/html[1]/body[1]/div[4]/ section[1]/div[2]/div[1]/div[3]/div[4]/h3").First().InnerHtml);

                                        card = new Cards(i);



                                    pk.Add(new Pokemon(name, abilities, status));

                                    foreach (Pokemon pkl in pk)
                                    {
                                        Console.WriteLine(pkl.Name);
                                        Console.WriteLine(pkl.Abilities);
                                        Console.WriteLine(pkl.Status);


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

}
