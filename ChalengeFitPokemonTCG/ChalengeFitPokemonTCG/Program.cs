using ChalengeFitPokemonTCG.Entities;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ChalengeFitPokemonTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pokemon> pk = new List<Pokemon>();
            Console.WriteLine("1 - Acesse o site https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/");
            Console.WriteLine("2 - Realize uma sem preencher nenhum campo (clicando em Search)");
            Console.WriteLine("3 - Salvar em um arquivo unico?");
            Console.WriteLine("4 - Sim | Não");

            
            string singleOrAll =  Console.ReadLine();
            Console.Write("3 - Informe abaixo a quantidade de paginas: ");
           
            int countPage = int.Parse(Console.ReadLine());

            var wc = new WebClient();
            var wc2 = new WebClient();


            for (int i = 1; i <= countPage; i++)
            {
                string pagina = wc.DownloadString("https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/" + i + "?cardName=&cardText=&evolvesFrom=&simpleSubmit=&format=unlimited&hitPointsMin=0&hitPointsMax=340&retreatCostMin=0&retreatCostMax=5&totalAttackCostMin=0&totalAttackCostMax=5&particularArtist=&sort=number&sort=number");



                var htmlDocument = new HtmlDocument();

                List<string> link = new List<string>();



                htmlDocument.LoadHtml(pagina);


                string name = string.Empty;
                string abilities = string.Empty;
                string status = string.Empty;
                string img = string.Empty;
                string base64Img = string.Empty;
                


                var nodeCollection = htmlDocument.GetElementbyId("cardResults").ChildNodes;


                foreach (HtmlNode node in htmlDocument.GetElementbyId("cardResults").ChildNodes)
                {
                    string linkpage;

                    linkpage = node.InnerHtml;

                    if (linkpage.Contains("<a href="))
                    {

                        linkpage = linkpage.Substring(linkpage.IndexOf("<a href="), linkpage.IndexOf(">")).Replace("<a href=", "").Replace(">", "").Replace("\"", "");
                        link.Add(linkpage);

                    }

                }

                var htmldocumentLink = new HtmlDocument();

                foreach (string li in link)
                {

                    string cards = wc2.DownloadString("https://www.pokemon.com" + li);

                    htmldocumentLink.LoadHtml(cards);

                    var nodes = htmldocumentLink.QuerySelectorAll("section .card-detail");

                    try
                    {
                        foreach (HtmlNode node in nodes)
                        {
                            Cards card;

                            if (node.Attributes.Count > 0)
                            {

                                name = node.QuerySelector(".card-description h1 ").InnerText;

                                abilities = node.QuerySelector(".stats-footer h3 a").InnerText;


                                status = node.QuerySelector(".stats-footer span").InnerText;

                                img = node.QuerySelector("div .card-image").InnerHtml;


                                card = new Cards(i);

                                img = card.ConvertAndSplitToBase64(img);

                                base64Img = card.convertBase64Img(img);

                                pk.Add(new Pokemon(name, status, abilities, img, base64Img));

                                StreamWriter sw = new StreamWriter(@"C:\Users\ryoji.kitano\Documents\Ryoji-Projetos\DesafiosFit\PokemonJson\pokemon.json");

                                foreach (Pokemon pkl in pk)
                                {

                                    if(singleOrAll == "sim") { 
                                    var json = JsonConvert.SerializeObject(pkl);


                                    sw.WriteLine(json);
                                    }
                                    else
                                    {

                                    }
                                }

                                sw.Close();
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
        }
    }

}
