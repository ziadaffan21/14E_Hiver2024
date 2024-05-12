using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel.ObservableClass;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
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
            SaveCommand = new DelegateCommand(LogIn, CanLogIn);
        }

        private bool CanLogIn()
        {
            return true;

        }

        private async void LogIn()
        {
            //Abonne User = await _abonneService.GetAbonneConnexion(ObservableUsersignInLogIn.Username,ObservableUsersignInLogIn.Password);
            //if (User is not null)
            //    ((MainWindow)Application.Current.MainWindow).ConnecterWindow(User);
            //else
            //    MessageBox.Show(Resource.errorConnection, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
