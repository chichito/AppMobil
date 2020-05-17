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
    public class ProductoViewModel : BaseViewModel
    {
        #region Attributes
        private string mensajeIva;
        private string mensajeRecargos;
        private int cantidad;
        private decimal subtotal;
        private decimal iva;
        private decimal recargos;
        private decimal total;
        private ObservableCollection<Galeriaimagenes> galeriaimagenes;
        private int position;
        private Galeriaimagenes selectedImagen;
        public Galeriaimagenes SelectedImagen
        {
            get { return selectedImagen; }
            set
            {
                selectedImagen = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Propiedades
        public int Position
        {
            get
            {
                if (position != galeriaimagenes.IndexOf(selectedImagen))
                    return galeriaimagenes.IndexOf(selectedImagen);

                return position;
            }
            set
            {
                position = value;
                selectedImagen = galeriaimagenes[position];

                OnPropertyChanged();
                OnPropertyChanged(nameof(selectedImagen));
            }
        }

        public ObservableCollection<Galeriaimagenes> Galeriaimagenes
        {
            get { return this.galeriaimagenes; }
            set { SetValue(ref this.galeriaimagenes, value); }
        }

        public EmpresasProductos EmpresasProductos
        {
            get;
            set;
        }
        public string MensajeIva
        {
            get { return this.mensajeIva; }
            set { SetValue(ref this.mensajeIva, value); }
        }
        public string MensajeRecargos
        {
            get { return this.mensajeRecargos; }
            set { SetValue(ref this.mensajeRecargos, value); }
        }
        public int Cantidad
        {
            get { return this.cantidad; }
            set 
            { 
                SetValue(ref this.cantidad, value);
                this.CalculosTotales();
            }
        }
        public decimal Subtotal
        {
            get { return this.subtotal; }
            set { SetValue(ref this.subtotal, value); }
        }
        public decimal Iva
        {
            get { return this.iva; }
            set { SetValue(ref this.iva, value); }
        }
        public decimal Recargos
        {
            get { return this.recargos; }
            set { SetValue(ref this.recargos, value); }
        }
        public decimal Total
        {
            get { return this.total; }
            set { SetValue(ref this.total, value); }
        }

        #endregion
        #region Constructor
        public  ProductoViewModel(EmpresasProductos empresasProductos)
        {
            this.EmpresasProductos = new EmpresasProductos();
            this.EmpresasProductos.Codigo = empresasProductos.Codigo;
            this.EmpresasProductos.Nombre = empresasProductos.Nombre;
            this.EmpresasProductos.Descripcion = empresasProductos.Descripcion;
            this.EmpresasProductos.Imagen = empresasProductos.Imagen;
            this.EmpresasProductos.Cantidad = empresasProductos.Cantidad;
            this.EmpresasProductos.Precio = empresasProductos.Precio;
            this.EmpresasProductos.Iva = empresasProductos.Iva;
            if (this.EmpresasProductos.Iva == true)
                this.MensajeIva = "* Tiene Iva";
            else
                this.MensajeIva = "* No Tiene Iva";
            this.EmpresasProductos.Recargos = empresasProductos.Recargos;
            if (this.EmpresasProductos.Recargos > 0)
                this.MensajeRecargos = "* Tiene Recargos de: " + this.EmpresasProductos.Recargos.ToString();
            else
                this.MensajeRecargos = "* No Tiene Recargos";
            this.EmpresasProductos.CodigoSubcategoria = empresasProductos.CodigoSubcategoria;
            this.EmpresasProductos.NombreSubCategoria = empresasProductos.NombreSubCategoria;
            this.EmpresasProductos.DescripcionEmpresa = empresasProductos.DescripcionEmpresa;
            this.EmpresasProductos.CodigoEmpresa = empresasProductos.CodigoEmpresa;
            this.EmpresasProductos.NombreEmpresa = empresasProductos.NombreEmpresa;
            this.EmpresasProductos.DireccionEmpresa = empresasProductos.DireccionEmpresa;
            this.EmpresasProductos.TelefonoEmpresa = empresasProductos.TelefonoEmpresa;
            this.EmpresasProductos.CelularEmpresa = empresasProductos.CelularEmpresa;
            this.EmpresasProductos.LogoEmpresa = empresasProductos.LogoEmpresa;
            this.EmpresasProductos.FullTelefonos = empresasProductos.FullTelefonos;
            CargarGaleriaImagenes();
        }

        #endregion
        #region Comandos
        public ICommand ChangePositionCommand => new Command(ChangePosition);

        #endregion

        #region Metodos
        public async void CargarGaleriaImagenes()
        {
            var response = await StoreWebApiClient.Instance.GetItems<Galeriaimagenes>("Galeriaimagenes/"+this.EmpresasProductos.Codigo);
            Galeriaimagenes = new ObservableCollection<Galeriaimagenes>((List<Galeriaimagenes>)response.Result);
        }

        public void CalculosTotales()
        {
            this.Subtotal = Convert.ToDecimal(this.Cantidad) * Convert.ToDecimal(this.EmpresasProductos.Precio);
            if (this.EmpresasProductos.Iva == true)
                this.Iva = Convert.ToDecimal(this.Subtotal) * Convert.ToDecimal(0.12);
            this.Recargos = this.Recargos;
            this.Total = this.Subtotal + this.Iva + this.Recargos;
        }
        private void ChangePosition(object obj)
        {
            string direction = (string)obj;

            if (direction == "L")
            {
                if (position == 0)
                {
                    Position = galeriaimagenes.Count - 1;
                    return;
                }

                Position -= 1;
            }
            else if (direction == "R")
            {
                if (position == galeriaimagenes.Count - 1)
                {
                    Position = 0;
                    return;
                }

                Position += 1;
            }
        }
        #endregion
    }
}
