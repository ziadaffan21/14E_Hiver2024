using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            ChargerFilms();

        }
        private void ChargerFilms()
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
