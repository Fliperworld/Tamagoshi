namespace Tamagoshi.ApiPokemon
{
    internal class EvolutionChain
    {
        public EvolvesTo chain { get; set; }
        
    }

    internal class EvolvesTo
    {
        public EvolvesTo[] evolves_to { get; set; }
        public bool is_baby { get; set; }
        public NameURL species { get; set; }
    }
   
}
