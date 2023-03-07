using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tamagoshi.ApiPokemon;
using Tamagoshi.Model;

namespace Tamagoshi
{

    internal static class CacheControl
    {
        private static SQLiteConnection sqliteConnection;
        private static readonly string ConnectionString;
        private static string CacheFolder;
        static CacheControl()
        {

            var path = Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            CacheFolder = Path.Combine(path, "TamagoshiCache");


            if (!Directory.Exists(CacheFolder))
                Directory.CreateDirectory(CacheFolder);

            var dbCacheFile = Path.Combine(CacheFolder, "DataBaseCache.sqlite");
            ConnectionString = $"Data Source=\"{dbCacheFile}\"; Version=3;";

            if (!File.Exists(dbCacheFile))
            {
                SQLiteConnection.CreateFile(dbCacheFile);
                ExecuteCommand("CREATE TABLE Pokemon_IDS(id INTEGER UNIQUE PRIMARY KEY, identifier TEXT NOT NULL,uri TEXT NOT NULL)");
                ExecuteCommand("CREATE TABLE Json_cache(uri TEXT UNIQUE PRIMARY KEY, JSON TEXT NOT NULL)");
            }


        }
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection(ConnectionString);
            sqliteConnection.Open();
            return sqliteConnection;
        }

        private static int ExecuteCommand(string command)
        {
            int ret;
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = command;
                ret = cmd.ExecuteNonQuery();
            }
            return ret;
        }

        internal static int PokemonsIDsCount
        {
            get
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Pokemon_IDS";
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        internal static List<PokemonName> GetPokemonsNames()
        {
            List<PokemonName> ret = new List<PokemonName>();

            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT id,identifier FROM Pokemon_IDS";
                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ret.Add(new PokemonName(rdr.GetInt32(0),rdr.GetString(1)));
                }

            }
            return ret.Count == 0 ? null : ret;

        }

        internal static string GetPokemonUri(string pokemonName)
        {
            using (var cmd = DbConnection().CreateCommand())
            {
                var identifier = pokemonName.ToLower();
                cmd.CommandText = $"SELECT uri FROM Pokemon_IDS WHERE identifier = '{identifier}' LIMIT 1";
                var result = cmd.ExecuteScalar();
                return result == null ? null : result.ToString();
            }
        }

        internal static string GetCachedJson(string uri)
        {
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = $"SELECT JSON FROM Json_cache WHERE uri = '{uri}' LIMIT 1";
                var result = cmd.ExecuteScalar();
                return result == null ? null : result.ToString();
            }
        }

        internal static int StoreCachedJson(string uri, string JSON)
        {
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO Json_cache(uri,JSON) VALUES ($uri,$json)";
                cmd.Parameters.AddWithValue("$uri", uri);
                cmd.Parameters.AddWithValue("$json", JSON);
                return cmd.ExecuteNonQuery();
            }
        }

        internal static int UpdatePokemonsIdTable(List<NameURL> names)
        {
            int ret = 0;
            var connection = DbConnection();
            int lenght = PokemonService.ApiBaseURL.Length;

            using (var transaction = connection.BeginTransaction())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT OR REPLACE INTO Pokemon_IDS (id,identifier,uri) VALUES ($id,$name,$uri)";

                var parameterID = command.CreateParameter();
                parameterID.ParameterName = "$id";
                command.Parameters.Add(parameterID);
                var parameterName = command.CreateParameter();
                parameterName.ParameterName = "$name";
                command.Parameters.Add(parameterName);
                var parameterUri = command.CreateParameter();
                parameterUri.ParameterName = "$uri";
                command.Parameters.Add(parameterUri);


                foreach (var data in names)
                {
                    var id = data.url.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).Last();
                    parameterID.Value = id;
                    parameterName.Value = data.name;
                    parameterUri.Value = data.url.Substring(lenght);
                    ret += command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            return ret;
        }

        internal static async Task<byte[]> GetOrDownloadImage(string url)
        {
            var uri = url.Substring(PokemonService.SpritesBaseURL.Length);
            var file = Path.Combine(CacheFolder, uri);
            if (File.Exists(file))
                return File.ReadAllBytes(file);

            var folder = Path.GetDirectoryName(file);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            using (var webClient = new WebClient())
            {
                var imageBytes = await webClient.DownloadDataTaskAsync(url);
                File.WriteAllBytes(file, imageBytes);
                return imageBytes;
            }
        }

    }
}
