using AnimeTrendingApp.Services;
using AnimeTrendingApp.ViewModels;
using AnimeTrendingApp.Views;
using DryIoc;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeTrendingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Container = new Container();
            Container.RegisterInstance(new HttpClient());
            Container.Register<IAnimeService, AnimeService>(Reuse.Singleton);
            Container.Register<TrendingViewModel>(Reuse.Transient);

            MainPage = new NavigationPage(new TrendingPage());
        }

        public IContainer Container { get; private set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
