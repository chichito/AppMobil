using AppMobil.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppMobil.ViewModels
{
    public class ProductoViewModel : BaseViewModel
    {
        #region atrivutos
        #endregion
        #region Propiedades
        public EmpresasProductos EmpresasProductos
        {
            get;
            set;
        }

        #endregion
        public ProductoViewModel(EmpresasProductos empresasProductos)
        {
            this.EmpresasProductos = empresasProductos;
        }

    }
}
