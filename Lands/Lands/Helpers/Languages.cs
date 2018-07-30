namespace Lands.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }
        public static string TokenValidation
        {
            get { return Resource.TokenValidation; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }
        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }

        public static string ForgotPassword
        {
            get { return Resource.ForgotPassword; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }


    }

}
