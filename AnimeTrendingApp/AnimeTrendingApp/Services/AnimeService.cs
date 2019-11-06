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
    public class AnimeService : IAnimeService
    {
        public const string API_BASE_URL = "https://kitsu.io/api/edge";
        public const string TRENDING_ANIMES_ENDPOINT = API_BASE_URL + "/trending/anime";
        public const string ANIME_ENDPOINT = API_BASE_URL + "/anime";

        private readonly HttpClient _client;
        private readonly string[] _seasons =
        {
            "winter",
            "spring",
            "summer",
            "fall"
        };
        
        public AnimeService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        private async Task<ObservableCollection<Anime>> GetAnimeCollection(HttpResponseMessage response)
        {
            var animes = new ObservableCollection<Anime>();
            var definition = new
            {
                Data = new[]
                {
                        new { Attributes = new Anime() }
                    }
            };
            string content = await response.Content.ReadAsStringAsync();
            var animesList = JsonConvert.DeserializeAnonymousType(content, definition).Data;
            foreach (var anime in animesList)
            {
                animes.Add(anime.Attributes);
            }
            return animes;
        }

        public async Task<ObservableCollection<Anime>> GetTrendingAnimes()
        {
            var uri = new Uri(TRENDING_ANIMES_ENDPOINT);
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await GetAnimeCollection(response);
            }
            return new ObservableCollection<Anime>();
        }

        public async Task<ObservableCollection<Anime>> GetSeasonTrendingAnimes()
        {
            int year = DateTime.Today.Year;
            string season = _seasons[(DateTime.Today.Month - 1) / 3];
            var uri = new Uri(ANIME_ENDPOINT +
                $"?filter[status]=current&filter[season]={season}&filter[year]={year}&sort=-user_count");
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await GetAnimeCollection(response);
            }
            return new ObservableCollection<Anime>();
        }
    }
}
