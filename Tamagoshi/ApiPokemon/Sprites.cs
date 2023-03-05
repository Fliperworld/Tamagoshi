using System.Text.Json.Serialization;

namespace Tamagoshi.ApiPokemon
{
    internal class Sprites
    {
        public string front_default { get; set; }
        public string front_female { get; set; }

        public string front_shiny { get; set; }
        public string front_shiny_female { get; set; }

        public Other other { get; set; }
    }

    internal class OfficialArtwork
    {
        public string front_default { get; set; }
        public string front_shiny { get; set; }
    }
    internal class Other
    {
        [JsonPropertyName("official-artwork")]
        public OfficialArtwork Official_Artwork { get; set; }
    }
}
