namespace ChalengeFitPokemonTCG.Entities
{
    class Pokemon
    {
        
        public string Name { get; set; }
        public string NumberExpansion { get; set; }
        public string Expansion { get; set; }
        public string LinkImg { get; set; }
        

        public string Base64Img { get; set; }

        public Pokemon()
        {

        }

        public Pokemon(string name, string numberExpansion, string expansion, string img, string base64Img)
        {
            Name = name;
            NumberExpansion = numberExpansion;
            Expansion = expansion;
            LinkImg = img;
            Base64Img = base64Img;
        }

        
    }
}
