

namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Lands.Helpers;

    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; } 
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }
        private void Navigate()
        {
            App.Master.IsPresented = false; // Esto cierra automaticamente el menú.

            if (this.PageName == "LoginPage")
            {
                Settings.IsRemembered = "false"; // Elimino Token de persistencia
                var mainViewmodel = MainViewModel.GetInstance();
                mainViewmodel.Token = null; // Elimino Token de la memoria (MainViewModel)
                mainViewmodel.User = null; // Elimino Usuario de la memoria (MainViewModel)

                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (this.PageName == "MyProfilePage")
            {
                MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                App.Navigator.PushAsync(new MyProfilePage());
            }
        }
        #endregion

    }
}
