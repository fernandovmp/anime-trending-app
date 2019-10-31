using AnimeTrendingApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimeTrendingApp.Services
{
    public class AnimeService
    {
        private readonly HttpClient _client;
        public const string API_BASE_URL = "https://kitsu.io/api/edge";
        public const string TRENDING_ANIMES_ENDPOINT = API_BASE_URL + "/trending/anime";

        public AnimeService()
        {
            _client = new HttpClient();
        }

        public async Task<ObservableCollection<Anime>> GetTrendingAnimes()
        {
            var animes = new ObservableCollection<Anime>();

            var uri = new Uri(TRENDING_ANIMES_ENDPOINT);
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var definition = new
                {
                    Data = new[]
                    {
                        new { Attributes = new Anime() }
                    }
                };

                var animesList = JsonConvert.DeserializeAnonymousType(content, definition).Data;
                foreach (var anime in animesList)
                {
                    animes.Add(anime.Attributes);
                }
            }
            return animes;
        }
    }
}
