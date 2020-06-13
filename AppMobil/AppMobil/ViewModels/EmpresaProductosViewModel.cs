using AppMobil.Models;
using AppMobil.Services.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class EmpresaProductosViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<ProductosItemViewModel> empresasproductos;
        private bool isRefreshing;
        private string filter;
        #endregion
        #region Propiedades
        public Empresa Empresa
        {
            get;
            set;
        }
        public ObservableCollection<ProductosItemViewModel> EmpresasProductos
        {
            get { return empresasproductos; }
            set { SetValue(ref empresasproductos, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                Search();
            }
        }
        #endregion
        #region Constructor
        public EmpresaProductosViewModel(Empresa empresa)
        {
            this.Empresa = empresa;
            loadEmpresasProductos();
        }

        #endregion
        #region Metodos
        private async void loadEmpresasProductos()
        {
            this.IsRefreshing = true;
            string sUrl = "productosempresas";
            if (!string.IsNullOrEmpty(this.Filter))
                sUrl += "/" + this.Filter.ToUpper();

            if (!string.IsNullOrEmpty(App.codigoCategoriaEmpresa))
                sUrl += "?codigocategoria=" + App.codigoCategoriaEmpresa + "&codigoempresa=" + this.Empresa.Codigo;
            else
                sUrl += "?codigoempresa=" + this.Empresa.Codigo;

            //pit?codigoempresa=9FDAC07D-78DD-4B82-BD4B-16224EF77AF5&codigosubcategoria=cf47f4f1-5587-415d-86e1-2eb49130f6b0
            var response = await StoreWebApiClient.Instance.GetItems<EmpresasProductos>(sUrl);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                return;
            }
            MainViewModel.GetInstance().EmpresasProdcutosList = (List<EmpresasProductos>)response.Result;
            this.EmpresasProductos = new ObservableCollection<ProductosItemViewModel>(ToEmpresaProductosItemViewModel());
            ////MainViewModel.GetInstance().EmpresasList = (List<EmpresasProductos>)response.Result;
            //var leempre = (List<EmpresasProductos>)response.Result;
            //EmpresasProductos = new ObservableCollection<EmpresasProductos>(leempre);
            this.IsRefreshing = false;
        }
        #endregion

        #region Comandos
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        #endregion
        #region Metodos
        private IEnumerable<ProductosItemViewModel> ToEmpresaProductosItemViewModel()
        {
            return MainViewModel.GetInstance().EmpresasProdcutosList.Select(e => new ProductosItemViewModel
            {
                Codigo = e.Codigo,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Imagen = MainViewModel.GetInstance().KeysParametros["IpImagenes"] + e.Imagen,
                Cantidad = e.Cantidad,
                Precio = e.Precio,
                Iva = e.Iva,
                Recargos = e.Recargos,
                CodigoSubcategoria = e.CodigoSubcategoria,
                NombreSubCategoria = e.NombreSubCategoria,
                DescripcionEmpresa = e.DescripcionEmpresa,
                CodigoEmpresa = e.CodigoEmpresa,
                NombreEmpresa = e.NombreEmpresa,
                DireccionEmpresa = e.DireccionEmpresa,
                TelefonoEmpresa = e.TelefonoEmpresa,
                CelularEmpresa = e.CelularEmpresa,
                LogoEmpresa = e.LogoEmpresa,
                FullTelefonos = "Cel: " + e.CelularEmpresa + " Telf: " + e.TelefonoEmpresa,
            });
        }

        private void Search()
        {
            loadEmpresasProductos();
        }

        #endregion

    }
}
