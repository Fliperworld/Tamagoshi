using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tamagoshi.ApiPokemon
{
    internal static class PokemonService
    {
        internal const string ApiBaseURL = "https://pokeapi.co/api/v2";
        internal const string SpritesBaseURL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/";

        private static bool isInitialized;

        private static RestClient ApiClient;
        static PokemonService()
        {
            isInitialized = false;
            ApiClient = new RestClient(ApiBaseURL);
            AsyncInitialization();
        }

        private static async void AsyncInitialization()
        {
            int CachePokemonIDcount = CacheControl.PokemonsIDsCount;
            int ApiPokemonIDCount = 0;

            var request = new RestRequest("/pokemon/", Method.Get);
            request.AddParameter("limit", 1);

            var response = await ApiClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                BaseApi data = JsonSerializer.Deserialize<BaseApi>(response.Content);
                ApiPokemonIDCount = data.count;
            }
            else
            {
                isInitialized = true;
                if (CachePokemonIDcount > 0)
                    throw new MissmatchException();//can ignore
                else
                    throw new TamagoshiFatalException();
            }

            //NoChanges neeeded.
            if (CachePokemonIDcount == ApiPokemonIDCount)
            {
                isInitialized = true;
                return;
            }


            List<NameURL> names = new List<NameURL>();
            request = new RestRequest("/pokemon/", Method.Get);
            request.AddParameter("limit", ApiPokemonIDCount);
            response = await ApiClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                BaseApi data = JsonSerializer.Deserialize<BaseApi>(response.Content);
                names.AddRange(data.results);
            }

            var inseridos = CacheControl.UpdatePokemonsIdTable(names);

            isInitialized = true;
        }

        internal static async Task<int> GetPokemonsApiCount()
        {
            while (!isInitialized)
                await Task.Delay(50);

            return CacheControl.PokemonsIDsCount;
        }
        internal static async Task<Pokemon> GetPokemon(string name)
        {
            while (!isInitialized)
                await Task.Delay(50);

            var uri = CacheControl.GetPokemonUri(name);

            if (uri == null)
                return null;

            var json = CacheControl.GetCachedJson(uri);
            if (json != null)
            {
                return JsonSerializer.Deserialize<Pokemon>(json);
            }

            var response = await ApiClient.ExecuteAsync(new RestRequest(uri, Method.Get));
            if (!response.IsSuccessful)
                throw new ApiResponseException();

            CacheControl.StoreCachedJson(uri, response.Content);

            return JsonSerializer.Deserialize<Pokemon>(response.Content);
        }

        internal static async Task<PokemonEspecies> GetPokemonEspecies(string url)
        {
            var uri = url.Substring(ApiBaseURL.Length);

            var json = CacheControl.GetCachedJson(uri);
            if (json != null)
            {
                return JsonSerializer.Deserialize<PokemonEspecies>(json);
            }

            var response = await ApiClient.ExecuteAsync(new RestRequest(uri, Method.Get));

            if (!response.IsSuccessful)
                throw new ApiResponseException();

            CacheControl.StoreCachedJson(uri, response.Content);
            
            return JsonSerializer.Deserialize<PokemonEspecies>(response.Content);
        }

        internal static async Task<EvolutionChain> GetPokemonEvolutionChain(string url)
        {
            var uri = url.Substring(ApiBaseURL.Length);

            var json = CacheControl.GetCachedJson(uri);
            if (json != null)
            {
                return JsonSerializer.Deserialize<EvolutionChain>(json);
            }

            var response = await ApiClient.ExecuteAsync(new RestRequest(url, Method.Get));

            if (!response.IsSuccessful)
                throw new ApiResponseException();

            CacheControl.StoreCachedJson(uri, response.Content);

            return JsonSerializer.Deserialize<EvolutionChain>(response.Content);
        }
    }
}
