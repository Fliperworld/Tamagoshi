namespace Tamagoshi.ApiPokemon
{
    internal class PokemonEspecies
    {
        public int base_happiness { get; set; }
        public int capture_rate { get; set; }

        public NameURL evolution_chain { get; set; } = null;
        public NameURL evolves_from_species { get; set; } = null;
        public NameURL growth_rate { get; set; }
        public NameURL habitat { get; set; }

        public Names[] names { get; set; }

        public Varieties[] varieties { get; set; }
    }

    internal class Varieties
    {
        public bool is_default = true;
        public NameURL pokemon { get; set; }
    }
}
