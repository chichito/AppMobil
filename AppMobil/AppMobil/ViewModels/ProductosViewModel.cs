using AppMobil.Models;
using AppMobil.Services.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class ProductosViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<EmpresasProductos> empresasproductos;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Propiedades
        public Empresa Empresa
        {
            get;
            set;
        }
        public Subcategoria Subcategoria
        {
            get;
            set;
        }
        public ObservableCollection<EmpresasProductos> EmpresasProductos
        {
            get { return empresasproductos; }
            set { SetValue(ref empresasproductos, value); }
        }

        #endregion
        #region Constructor
        public ProductosViewModel(Subcategoria subcategoria, Empresa empresa)
        {
            this.Subcategoria = subcategoria;
            this.Empresa = empresa;
            loadEmpresasProductos();
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
        #region Metodos
        private async void loadEmpresasProductos()
        {
            this.IsRefreshing = true;
            string sUrl = "productosempresas";
            if (string.IsNullOrEmpty(this.Filter))
                sUrl += "?codigoempresa=" + this.Empresa.Codigo + "&codigosubcategoria=" + this.Subcategoria.Codigo;
            else
                sUrl += "/" + this.Filter.ToUpper() + "?codigoempresa=" + this.Empresa.Codigo + "&codigosubcategoria=" + this.Subcategoria.Codigo;
            //pit?codigoempresa=9FDAC07D-78DD-4B82-BD4B-16224EF77AF5&codigosubcategoria=cf47f4f1-5587-415d-86e1-2eb49130f6b0
            var response = await StoreWebApiClient.Instance.GetItems<EmpresasProductos>(sUrl);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                return;
            }
            //MainViewModel.GetInstance().EmpresasList = (List<EmpresasProductos>)response.Result;
            var leempre = (List<EmpresasProductos>)response.Result;
            EmpresasProductos = new ObservableCollection<EmpresasProductos>(leempre);
            this.IsRefreshing = false;
        }
        #endregion
        private void Search()
        {
            loadEmpresasProductos();
        }
    }
}
