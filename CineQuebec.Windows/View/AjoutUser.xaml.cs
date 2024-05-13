using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
using Microsoft.VisualBasic;
using System.Text;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour AjoutUser.xaml
    /// </summary>
    public partial class AjoutUser : Window
    {

        private readonly AjoutUserViewModel _viewModel;

        public AjoutUser(IAbonneService abonneService)
        {
            InitializeComponent();
            _viewModel = new AjoutUserViewModel(abonneService);
            DataContext = _viewModel;
            _viewModel.ErrorOccurred += _viewModel_ErrorOccurred;
            _viewModel.ReussiteOuEchec += _viewModel_ReussiteOuEchec;
            Unloaded += AjoutUser_Unloaded;
        }

        private void _viewModel_ReussiteOuEchec(bool reussite)
        {
            if (reussite)
                MessageBox.Show(Resource.ajoutUser, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(Resource.errorAjoutUser, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);


        }

        private void AjoutUser_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.ErrorOccurred -= _viewModel_ErrorOccurred;
            _viewModel.ReussiteOuEchec -= _viewModel_ReussiteOuEchec;
        }

        private void _viewModel_ErrorOccurred(string information)
        {
            MessageBox.Show(information, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.ObservableUsersignInLogIn.SecurePassword = txtPassword.SecurePassword;
        }
    }
}