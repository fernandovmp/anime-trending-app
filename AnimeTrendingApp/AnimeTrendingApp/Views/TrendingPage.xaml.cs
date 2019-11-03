using AnimeTrendingApp.Controls;
using AnimeTrendingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeTrendingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrendingPage : ContentPage
    {
        public TrendingPage()
        {
            InitializeComponent();
            BindingContext = (Application.Current as App).Container
                .Resolve(typeof(ViewModels.TrendingViewModel), DryIoc.IfUnresolved.ReturnDefault);
        }

        private async void AnimeCardTapped(object sender, EventArgs e)
        {
            object anime = (sender as AnimeCard).BindingContext;
            await Navigation.PushAsync(new AnimePage
            {
                BindingContext = anime
            });
        }
    }
}
