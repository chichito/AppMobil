using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using AppMobil.Models;
using AppMobil.Services;
using AppMobil.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class EmpresaCategoriaItemViewModel: Subcategoria
    {
        private NavigationService navigationService;

        public EmpresaCategoriaItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #region Commands
        public ICommand SelectCategoriaCommand
        {
            get
            {
                return new RelayCommand(SelectCategoria);
            }
        }
        #endregion
        private async void SelectCategoria()
        {
            //MainViewModel.GetInstance().EmpresaCategorias = new EmpresaCategoriasViewModel(this);
            MainViewModel.GetInstance().Productos = new ProductosViewModel(this,MainViewModel.GetInstance().EmpresaCategorias.Empresa);
            await navigationService.NavigateOnMaster("ProductosPage");
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductosPage());
            //await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage());
        }

    }
}
