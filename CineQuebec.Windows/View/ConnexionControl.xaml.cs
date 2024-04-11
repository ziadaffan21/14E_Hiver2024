using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Ressources.i18n;
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
        private readonly IAbonneService _abonneService;
        private readonly IDataBaseSeeder _dataBaseSeeder;
        private StringBuilder sb = new();

        public ConnexionControl(IAbonneService abonneService, IDataBaseSeeder dataBaseSeeder)
        {
            _abonneService = abonneService;
            _dataBaseSeeder = dataBaseSeeder;
            InitializeComponent();
            _dataBaseSeeder.Seed();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValiderFomulaire())
            {
                if (await _abonneService.GetAbonneConnexion(txtUsername.Text.Trim(), txtPassword.Password.ToString()))
                    ((MainWindow)Application.Current.MainWindow).ConnecterWindow();
                else
                    MessageBox.Show(Resource.errorConnection, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(sb.ToString(), Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Creer_Compte(object sender, RoutedEventArgs e)
        {
            var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
            var ajoutUser = container.Resolve<AjoutUser>();
            ajoutUser.ShowDialog();
            //if (!(bool)ajoutUser.ShowDialog())
            //    MessageBox.Show(Resource.errorAjoutUser, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool ValiderFomulaire()
        {
            sb.Clear();

            if (string.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
                sb.AppendLine("Le champs username ne peut pas etre vide.");
            if (string.IsNullOrWhiteSpace(txtPassword.Password.ToString()))
                sb.AppendLine("Le champs mot de passe ne peut pas etre vide.");

            if (sb.Length > 0)
                return false;

            return true;
        }
    }
}