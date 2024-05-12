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
//        public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.Register(
//"SecurePassword", typeof(SecureString), typeof(MainWindow), new PropertyMetadata(default(SecureString)));
        //private readonly IAbonneService _abonneService;
        //private readonly IDataBaseSeeder _dataBaseSeeder;
        private readonly ConnexionModelView _viewModel;
        private StringBuilder sb = new();
        public Abonne User { get; set; }



        //public SecureString SecurePassword
        //{
        //    get => (SecureString)GetValue(SecurePasswordProperty);
        //    set => SetValue(SecurePasswordProperty, value);
        //}

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //SecurePassword = txtPassword.SecurePassword;
            //_viewModel.ObservableUsersignInLogIn.Password=SecurePassword.ToString();
        }


        public ConnexionControl(IAbonneService abonneService, IDataBaseSeeder dataBaseSeeder)
        {
            //_abonneService = abonneService;
            _viewModel = new ConnexionModelView(abonneService);
            DataContext = _viewModel;
            dataBaseSeeder.Seed();
            //_dataBaseSeeder = dataBaseSeeder;
            InitializeComponent();
            //_dataBaseSeeder.Seed();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!ValiderFomulaire())
            {
                MessageBox.Show(SecurePassword.Length.ToString());
                MessageBox.Show(sb.ToString(), Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
                //    User = await _abonneService.GetAbonneConnexion(txtUsername.Text.Trim(), txtPassword.Password.ToString());
                //    if (User is not null)
                //        ((MainWindow)Application.Current.MainWindow).ConnecterWindow(User);
                //    else
                //        MessageBox.Show(Resource.errorConnection, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Information);
                //}
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
            if (string.IsNullOrWhiteSpace(txtPassword.Password.ToString()))
                sb.AppendLine("Le champs mot de passe ne peut pas etre vide.");

            if (sb.Length > 0)
                return false;

            return true;
        }
    }
}