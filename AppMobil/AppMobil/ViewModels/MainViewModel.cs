using AppMobil.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;

namespace AppMobil.ViewModels
{
    public class MainViewModel
    {
        #region propiedades
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
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
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
