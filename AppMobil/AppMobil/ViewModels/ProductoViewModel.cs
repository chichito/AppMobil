using AppMobil.Models;
using AppMobil.Services.Services;
using AppMobil.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
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
        private decimal cantidad;
        private decimal subtotal;
        private decimal iva;
        private decimal recargos;
        private decimal total;
        private ObservableCollection<Galeriaimagenes> galeriaimagenes;

        #endregion
        #region Propiedades
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
        public decimal Cantidad
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
            this.Recargos = Convert.ToDecimal(this.EmpresasProductos.Recargos);
            CargarGaleriaImagenes();
        }

        #endregion
        #region Comandos
        public ICommand ComprasCommand
        {
            get
            {
                return new RelayCommand(Compras);
            }
        }

        public ICommand VerComprasCommand
        {
            get
            {
                return new RelayCommand(VerComprasAsync);
            }
        }

        public ICommand ChangePositionCommand => new Command(ChangePosition);

        #endregion

        #region Metodos
        private async void Compras()
        {
            if (MainViewModel.GetInstance().Pedidosempresas == null)
            {
                MainViewModel.GetInstance().Pedidosempresas = new Pedidosempresas();
                MainViewModel.GetInstance().Pedidosempresas.Codigousuario = App.CurrentUsuario.Codigo;
                MainViewModel.GetInstance().Pedidosempresas.Fechapedido = DateTime.Now;
                MainViewModel.GetInstance().Pedidosempresas.Codigoestado = 1;
            }
            
            Pedido pedido = null;
            foreach (Pedido ped in MainViewModel.GetInstance().Pedidosempresas.Pedidos)
                if (ped.Codigoempresa == this.EmpresasProductos.CodigoEmpresa)
                    pedido = ped;
            if (pedido == null)
            {
                pedido = new Pedido();
                pedido.Codigoempresa = this.EmpresasProductos.CodigoEmpresa;
                pedido.Nombreempresa = this.EmpresasProductos.NombreEmpresa;
                pedido.Logoempresa = this.EmpresasProductos.LogoEmpresa;
                pedido.Codigoestadodespacho = 1;
                pedido.Codigoestadopedido = 1;
                MainViewModel.GetInstance().Pedidosempresas.Pedidos.Add(pedido);
            }

            Pedidosdetalle pedidosdetalle = null; 
            foreach (Pedidosdetalle pedDet in pedido.Pedidosdetalles)
                if (pedDet.Codigoproducto == this.EmpresasProductos.Codigo)
                    pedidosdetalle = pedDet;

            if (pedidosdetalle == null)
                pedidosdetalle = new Pedidosdetalle();
            pedidosdetalle.Codigoproducto = this.EmpresasProductos.Codigo;
            pedidosdetalle.Nombreproducto = this.EmpresasProductos.Nombre;
            pedidosdetalle.Logoproducto = this.EmpresasProductos.Imagen;
            pedidosdetalle.Codigoestado = 1;
            pedidosdetalle.Cantidad = this.Cantidad;
            pedidosdetalle.Iva = this.Iva;
            pedidosdetalle.Recargos = this.Recargos;
            pedidosdetalle.Precio = this.Total;
            pedido.Pedidosdetalles.Add(pedidosdetalle);

            //var response = await StoreWebApiClient.Instance.PostItem<Pedidosempresas>("pedidosempresas", MainViewModel.GetInstance().Pedidosempresas);
            //if (!response.IsSuccess)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
            //    return;
            //}
            await Application.Current.MainPage.DisplayAlert("Mensage", "Agregado Exitosamente", "OK");
            //MainViewModel.GetInstance().Pedidosempresas = null;
        }

        private async void VerComprasAsync()
        {
            MainViewModel.GetInstance().VerPedidos = new VerPedidosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new VerPedidosPage());

        }
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

        private Galeriaimagenes selectedBurger;
        public Galeriaimagenes SelectedBurger
        {
            get { return selectedBurger; }
            set
            {
                selectedBurger = value;
                OnPropertyChanged();
            }
        }

        private int position;
        public int Position
        {
            get
            {
                if (position != galeriaimagenes.IndexOf(selectedBurger))
                    return galeriaimagenes.IndexOf(selectedBurger);

                return position;
            }
            set
            {
                position = value;
                selectedBurger = galeriaimagenes[position];

                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedBurger));
            }
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
