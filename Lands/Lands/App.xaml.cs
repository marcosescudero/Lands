using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Lands
{
    using Helpers;
    using Lands.Views; // Nuestra carpeta
    using Models;
    using Services;
    using ViewModels;
    using Xamarin.Forms;

    public partial class App : Application
	{
        #region Properties
        public static NavigationPage Navigator
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
                // Persistencia en SQlite
                var dataService = new DataService();
                var user = dataService.First<UserLocal>(false); 

                // Carga la persistencias (de settings y de Sqlite) a la MainVieMmodel
                var mainViewmodel = MainViewModel.GetInstance();
                mainViewmodel.Token = Settings.Token; // Guarda Token de la memoria (MainViewModel)
                mainViewmodel.TokenType = Settings.TokenType; // Guarda TokenType de la memoria (MainViewModel)
                mainViewmodel.User = user; // Guarda el user (traido del sqLite) en la MainViewModel;

                mainViewmodel.Lands = new LandsViewModel(); // carga lands


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
