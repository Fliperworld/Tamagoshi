namespace Tamagoshi.ApiPokemon
{
    internal class BaseApi
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public NameURL[] results { get; set; }
    }
}
