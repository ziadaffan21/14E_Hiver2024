using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineQuebec.Windows.ViewModel.ObservableClass
{
    public class ObservableUsersignInLogIn:PropertyNotifier
    {
//        public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.Register(
//"SecurePassword", typeof(SecureString), typeof(MainWindow), new PropertyMetadata(default(SecureString)));

        private string _username;
       // private string _password;
        private SecureString _securePassword;

        public string Username { get { return _username; } set { if (_username != value.Trim()) { _username = value.Trim(); OnPropertyChanged(); } } }
        //public string Password { get { return _password; } set { if (_password != value) { _password = value; OnPropertyChanged(); } } }

        public SecureString SecurePassword
        {
            get => _securePassword;
            set { _securePassword = value; OnPropertyChanged(); }
        }

        public ObservableUsersignInLogIn()
        {
            SecurePassword = new();
        }

        internal bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username) && SecurePassword.Length>0;
        }

        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException(nameof(securePassword));

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
