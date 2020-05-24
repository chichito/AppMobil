using AppMobil.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Reflection;
using AppMobil.Views;
using AppMobil.Services.Services;

namespace AppMobil.ViewModels
{
    public class VerPedidosViewModel : BaseViewModel
    {
        #region Atributos
        private decimal totalPagar;
        private bool isRefreshing;
        private string filter;
        ObservableCollection<PedidosDetallesVer> verPedidosempresas;


        #endregion
        #region Propiedades
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public decimal TotalPagar
        {
            get { return this.totalPagar; }
            set { SetValue(ref this.totalPagar, value); }
        }
        public ObservableCollection<PedidosDetallesVer> VerPedidosempresas
        {
            get { return verPedidosempresas; }
            set { SetValue(ref verPedidosempresas, value); }
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
        public VerPedidosViewModel()
        {
            loadVerPedidos();
        }

        #endregion
        #region Comandos
        public ICommand ComprarCommand
        {
            get
            {
                return new RelayCommand(RealizarCompraPedidos);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(loadVerPedidos);
            }
        }
        public Command<PedidosDetallesVer> RemoveCommand
        {
            get
            {
                return new Command<PedidosDetallesVer>((Product) =>
                {
                    foreach (Pedido ped in MainViewModel.GetInstance().Pedidosempresas.Pedidos)
                    {
                        Boolean ban = false;
                        foreach (Pedidosdetalle pedDet in ped.Pedidosdetalles)
                        {
                            if (pedDet.Codigoproducto == Product.Codigoproducto)
                            {
                                ped.Pedidosdetalles.Remove(pedDet);
                                ban = true;
                                break;
                            }
                        }
                        if (ped.Pedidosdetalles.Count == 0)
                        {
                            MainViewModel.GetInstance().Pedidosempresas.Pedidos.Remove(ped);
                        }
                        if (ban)
                            break;
                    }
                    VerPedidosempresas.Remove(Product);
                });
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        #endregion
        #region Metodos
        private async void RealizarCompraPedidos()
        {
            var response = await StoreWebApiClient.Instance.PostItem<Pedidosempresas>("pedidosempresas", MainViewModel.GetInstance().Pedidosempresas);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Mensage", "Comprar Realizada Exitosamente", "OK");
            MainViewModel.GetInstance().Pedidosempresas = null;
            
        }

        private void loadVerPedidos()
        {
            this.IsRefreshing = true;
            this.VerPedidosempresas = toPedidosVerDetalles();
            this.IsRefreshing = false;
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.VerPedidosempresas = new ObservableCollection<PedidosDetallesVer>(toPedidosVerDetalles());
            }
            else
            {
                this.VerPedidosempresas = new ObservableCollection<PedidosDetallesVer>(toPedidosVerDetalles().Where(
                        l => l.Nombreproducto.ToLower().Contains(this.Filter.ToLower())));
            }
        }

        private ObservableCollection<PedidosDetallesVer> toPedidosVerDetalles()
        {
            this.TotalPagar = 0;
            ObservableCollection<PedidosDetallesVer> pedidosDetallesVer = new ObservableCollection<PedidosDetallesVer>();
            foreach (Pedido pedido in MainViewModel.GetInstance().Pedidosempresas.Pedidos)
            {
                foreach (Pedidosdetalle pedidosdetalle in pedido.Pedidosdetalles)
                {
                    PedidosDetallesVer pVer = new PedidosDetallesVer();
                    pVer.Fechapedido = MainViewModel.GetInstance().Pedidosempresas.Fechapedido;
                    pVer.Nombreempresa = pedido.Nombreempresa;
                    pVer.LogoNombreempresa = pedido.Logoempresa;
                    pVer.Codigoproducto = pedidosdetalle.Codigoproducto;
                    pVer.Nombreproducto = pedidosdetalle.Nombreproducto;
                    pVer.Logoproducto = pedidosdetalle.Logoproducto;
                    pVer.Codigoestado = pedidosdetalle.Codigoestado;
                    pVer.Cantidad = pedidosdetalle.Cantidad;
                    pVer.Iva = pedidosdetalle.Iva;
                    pVer.Recargos = pedidosdetalle.Recargos;
                    pVer.Precio = pedidosdetalle.Precio;
                    this.TotalPagar += pVer.Precio;
                    pedidosDetallesVer.Add(pVer);
                }
            }
            return pedidosDetallesVer;
        }

        #endregion
    }

    public class PedidosDetallesVer
    {
        public DateTime Fechapedido { get; set; }
        public string Nombreempresa { get; set; }
        public string LogoNombreempresa { get; set; }
        public string Codigoproducto { get; set; }
        public string Nombreproducto { get; set; }
        public string Logoproducto { get; set; }
        public int Codigoestado { get; set; }
        public decimal Cantidad { get; set; }
        public Decimal Iva { get; set; }
        public Decimal Recargos { get; set; }
        public Decimal Precio { get; set; }

    }
}
