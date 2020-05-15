using AppMobil.Models;
using AppMobil.Services;
using AppMobil.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobil
{
    public partial class App : Application
    {
        public static Usuario CurrentUsuario;
        public App()
        {
            InitializeComponent();

            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
