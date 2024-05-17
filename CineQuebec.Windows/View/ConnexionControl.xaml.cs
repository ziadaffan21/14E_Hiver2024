using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ConnexionControl.xaml
    /// </summary>
    public partial class ConnexionControl : UserControl
    {
        private readonly ConnexionModelView _viewModel;

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.ObservableUsersignInLogIn.SecurePassword = txtPassword.SecurePassword;
        }


        public ConnexionControl(IAbonneService abonneService, IDataBaseSeeder dataBaseSeeder)
        {
            _viewModel = new ConnexionModelView(abonneService);
            DataContext = _viewModel;
            dataBaseSeeder.Seed();
            _viewModel.ErrorOccured += _viewModel_ErrorOccurred;
            Unloaded += ConnexionControl_Unloaded;
            _viewModel.ConnexionErreur += _viewModel_ConnexionErreur;
            InitializeComponent();
        }

        private void _viewModel_ConnexionErreur(bool reussite)
        {
            if(reussite)
            {
                ((MainWindow)Application.Current.MainWindow).ConnecterWindow(_viewModel.User);
            }
            else
            {
                MessageBox.Show(Resource.errorConnection, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void ConnexionControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.ErrorOccured -= _viewModel_ErrorOccurred;
            _viewModel.ConnexionErreur-= _viewModel_ConnexionErreur;
        }

        private void _viewModel_ErrorOccurred(string information)
        {
            MessageBox.Show(information, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Creer_Compte(object sender, RoutedEventArgs e)
        {
            var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
            var ajoutUser = container.Resolve<AjoutUser>();
            ajoutUser.ShowDialog();
        }

    }
}