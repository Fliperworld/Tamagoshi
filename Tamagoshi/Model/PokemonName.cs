using System.Linq;
using System.Threading.Tasks;
using Tamagoshi.ApiPokemon;

namespace Tamagoshi.Model
{
    public class PokemonName
    {
        private string m_enName = null;
        private string m_jpName = null;


        public readonly int ID;
        public readonly string Identifier;
        public string DisplayName { get; private set; }


        public async Task<string> GetEnglishName()
        {
            if (m_enName == null)
            {
                var pokemon = await PokemonService.GetPokemon(Identifier);
                var especies = await PokemonService.GetPokemonEspecies(pokemon.species.url);
                var result = especies.names.Where(x => x.language.name == "en").FirstOrDefault();
                if (result != null)
                {
                    m_enName = result.name;
                    DisplayName = result.name;
                }
            }
            return m_enName;
        }
        public async Task<string> GetJapaneseName()
        {
            if (m_jpName == null)
            {
                var pokemon = await PokemonService.GetPokemon(Identifier);
                var especies = await PokemonService.GetPokemonEspecies(pokemon.species.url);
                var result = especies.names.Where(x => x.language.name == "ja").FirstOrDefault();
                if (result != null)
                    m_jpName = result.name;
            }
            return m_jpName;
        }

        public PokemonName(int id, string identifier, string enname, string jpname)
        {
            ID = id;
            Identifier = identifier;
            DisplayName = m_enName = enname;
            m_jpName = jpname;
        }
        public PokemonName(int id, string identifier)
        {
            ID = id;
            Identifier = identifier;
            DisplayName = char.ToUpper(identifier[0]) + identifier.Substring(1);
        }

        public override string ToString()
        {
            return (m_jpName != null) ? $"{DisplayName} ({m_jpName})"  : DisplayName;
        }
    }
}
