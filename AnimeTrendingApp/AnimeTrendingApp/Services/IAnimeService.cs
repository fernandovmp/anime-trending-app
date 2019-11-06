using AnimeTrendingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AnimeTrendingApp.Services
{
    public interface IAnimeService
    {
        Task<ObservableCollection<Anime>> GetTrendingAnimes();
        Task<ObservableCollection<Anime>> GetSeasonTrendingAnimes();
    }
}
