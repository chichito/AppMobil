﻿using AppMobil.Models;
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
    public class EmpresasProductosViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<ProductosItemViewModel> empresasproductos;
        private bool isRefreshing;
        private string filter;
        #endregion
        #region Propiedades
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
                this.Search();
            }
        }

        #endregion
        #region Constructor
        public EmpresasProductosViewModel()
        {
            loadEmpresasProductos();
        }

        #endregion

        #region Metodos
        public async void loadEmpresasProductos()
        {
            this.IsRefreshing = true;
            string sUrl = "productosempresas";
            if (!string.IsNullOrEmpty(this.Filter))
                sUrl +=  "/" +this.Filter.ToUpper();

            if (!string.IsNullOrEmpty(App.codigoCategoriaEmpresa))
                sUrl += "?codigocategoria=" + App.codigoCategoriaEmpresa;

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
                FullTelefonos = "Cel: "+e.CelularEmpresa + " Telf: " + e.TelefonoEmpresa,
            });
        }
        private void Search()
        {
            loadEmpresasProductos();
        }

        #endregion
    }
}
