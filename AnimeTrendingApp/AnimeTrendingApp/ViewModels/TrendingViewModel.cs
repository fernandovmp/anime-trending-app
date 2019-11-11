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
            GetTrendings();
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

        private async Task FetchCollection(Task<ObservableCollection<Anime>> fetchTask)
        {
            IsBusy = true;
            Trendings = await fetchTask;
            OnPropertyChanged(nameof(Trendings));
            IsBusy = false;
        }

        private void GetTrendings() => Task.Run(() => FetchCollection(_animeService.GetTrendingAnimes()));

        private void GetSeasonTrendings() => Task.Run(() => FetchCollection(_animeService.GetSeasonTrendingAnimes()));

        private void ApplyFilter()
        {
            if(Filter == Filter.All)
            {
                Filter = Filter.Season;
                GetSeasonTrendings();
                FilterToolbarItemText = "Season";
                return;
            }
            Filter = Filter.All;
            GetTrendings();
            FilterToolbarItemText = "All";
        }
    }
}
