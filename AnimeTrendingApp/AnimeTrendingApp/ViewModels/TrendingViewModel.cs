using AnimeTrendingApp.Models;
using AnimeTrendingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AnimeTrendingApp.ViewModels
{
    public enum Filter { All, Season }
    public class TrendingViewModel : BaseViewModel
    {
        private readonly IAnimeService _animeService;
        private bool _isBusy;
        private Filter _filter;
        private string _filterToolbarItemText = "All";

        public TrendingViewModel(IAnimeService animeService)
        {
            _animeService = animeService;
            Task.Run(() => GetTrendings());
            FilterCommand = new Command(ApplyFilter);
        }

        public ObservableCollection<Anime> Trendings { get; private set; }
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        public ICommand FilterCommand { get; private set; }
        public Filter Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }
        public string FilterToolbarItemText
        {
            get => _filterToolbarItemText;
            set => SetProperty(ref _filterToolbarItemText, value);
        }

        private async Task GetTrendings()
        {
            IsBusy = true;
            Trendings = await _animeService.GetTrendingAnimes();
            OnPropertyChanged(nameof(Trendings));
            IsBusy = false;
        }

        private async Task GetSeasonTrendings()
        {
            IsBusy = true;
            Trendings = await _animeService.GetSeasonTrendingAnimes();
            OnPropertyChanged(nameof(Trendings));
            IsBusy = false;
        }

        private void ApplyFilter()
        {
            if(Filter == Filter.All)
            {
                Filter = Filter.Season;
                Task.Run(() => GetSeasonTrendings());
                FilterToolbarItemText = "Season";
                return;
            }
            Filter = Filter.All;
            Task.Run(() => GetTrendings());
            FilterToolbarItemText = "All";
        }
    }
}
