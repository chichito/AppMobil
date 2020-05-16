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
    public class ProductosItemViewModel : EmpresasProductos
    {
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
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoPage());
        }
        #endregion

    }
}
