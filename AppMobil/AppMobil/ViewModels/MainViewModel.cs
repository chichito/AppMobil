using AppMobil.Models;
using AppMobil.Services.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.SymbolStore;
using System.Text;
using Xamarin.Forms;

namespace AppMobil.ViewModels
{
    public class MainViewModel
    {
        #region propiedades
        public Dictionary<string, string> KeysParametros
        {
            get;
            set;
        }
            //= new List<KeyValuePair<string, int>>();
        public List<Parametros> ParametrosList
        {
            get;
            set;
        }
        public Pedidosempresas Pedidosempresas
        {
            get;
            set;
        }
        public List<Categorias> CategoriasList
        {
            get;
            set;
        }
        public List<Empresa> EmpresasList
        {
            get;
            set;
        }
        public List<Subcategoria> SubcategoriasList
        {
            get;
            set;
        }
        public List<EmpresasProductos> EmpresasProdcutosList
        {
            get;
            set;
        }
        public ObservableCollection<MenuItemViewModel> Menus 
        { 
            get; 
            set; 
        }

        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }
        public EmpresaProductosViewModel EmpresaProductos
        {
            get;
            set;
        }
        public EmpresasProductosViewModel EmpresasProductos
        {
            get;
            set;
        }
        public EmpresasViewModel Empresas
        { 
            get; 
            set; 
        }
        public EmpresaCategoriasViewModel EmpresaCategorias
        {
            get;
            set;
        }
        public ProductosViewModel Productos
        {
            get;
            set;
        }
        public ProductoViewModel Producto
        {
            get;
            set;
        }

        public VerPedidosViewModel VerPedidos
        {
            get;
            set;
        }
        public CategoriasViewModel Categorias
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            loadParametros();
            this.loadMenus();
        }

        #endregion

        #region Metodos
        private void loadMenus()
        {
            Menus = new ObservableCollection<MenuItemViewModel>();

            Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_settings.png",
                PageName = "NewOrderPage",
                Title = "Perfil",
            });

            Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_category.png",
                PageName = "CategoriasPage",
                Title = "Categorias",
            });

            Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app.png",
                PageName = "LoginPage",
                Title = "Salir",
            });

        }
        private async void loadParametros()
        {
            var connection = await StoreWebApiClient.Instance.CheckConnection();

            //if (!connection.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        connection.Message,
            //        Languages.Accept);
            //    return;
            //}

            var response = await StoreWebApiClient.Instance.GetItems<Parametros>("Parametros");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                return;
            }
            MainViewModel.GetInstance().ParametrosList = (List<Parametros>)response.Result;
            KeysParametros = new Dictionary<string, string>();
            foreach (Parametros parametros in MainViewModel.GetInstance().ParametrosList)
            {
                KeysParametros.Add(parametros.Nombre, parametros.Valor);
            }
        }
        #endregion
        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();
            return instance;
        }
        #endregion
    }
}
