using AppMobil.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AppMobil.ViewModels
{
    public class EmpresaCategoriasViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<EmpresaCategoriaItemViewModel> subcategorias;
        #endregion
        #region Propiedades
        public Empresa Empresa
        {
            get;
            set;
        }
        public ObservableCollection<EmpresaCategoriaItemViewModel> Subcategorias
        {
            get { return this.subcategorias; }
            set { this.SetValue(ref this.subcategorias, value); }
        }
        #endregion
        #region Constructor
        public EmpresaCategoriasViewModel(Empresa empresa)
        {
            this.Empresa = empresa;
            MainViewModel.GetInstance().SubcategoriasList = this.Empresa.Subcategorias;
            this.Subcategorias = new ObservableCollection<EmpresaCategoriaItemViewModel>(ToEmpresaSubcategoraItemViewModel());
        }

        #endregion
        #region Metodos
        private IEnumerable<EmpresaCategoriaItemViewModel> ToEmpresaSubcategoraItemViewModel()
        {
            return MainViewModel.GetInstance().SubcategoriasList.Select(e => new EmpresaCategoriaItemViewModel
            {
                Codigo = e.Codigo,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Imagen = MainViewModel.GetInstance().KeysParametros["IpImagenes"] + e.Imagen,
                CodigoEmpresa = e.CodigoEmpresa,
            });
        }

        #endregion
    }
}
