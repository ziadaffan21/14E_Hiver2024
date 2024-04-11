using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ConsultationAbonnesControl.xaml
    /// </summary>
    public partial class ConsultationAbonnesControl : UserControl
    {
        private readonly IAbonneService _abonneService;

        public ConsultationAbonnesControl(IAbonneService abonneService)
        {
            _abonneService = abonneService;
            InitializeComponent();
            ChargerAbonnes();
        }

        private void ChargerAbonnes()
        {
            try
            {
                var abonne = _abonneService.GetAllAbonnes();
                lstUtilisisateurs.ItemsSource = abonne;
            }
            catch (MongoDataConnectionException err)
            {
                MessageBox.Show(err.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(Resource.erreurGenerique, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}