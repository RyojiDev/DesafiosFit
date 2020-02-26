namespace ChalengeFitPokemonTCG.Entities
{
    class Pokemon
    {
        
        public string Name { get; set; }
        public string Abilities { get; set; }
        public string Status { get; set; }
        public string LinkImg { get; set; }
        

        public string Base64Img { get; set; }

        public Pokemon()
        {

        }

        public Pokemon(string name, string abilities, string status, string img, string base64Img)
        {
            Name = name;
            Abilities = abilities;
            Status = status;
            LinkImg = img;
            Base64Img = base64Img;
        }

        
    }
}
