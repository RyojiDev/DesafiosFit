using ChalengeFitPokemonTCG.Entities;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ChalengeFitPokemonTCG
{
    class Program
    {
        static bool  arquivosMultiplos = false;

        static void Produce(int pages, ITargetBlock<Pokemon> target)
        {

            Parallel.For(1, pages + 1, (i) =>
            {
                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(Helper.GetLink(i));

                var nodes = htmlDoc.DocumentNode.QuerySelectorAll("#cardResults > li a");

                Parallel.ForEach(nodes, (item) =>
                {
                    var htmlItem = web.Load(Helper.GetLink(0, item.GetAttributeValue("href", "")));
                    string description = htmlItem.DocumentNode.QuerySelector(".card-description h1").InnerText;
                    string expansion = htmlItem.DocumentNode.QuerySelector(".stats-footer a").InnerText;
                    string expansionNumber = htmlItem.DocumentNode.QuerySelector(".stats-footer span").InnerText;
                    string urlCardImage = htmlItem.DocumentNode.QuerySelector(".card-image img").GetAttributeValue("src", "");
                    string base64CardImage = Convert.ToBase64String(new HttpClient().GetByteArrayAsync(urlCardImage).Result);

                    target.Post(new Pokemon(description, expansionNumber, expansion, base64CardImage));
                });
            });

        }

        static async Task ConsumeAsync(ISourceBlock<Pokemon> source)
        {
            BlockingCollection<Pokemon> bag = new BlockingCollection<Pokemon>();

            while(await source.OutputAvailableAsync())
            {
                Pokemon data = (Pokemon)source.Receive();
                bag.Add(data);

                if (!arquivosMultiplos)
                    await CriarArquivoUnico(bag);
                else
                    await CriarArquivMultiplo(bag);
            }
        }
        static void Main(string[] args)
        {
            
            Console.WriteLine("1 - Acesse o site https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/");
            Console.WriteLine("2 - Realize uma sem preencher nenhum campo (clicando em Search)");
            Console.Write("Entre com o numero de paginas a ser varridas: ");
            int countPage = int.Parse(Console.ReadLine());
            Console.Write("Você deseja salvar em multiplos arquivos ? (true/false): ");
            arquivosMultiplos = bool.Parse(Console.ReadLine());


            var buffer = new BufferBlock<Pokemon>();
            var consumer = ConsumeAsync(buffer);

            Produce(countPage, buffer);

            consumer.Wait();
           
        }

        static async Task CriarArquivoUnico(BlockingCollection<Pokemon> list)
        {
            using (StreamWriter file = File.CreateText(Helper.GetPath("single_file_pokemons.json")))
            {
                var json = JsonConvert.SerializeObject(list);
                await file.WriteAsync(json);
            }
        }

        static async Task CriarArquivMultiplo(BlockingCollection<Pokemon> list)
        {
            foreach (var pokemon in list)
            {
                using (StreamWriter file = File.CreateText(Helper.GetPath("multiple_file_" + pokemon.Description + ".json")))
                {
                    var json = JsonConvert.SerializeObject(pokemon);
                    await file.WriteAsync(json);
                }
            }
        }


    }

}
