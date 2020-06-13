using AppMobil.Models;
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
    public class ProductosItemViewModel : EmpresasProductos
    {
        private NavigationService navigationService;

        public ProductosItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #region Commands
        public ICommand SelectionCommand
        {
            get
            {
                return new RelayCommand(SelectProductos);
            }
        }

        private async void SelectProductos()
        { //ghg
            //MainViewModel.GetInstance().EmpresaCategorias = new EmpresaCategoriasViewModel(this);
            //MainViewModel.GetInstance().EmpresaProductos = new EmpresaProductosViewModel(this);
            MainViewModel.GetInstance().Producto = new ProductoViewModel(this);
            await navigationService.NavigateOnMaster("ProductoPage");
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductoPage());
        }
        #endregion

    }
}
