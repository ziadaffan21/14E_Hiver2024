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
        private SecureString _securePassword;

        public string Username { get { return _username; } set { if (_username != value.Trim()) { _username = value.Trim(); OnPropertyChanged(); } } }

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
    }
}
