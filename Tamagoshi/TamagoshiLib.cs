using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Tamagoshi.ApiPokemon;
using Tamagoshi.Model;

namespace Tamagoshi
{
    public static class TamagoshiLib
    {

        private static string CacheFolder = null;
        private static List<Mascote> MascoteMemoryCache = new List<Mascote>();
        private const int MaxMemoryCache = 100;

        static TamagoshiLib()
        {
            if (CacheFolder == null)
            {
                var path = Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                CacheFolder = Path.Combine(path, "MascoteCache");
            }
            if (!Directory.Exists(CacheFolder))
            {
                try
                {
                    Directory.CreateDirectory(CacheFolder);
                }
                catch { }
            }
        }

        #region SaveLoadCache
        private static Mascote GetMascoteFromDisk(string name)
        {


            var file = Path.Combine(CacheFolder, $"{name}.json");
            if (!File.Exists(file))
                return null;

            return JsonSerializer.Deserialize<Mascote>(File.ReadAllText(file));

        }
        private static void AddMascoteToCache(Mascote mascote)
        {
            var file = Path.Combine(CacheFolder, $"{mascote.LowerName}.json");

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var bytes = JsonSerializer.SerializeToUtf8Bytes(mascote);
                fs.Write(bytes, 0, bytes.Length);
            }

            if (MascoteMemoryCache.Count >= MaxMemoryCache)
                MascoteMemoryCache.RemoveAt(0);

            MascoteMemoryCache.Add(mascote);
        }
        private static Mascote GetMascoteFromCache(string name)
        {
            var cachedPokemon = MascoteMemoryCache.Where(m => m.LowerName == name).FirstOrDefault();
            if (cachedPokemon != null)
                return cachedPokemon;

            cachedPokemon = GetMascoteFromDisk(name);
            if (cachedPokemon != null)
            {
                if (MascoteMemoryCache.Count >= MaxMemoryCache)
                    MascoteMemoryCache.RemoveAt(0);

                MascoteMemoryCache.Add(cachedPokemon);
            }
            return cachedPokemon;
        }
        #endregion

        private static async Task<string> Base64Image(string url)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = await webClient.DownloadDataTaskAsync(url);
                return Convert.ToBase64String(imageBytes);
            }
        }
        
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

            var mascote = GetMascoteFromCache(nameOrID);
            if (mascote != null)
                return mascote;

            var pokemon = await PokemonService.GetPokemon(nameOrID);

            /*if (pokemon == null)
                return new Mascote();//ERRO*/

            string enName = pokemon.name;
            string jpName = pokemon.name;

            var especies = await PokemonService.GetPokemonEspecies(pokemon.species.url);
            var en = especies.names.Where(x => x.language.name == "en").FirstOrDefault();
            if (en != null)
                enName = en.name;

            var jp = especies.names.Where(x => x.language.name == "ja").FirstOrDefault();
            if (jp != null)
                jpName = jp.name;

            var icon =  await Base64Image(pokemon.sprites.front_default);
            var image = await Base64Image(pokemon.sprites.other.Official_Artwork.front_default);

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

            mascote = new Mascote()
            {
                LowerName = pokemon.name,
                Altura = pokemon.height,
                Peso = pokemon.weight,
                ID = pokemon.id,
                Base64Icon = icon,
                Base64Sprite = image,
                Name = enName,
                JapaneseName = jpName,
                GrowthRate = Enums.GetGrowthRate(especies.growth_rate),
                Habitat = Enums.GetHabitat(especies.habitat),
                RegressTo = m_regressTo,
                Evolutions = m_evolutions.Count > 0 ? m_evolutions.ToArray() : null,
            };

            

            

            /**/


            AddMascoteToCache(mascote);
            return mascote;
        }
        public static async Task<Mascote> GetPokemonInfo(string nameOrID)
        {
            return await GetPokemonInfo(nameOrID, null);
        }

        public static async Task<int> LoadAndGetPokemonsCount()
        {
            return await PokemonService.GetPokemonsApiCount();
        }

    }
}
