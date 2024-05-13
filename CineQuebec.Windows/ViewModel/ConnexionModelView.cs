using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel.ObservableClass;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class ConnexionModelView:PropertyNotifier
    {
        private ObservableUsersignInLogIn _observableUsersSignInLogIn;
        private IAbonneService _abonneService;
        public event Action<string> ErrorOccurred;

        public ICommand SaveCommand { get; init; }


        public ObservableUsersignInLogIn ObservableUsersignInLogIn
        {
            get { return _observableUsersSignInLogIn; }
            set { _observableUsersSignInLogIn = value; OnPropertyChanged(); }
        }

        public ConnexionModelView(IAbonneService abonneService)
        {
                _abonneService = abonneService;
            ObservableUsersignInLogIn = new();
            ObservableUsersignInLogIn.PropertyChanged += ReEvaluateButtonState;
            SaveCommand = new DelegateCommand(LogIn, CanLogIn);
        }

        private void ReEvaluateButtonState(object sender, PropertyChangedEventArgs e)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged(); 
        }

        private bool CanLogIn()
        {
            return ObservableUsersignInLogIn.IsValidConnexion();

        }

        private async void LogIn()
        {
            try
            {
                string password = Utils.ConvertToUnsecureString(ObservableUsersignInLogIn.SecurePassword);
                ValiderFomulaire(ObservableUsersignInLogIn.Username, password);
                Abonne User = await _abonneService.GetAbonneConnexion(ObservableUsersignInLogIn.Username, password);
                if (User is not null)
                    ((MainWindow)Application.Current.MainWindow).ConnecterWindow(User);
                else
                    ErrorOccurred.Invoke(Resource.errorConnection);

            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
            
        }

        private void ValiderFomulaire(string username, string password)
        {

            if (string.IsNullOrWhiteSpace(username))
                throw new UsernameLengthException("Le champs username ne peut pas etre vide.");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Le champs mot de passe ne peut pas etre vide.");
        }
    }
}
