using AppMobil.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobil.Services
{
    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginPage":
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
                case "MasterPage":
                    Application.Current.MainPage = new MasterPage();
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        public async Task NavigateOnMaster(string pageName)
        {
            App.Master.IsPresented = false;
            switch (pageName)
            {
                case "CategoriasPage":
                    await App.Navigator.PushAsync(new CategoriasPage());
                    break;
                case "ProductosPage":
                    await App.Navigator.PushAsync(new ProductosPage());
                    break;
                case "ProductoPage":
                    await App.Navigator.PushAsync(new ProductoPage());
                    break;
                case "EmpresaProductosTabbPage":
                    await App.Navigator.PushAsync(new EmpresaProductosTabbPage());
                    break;
                case "VerPedidosPage":
                    await App.Navigator.PushAsync(new VerPedidosPage());
                    break;//VerPedidosPage
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        public async Task NavigateOnLogin(string pageName)
        {
            switch (pageName)
            {
                default:
                    Console.WriteLine("Default case");
                    break;

            }
        }
        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}