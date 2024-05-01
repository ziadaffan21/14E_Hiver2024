using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using Unity;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour AbonneHomeControl.xaml
    /// </summary>
    public partial class AbonneHomeControl : UserControl
    {
        public Abonne User { get; set; }
        private readonly IAbonneService _abonneService;
        private readonly IRealisateurRepository _realisateurRepository;
        private readonly IActeurRepository _acteurRepository;
        public AbonneHomeControl(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository)
        {
            InitializeComponent();
            _abonneService = abonneService;
            _realisateurRepository = realisateurRepository;
            _acteurRepository = acteurRepository;
        }

        private void btnReserverUnePlace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVoirPreferance_Click(object sender, RoutedEventArgs e)
        {
            var listPreferanceView = new ListPreferencesView(_abonneService, _realisateurRepository, _acteurRepository, User);
            listPreferanceView.ShowDialog();
        }

        private void btnNoteUnFilm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
