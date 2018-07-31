

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
            if (this.PageName == "LoginPage")
            {
                Settings.Token = string.Empty; // Elimino Token de persistencia
                Settings.TokenType = string.Empty; // Elimino TokenType de persistencia
                var mainViewmodel = MainViewModel.GetInstance(); 
                mainViewmodel.Token = string.Empty; // Elimino Token de la memoria (MainViewModel)
                mainViewmodel.Token = string.Empty; // Elimino TokenType de la memoria (MainViewModel)

                Application.Current.MainPage = new LoginPage();
            }
        }
        #endregion

    }
}
