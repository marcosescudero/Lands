using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Lands
{
    using Helpers;
    using Lands.Views; // Nuestra carpeta
    using Xamarin.Forms;
    using ViewModels;

    public partial class App : Application
	{
        #region Properties
        public static NavigationPage Navigation
        {
            get;
            internal set;
        }
        #endregion

        #region Constructors
        public App ()
		{
			InitializeComponent();

            // Verifico si hay token guardado en persistencia
            if (string.IsNullOrEmpty(Settings.Token))
            {
                this.MainPage = new NavigationPage(new LoginPage());
            } else
            {
                var mainViewmodel = MainViewModel.GetInstance();
                mainViewmodel.Token = Settings.Token; // Guarda Token de la memoria (MainViewModel)
                mainViewmodel.TokenType = Settings.TokenType; // Guarda TokenType de la memoria (MainViewModel)
                this.MainPage = new MasterPage();
            }

            //MainPage = new MainPage();
            //this.MainPage = new NavigationPage(new LoginPage());
            //this.MainPage = new MasterPage();
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
