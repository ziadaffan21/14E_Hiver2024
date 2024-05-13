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

namespace CineQuebec.Windows.ViewModel
{
    public class AjoutUserViewModel:PropertyNotifier
    {
        private ObservableUsersignInLogIn _observableUsersSignInLogIn;
        private IAbonneService _abonneService;
        public ICommand SaveCommand { get; init; }


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
                Abonne _abonne = new Abonne(ObservableUsersignInLogIn.Username, DateTime.Now);
                _abonne.Salt = PasswodHasher.CreateSalt();
                _abonne.Password = PasswodHasher.HashPassword(Utils.ConvertToUnsecureString(ObservableUsersignInLogIn.SecurePassword), _abonne.Salt);
                bool result = await _abonneService.Add(_abonne);
                if (result)
                {
                    MessageBox.Show(Resource.ajoutUser, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                    // DialogResultConverter d=new() = true;
                }
                else
                {
                    MessageBox.Show(Resource.errorAjoutUser, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
                    //DialogResult = false;
                }
            }
            catch (ExistingAbonneException ex)
            {
                MessageBox.Show(ex.Message, Resource.existingAbonneTitre, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MongoDataConnectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(Resource.erreurGenerique, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ReEvaluateButtonState(object sender, PropertyChangedEventArgs e)
        {

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
