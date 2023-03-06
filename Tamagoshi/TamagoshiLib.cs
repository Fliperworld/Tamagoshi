using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tamagoshi.ApiPokemon;
using Tamagoshi.Model;

namespace Tamagoshi
{   
    public static class TamagoshiLib
    {
        private static EvolvesTo[] GetEvolves(EvolvesTo evolves,string name)
        {
            if(evolves.species.name == name) return evolves.evolves_to;

            foreach(var ev in evolves.evolves_to)
            {
                var response = GetEvolves(ev, name);
                if(response != null && response.Length > 0) return response;
            }
            return null;
        }
        private static async Task<Mascote> GetPokemonInfo(string nameOrID, EvolvesTo EvolutionList)
        {
            nameOrID = nameOrID.ToLower();

            var pokemon = await PokemonService.GetPokemon(nameOrID);
            
            string enName = pokemon.name;
            string jpName = pokemon.name;

            var especies = await PokemonService.GetPokemonEspecies(pokemon.species.url);
            var en = especies.names.Where(x => x.language.name == "en").FirstOrDefault();
            if (en != null)
                enName = en.name;

            var jp = especies.names.Where(x => x.language.name == "ja").FirstOrDefault();
            if (jp != null)
                jpName = jp.name;
          
            if (EvolutionList == null)
            {
                var Echain = await PokemonService.GetPokemonEvolutionChain(especies.evolution_chain.url);
                EvolutionList = Echain.chain;
            }

            var evolutions = GetEvolves(EvolutionList, pokemon.name);
            List<string> m_evolutions = new List<string>();
            if (evolutions != null)
            {
                foreach (var ev in evolutions)
                {
                    var mascoteEv = await GetPokemonInfo(ev.species.name, ev);
                    m_evolutions.Add(mascoteEv.Name);
                }
            }

            string m_regressTo = null;
            if (especies.evolves_from_species != null)
            {
                var m_pokemon = await PokemonService.GetPokemon(especies.evolves_from_species.name);
                var m_especies = await PokemonService.GetPokemonEspecies(m_pokemon.species.url);
                var m_en = m_especies.names.Where(x => x.language.name == "en").FirstOrDefault();
                if(m_en != null)
                    m_regressTo = m_en.name;
            }

            var mascote = new Mascote()
            {
                LowerName = pokemon.name,
                Altura = pokemon.height,
                Peso = pokemon.weight,
                ID = pokemon.id,
                IconURL = pokemon.sprites.front_default,
                SpriteURL = pokemon.sprites.other.Official_Artwork.front_default,
                Name = enName,
                JapaneseName = jpName,
                GrowthRate = Enums.GetGrowthRate(especies.growth_rate),
                Habitat = Enums.GetHabitat(especies.habitat),
                RegressTo = m_regressTo,
                Evolutions = m_evolutions.Count > 0 ? m_evolutions.ToArray() : null,
            };
        
            return mascote;
        }
        public static async Task<Mascote> GetPokemonInfo(string nameOrID)
        {
            return await GetPokemonInfo(nameOrID, null);
        }
        public static async Task<byte[]> GetMascoteIcon(Mascote mascote)
        {
            return await CacheControl.GetOrDownloadImage(mascote.IconURL);
        }
        public static async Task<byte[]> GetMascoteSprite(Mascote mascote)
        {
            return await CacheControl.GetOrDownloadImage(mascote.SpriteURL);
        }
        public static async Task<int> LoadAndGetPokemonsCount()
        {
            return await PokemonService.GetPokemonsApiCount();
        }

    }
}
