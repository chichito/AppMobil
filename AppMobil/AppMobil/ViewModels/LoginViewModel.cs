using AppMobil.Helpers;
using AppMobil.Models;
using AppMobil.Services;
using AppMobil.Services.Services;
using AppMobil.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;


namespace AppMobil.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {   
        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion
        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            //this.apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "fer";
            this.Password = "fer";
        }
        #endregion
        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Password en blanco",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

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

            var usuario = await StoreWebApiClient.Instance.LoginCustomer(this.Email, this.Password);
            if (usuario == default(Usuario))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error!", "Wrong credentials!", "OK");
                this.Password = string.Empty;
                this.Email = string.Empty;
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

            App.CurrentUsuario = usuario;
            await Application.Current.MainPage.DisplayAlert("Welcome!", $"Welcome {usuario.Nombres}", "OK");
            MainViewModel.GetInstance().Empresas = new EmpresasViewModel();
            MainViewModel.GetInstance().EmpresasProductos = new EmpresasProductosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new EmpresasProductosTabbPage());
        }
        #endregion
    }
}
