using AnimeTrendingApp.Models;
using AnimeTrendingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AnimeTrendingApp.ViewModels
{
    public enum Filter { All, Season }
    public class TrendingViewModel : BaseViewModel
    {
        private readonly IAnimeService _animeService;
        private bool _isBusy;
        private Filter _filter;
        private string _filterToolbarItemText = "All";
        private bool _isNotConnected;

        public TrendingViewModel(IAnimeService animeService)
        {
            _animeService = animeService;
            GetTrendings();
            FilterCommand = new Command(ApplyFilter);
            IsNotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        ~TrendingViewModel()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsNotConnected = e.NetworkAccess != NetworkAccess.Internet;
            if(!IsNotConnected)
            {
                if(Filter == Filter.All)
                {
                    GetTrendings();
                    return;
                }
                GetSeasonTrendings();
            }
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
        public bool IsNotConnected
        {
            get => _isNotConnected;
            set => SetProperty(ref _isNotConnected, value);
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
