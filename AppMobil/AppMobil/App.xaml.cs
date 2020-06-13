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

        private NavigationService navigationService;
        public static string codigoCategoriaEmpresa;
        public static Usuario CurrentUsuario;

        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        public App()
        {
            navigationService = new NavigationService();
            InitializeComponent();
            navigationService.SetMainPage("LoginPage");
            //this.MainPage = new NavigationPage(new LoginPage());
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
