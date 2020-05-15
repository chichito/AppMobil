using AppMobil.Models;
using AppMobil.Services.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class EmpresasProductosViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<EmpresasProductos> empresasproductos;
        private bool isRefreshing;
        private string filter;
        #endregion
        #region Propiedades
        public ObservableCollection<EmpresasProductos> EmpresasProductos
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
        private async void loadEmpresasProductos()
        {
            this.IsRefreshing = true;
            string sUrl = "productosempresas";
            if (!string.IsNullOrEmpty(this.Filter))
                sUrl +=  "/" +this.Filter.ToUpper();
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

        #region Comandos
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        private void Search()
        {
            loadEmpresasProductos();
        }

        #endregion
    }
}
