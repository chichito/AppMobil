using AppMobil.Services;
using AppMobil.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class MenuItemViewModel
    {
        NavigationService navigationService;
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }

        #region Comandos
        public ICommand NavigateComand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }
        #endregion
        #region Metodos
        private async void Navigate()
        {
            if (this.PageName == "LoginPage")
            {
                App.Current.MainPage = new LoginPage();
            }
            if (this.PageName == "CategoriasPage")
            {
                MainViewModel.GetInstance().Categorias = new CategoriasViewModel();
                await navigationService.NavigateOnMaster(PageName);
                //await App.Current.MainPage.Navigation.PushModalAsync(new CategoriasPage());
            }
        }

        #endregion
    }
}
