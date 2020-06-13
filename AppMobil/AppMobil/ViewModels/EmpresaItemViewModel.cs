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
    public class EmpresaItemViewModel : Empresa
    {
        private NavigationService navigationService;

        public EmpresaItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #region Commands
        public ICommand SelectEmpresaCommand
        {
            get
            {
                return new RelayCommand(SelectEmpresa);
            }
        }

        private async void SelectEmpresa()
        {
            MainViewModel.GetInstance().EmpresaCategorias = new EmpresaCategoriasViewModel(this);
            MainViewModel.GetInstance().EmpresaProductos = new EmpresaProductosViewModel(this);
            await navigationService.NavigateOnMaster("EmpresaProductosTabbPage");
            //await App.Navigator.PushAsync(new EmpresaProductosTabbPage());
            //await App.Navigation.PushAsync(new LandTabbedPage());
        }
        #endregion
    }
}
