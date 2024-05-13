using CineQuebec.Windows.DAL.Data;
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

        internal bool IsValidConnexion()
        {
            return !string.IsNullOrWhiteSpace(Username) && SecurePassword.Length>0;
        }

        internal bool IsValidInscription()
        {
            return !string.IsNullOrWhiteSpace(Username) && Username.Length>Abonne.NB_MIN_CARACTERES_USERNAME &&
                Username.Length<Abonne.NB_MAX_CARACTERES_PASSWORD && SecurePassword.Length > Abonne.NB_MIN_CARACTERES_PASSWORD
                && SecurePassword.Length<Abonne.NB_MAX_CARACTERES_PASSWORD;
        }
    }
}
