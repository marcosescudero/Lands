
namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using System;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;
    public class LoginViewModel : BaseViewModel
    {
        #region Services
        public ApiService apiService;
        public DataService dataService; // SQLite

        #endregion

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
            get { return this.password;}
            set { SetValue(ref this.password, value);}
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value);}
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsEnabled {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataService(); // SQLite

            this.IsRemembered = true;
            this.IsEnabled = true;


            //this.Email = "marcosescudero@gmail.com";
            //this.Password = "1234";
            //this.Email = "juan@gmail.com"; Esto era para evitar tener que escribir nuestro email en cada prueba
            //this.Password = "123456";

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
                    Languages.Accept
                    );
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept
                    );
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            /*
            if (this.Email != "marcosescudero@gmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect",
                    "Accept"
                    );
                this.Password = string.Empty;
                return;
            }
            */

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept
                    );
                this.Password = string.Empty;
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await this.apiService.GetToken(
                apiSecurity,
                this.Email, 
                this.Password
                );
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.TokenValidation,
                    Languages.Accept
                    );
                this.Password = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LoginError,
                    Languages.Accept
                    );
                this.Password = string.Empty;
                return;
            }

            var user = await this.apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                token.TokenType,
                token.AccessToken,
                this.Email
                );

            var userLocal = Converter.ToUserLocal(user);
            userLocal.Password = this.Password;

            var mainViewmodel = MainViewModel.GetInstance();
            mainViewmodel.Token = token;    // Lo guardamos en memoria (en la mainViewModel)
            mainViewmodel.User = userLocal;

            if (this.IsRemembered)
            {
                Settings.IsRemembered = "true"; // Lo guardamos tambien en la Settings (Persistencia)
            } else
            {
                Settings.IsRemembered = "false"; // Lo guardamos tambien en la Settings (Persistencia)
            }

            this.dataService.DeleteAllAndInsert(userLocal); //sqlite
            this.dataService.DeleteAllAndInsert(token); //sqlite

            mainViewmodel.Lands = new LandsViewModel();

            //await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
            Application.Current.MainPage = new MasterPage();

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        
        }

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        #endregion
    }
}
