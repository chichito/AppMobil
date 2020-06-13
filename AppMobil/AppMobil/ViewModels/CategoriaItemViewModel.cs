using AppMobil.Models;
using AppMobil.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms.Xaml;

namespace AppMobil.ViewModels
{
    public class CategoriaItemViewModel : Categorias
    {
        private NavigationService navigationService;

        public CategoriaItemViewModel()
        {
            navigationService = new NavigationService();
        }
        public ICommand SelectCategoriaCommand
        {
            get
            {
                return new RelayCommand(SelectCategoria);
            }
        }

        private async void SelectCategoria()
        {
            App.codigoCategoriaEmpresa = this.Codigo;
            MainViewModel.GetInstance().Empresas.loadEmpresas();
            MainViewModel.GetInstance().EmpresasProductos.loadEmpresasProductos();
            await navigationService.BackOnMaster();
        }

    }
}
