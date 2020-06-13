using AppMobil.Models;
using AppMobil.Services.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class EmpresasViewModel:BaseViewModel
    {
        #region atributos
        private ObservableCollection<EmpresaItemViewModel> empresas;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Propiedades

        public ObservableCollection<EmpresaItemViewModel> Empresas
        {
            get { return empresas; }
            set { SetValue(ref empresas, value); }
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
        #region Constructores
        public EmpresasViewModel()
        {
            this.loadEmpresas();
        }

        #endregion

        #region Metodos
        public async void loadEmpresas()
        {
            this.IsRefreshing = true;
            string sUrl = "Empresas";
            if (!string.IsNullOrEmpty(App.codigoCategoriaEmpresa))
                sUrl += "?codigoCategoria=" + App.codigoCategoriaEmpresa;
            var response = await StoreWebApiClient.Instance.GetItems<Empresa>(sUrl);
            if (!response.IsSuccess) 
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                return;
            }
            MainViewModel.GetInstance().EmpresasList = (List<Empresa>)response.Result;
            Empresas = new ObservableCollection<EmpresaItemViewModel>(this.ToEmpresaItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<EmpresaItemViewModel> ToEmpresaItemViewModel()
        {
            return MainViewModel.GetInstance().EmpresasList.Select(e => new EmpresaItemViewModel
            {
                Codigo = e.Codigo,
                Nombreempresa = e.Nombreempresa,
                Direccion = e.Direccion,
                Telefono = e.Telefono,
                Celular = e.Celular,
                Logo = MainViewModel.GetInstance().KeysParametros["IpImagenes"] + e.Logo,
                Estado = e.Estado,
                Iniciohorario = e.Iniciohorario,
                Finalhorario = e.Finalhorario,
                Categoriaempresa = e.Categoriaempresa,
                Empleadosempresas = e.Empleadosempresas,
                Subcategorias = e.Subcategorias,
                FullTelefonos = "Cel: " + e.Celular + " Tel: "+ e.Telefono,
            });
        }

        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(loadEmpresas);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Empresas = new ObservableCollection<EmpresaItemViewModel>(ToEmpresaItemViewModel());
            }
            else
            {
                this.Empresas = new ObservableCollection<EmpresaItemViewModel>(ToEmpresaItemViewModel().Where(
                        l => l.Nombreempresa.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
