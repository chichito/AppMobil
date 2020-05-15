using AppMobil.Models;
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
        #region Commands
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        private async void SelectLand()
        {
            MainViewModel.GetInstance().EmpresaCategorias = new EmpresaCategoriasViewModel(this);
            MainViewModel.GetInstance().EmpresaProductos = new EmpresaProductosViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EmpresaProductosTabbPage());
            //await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage());
        }
        #endregion
    }
}
