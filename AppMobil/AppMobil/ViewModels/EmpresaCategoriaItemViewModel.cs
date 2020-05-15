using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using AppMobil.Models;
using AppMobil.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class EmpresaCategoriaItemViewModel: Subcategoria
    {
        #region Commands
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }
        #endregion
        private async void SelectLand()
        {
            //MainViewModel.GetInstance().EmpresaCategorias = new EmpresaCategoriasViewModel(this);
            MainViewModel.GetInstance().Productos = new ProductosViewModel(this,MainViewModel.GetInstance().EmpresaCategorias.Empresa);
            await Application.Current.MainPage.Navigation.PushAsync(new ProductosPage());
            //await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage());
        }

    }
}
