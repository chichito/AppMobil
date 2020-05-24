using AppMobil.Models;
using AppMobil.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerPedidosPage : ContentPage
    {
        public VerPedidosPage()
        {
            InitializeComponent();
        }
        public void Remove_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.BindingContext as PedidosDetallesVer;
            MainViewModel.GetInstance().VerPedidos.RemoveCommand.Execute(product);
        }
    }
}