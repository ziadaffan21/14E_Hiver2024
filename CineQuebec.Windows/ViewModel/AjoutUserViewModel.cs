using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel.ObservableClass;
using DnsClient;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;

namespace CineQuebec.Windows.ViewModel
{
    public class AjoutUserViewModel:PropertyNotifier
    {
        private ObservableUsersignInLogIn _observableUsersSignInLogIn;
        private IAbonneService _abonneService;
        public ICommand SaveCommand { get; init; }
        public event Action<string> ErrorOccurred;
        public event Action<bool> ReussiteOuEchec;



        public ObservableUsersignInLogIn ObservableUsersignInLogIn
        {
            get { return _observableUsersSignInLogIn; }
            set { _observableUsersSignInLogIn = value; OnPropertyChanged(); }
        }

        public AjoutUserViewModel(IAbonneService abonneService)
        {
            _abonneService= abonneService;
            ObservableUsersignInLogIn = new();
            ObservableUsersignInLogIn.PropertyChanged += ReEvaluateButtonState;
            SaveCommand = new DelegateCommand(SignUp, CanSignUp);
        }

        private bool CanSignUp()
        {
            return ObservableUsersignInLogIn.IsValidInscription();
        }

        private async void SignUp()
        {
            try
            {
                string password = Utils.ConvertToUnsecureString(ObservableUsersignInLogIn.SecurePassword);
                ValidationFormulaire(ObservableUsersignInLogIn.Username, password);
                Abonne _abonne = new Abonne(ObservableUsersignInLogIn.Username, DateTime.Now);
                _abonne.Salt = PasswodHasher.CreateSalt();
                _abonne.Password = PasswodHasher.HashPassword(password, _abonne.Salt);


                ReussiteOuEchec.Invoke(await _abonneService.Add(_abonne));

            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }

        }

        private void ValidationFormulaire(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < Abonne.NB_MIN_CARACTERES_USERNAME || username.Length > Abonne.NB_MAX_CARACTERES_USERNAME)
                throw new UsernameLengthException($"Le username doit etre entre {Abonne.NB_MIN_CARACTERES_USERNAME} et {Abonne.NB_MAX_CARACTERES_USERNAME} caractères");


            if (string.IsNullOrEmpty(password) || password.Length < Abonne.NB_MIN_CARACTERES_PASSWORD || password.Length > Abonne.NB_MAX_CARACTERES_PASSWORD)
                throw new Exception($"Le mot de passe doit contenir entre {Abonne.NB_MIN_CARACTERES_PASSWORD} et {Abonne.NB_MAX_CARACTERES_PASSWORD} caractères.");
        }

        private void ReEvaluateButtonState(object sender, PropertyChangedEventArgs e)
        {

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
