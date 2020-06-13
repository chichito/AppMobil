using AppMobil.Models;
using AppMobil.Services;
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
    public class CategoriasViewModel :  BaseViewModel
    {
        #region atributos
        private ObservableCollection<CategoriaItemViewModel> categorias;
        private bool isRefreshing;
        private string filter;
        private NavigationService navigationService;
        #endregion

        #region Propiedades

        public ObservableCollection<CategoriaItemViewModel> Categorias
        {
            get { return categorias; }
            set { SetValue(ref categorias, value); }
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

        public CategoriasViewModel()
        {
            navigationService = new NavigationService(); 
            loadCategorias();
        }

        #region Metodos
        private async void loadCategorias()
        {
            this.IsRefreshing = true;
            var response = await StoreWebApiClient.Instance.GetItems<Categorias>("Categorias");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                return;
            }
            MainViewModel.GetInstance().CategoriasList = (List<Categorias>)response.Result;
            Categorias = new ObservableCollection<CategoriaItemViewModel>(this.ToCategoriaItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<CategoriaItemViewModel> ToCategoriaItemViewModel()
        {
            return MainViewModel.GetInstance().CategoriasList.Select(e => new CategoriaItemViewModel
            {
                Codigo = e.Codigo,
                Nombre = e.Nombre,
                Imagen = MainViewModel.GetInstance().KeysParametros["IpImagenes"] + e.Imagen,
                Categoriaempresa = e.Categoriaempresa,
            });
        }

        #endregion
        #region Comandos
        public ICommand TodosCommand
        {
            get
            {
                return new RelayCommand(toLoadEmpresas);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(loadCategorias);
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
                this.Categorias = new ObservableCollection<CategoriaItemViewModel>(ToCategoriaItemViewModel());
            }
            else
            {
                this.Categorias = new ObservableCollection<CategoriaItemViewModel>(ToCategoriaItemViewModel().Where(
                        l => l.Nombre.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        private async void toLoadEmpresas()
        {
            App.codigoCategoriaEmpresa = null;
            MainViewModel.GetInstance().Empresas.loadEmpresas();
            MainViewModel.GetInstance().EmpresasProductos.loadEmpresasProductos();
            await navigationService.BackOnMaster();
        }

        #endregion

    }
}
