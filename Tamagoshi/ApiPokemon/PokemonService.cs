using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tamagoshi.ApiPokemon
{
    internal static class PokemonService
    {
        private const int Limit = 1279;
        private const string ApiURL = "https://pokeapi.co/api/v2";
        private static bool isInitialized;

        private static Dictionary<string, string> PokemonsNames;
        private static RestClient ApiClient;
        static PokemonService()
        {
            isInitialized = false;
            PokemonsNames = new Dictionary<string, string>();
            ApiClient = new RestClient(ApiURL);
            AsyncInitialization();
        }

        private static async void AsyncInitialization()
        {
            var request = new RestRequest("/pokemon", Method.Get);
            request.AddParameter("limit",Limit);

            var response = await ApiClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                BaseApi data = JsonSerializer.Deserialize<BaseApi>(response.Content);
                var total = data.count;
                var urlSize = ApiURL.Length;
                foreach (var r in data.results)
                    PokemonsNames.Add(r.name, r.url.Remove(0,urlSize));

                while (data.next != null)
                {
                    response = await ApiClient.ExecuteAsync(new RestRequest(data.next.Remove(0, urlSize), Method.Get));
                    if (!response.IsSuccessful)
                        throw new ApiResponseException();
                   
                    data = JsonSerializer.Deserialize<BaseApi>(response.Content);
                    foreach (var r in data.results)
                        PokemonsNames.Add(r.name, r.url.Remove(0,urlSize));
                }

                if (PokemonsNames.Count != total)
                    throw new MissmatchException();
            
            }
            else
                throw new ApiResponseException();

            isInitialized = true;
        } 

        internal static async Task<int> GetPokemonsApiCount()
        {
            while (!isInitialized)
                await Task.Delay(50);

            return PokemonsNames.Count;
        }
        internal static async Task<Pokemon> GetPokemon(string name)
        {
            while(!isInitialized)
                await Task.Delay(50);

            if (!PokemonsNames.ContainsKey(name))
                return null;

            var response = await ApiClient.ExecuteAsync(new RestRequest(PokemonsNames[name], Method.Get));
            
            if (!response.IsSuccessful)
                throw new ApiResponseException();

            return JsonSerializer.Deserialize<Pokemon>(response.Content);
        }

        internal static async Task<PokemonEspecies> GetPokemonEspecies(string url)
        {
            url = url.Remove(0, ApiURL.Length);

            var response = await ApiClient.ExecuteAsync(new RestRequest(url, Method.Get));

            if (!response.IsSuccessful)
                throw new ApiResponseException();

            return JsonSerializer.Deserialize<PokemonEspecies>(response.Content);
        }

        internal static async Task<EvolutionChain> GetPokemonEvolutionChain(string url)
        {
            url = url.Remove(0, ApiURL.Length);

            var response = await ApiClient.ExecuteAsync(new RestRequest(url, Method.Get));

            if (!response.IsSuccessful)
                throw new ApiResponseException();

            return JsonSerializer.Deserialize<EvolutionChain>(response.Content);
        }
    }
}
