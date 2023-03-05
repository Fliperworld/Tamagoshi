namespace Tamagoshi.ApiPokemon
{
    internal class Pokemon
    {
        public Abilities[] abilities { get; set; }
        public int base_experience { get; set; }

        public int height { get; set; }
        public int id { get; set; }

        public string name { get; set; }
        public NameURL species { get; set; }

        public Sprites sprites { get; set; }
        public int weight { get; set; }
    }
}
