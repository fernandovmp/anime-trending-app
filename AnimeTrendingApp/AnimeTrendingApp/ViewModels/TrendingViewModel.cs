using AnimeTrendingApp.Models;
using AnimeTrendingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AnimeTrendingApp.ViewModels
{
    public class TrendingViewModel : BaseViewModel
    {
        private readonly AnimeService _animeService;

        public TrendingViewModel()
        {
            _animeService = new AnimeService();
            Task.Run(() => GetTrendings());
        }

        public ObservableCollection<Anime> Trendings { get; private set; }

        private async Task GetTrendings()
        {
            Trendings = await _animeService.GetTrendingAnimes();
            OnPropertyChanged(nameof(Trendings));
        }
    }
}
