using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
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
        private StringBuilder sb = new();
        public Abonne User { get; set; }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.ObservableUsersignInLogIn.SecurePassword = txtPassword.SecurePassword;
        }


        public ConnexionControl(IAbonneService abonneService, IDataBaseSeeder dataBaseSeeder)
        {
            //_abonneService = abonneService;
            _viewModel = new ConnexionModelView(abonneService);
            DataContext = _viewModel;
            //dataBaseSeeder.Seed();
            //_dataBaseSeeder = dataBaseSeeder;
            InitializeComponent();
            //_dataBaseSeeder.Seed();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!ValiderFomulaire())
            {
                MessageBox.Show(sb.ToString(), Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Creer_Compte(object sender, RoutedEventArgs e)
        {
            var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
            var ajoutUser = container.Resolve<AjoutUser>();
            ajoutUser.ShowDialog();
        }

        private bool ValiderFomulaire()
        {
            sb.Clear();

            if (string.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
                sb.AppendLine("Le champs username ne peut pas etre vide.");
            if (string.IsNullOrEmpty(txtPassword.Password))
                sb.AppendLine("Le champs mot de passe ne peut pas etre vide.");

            if (sb.Length > 0)
                return false;

            return true;
        }
    }
}