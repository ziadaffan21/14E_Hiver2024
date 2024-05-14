using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
        private readonly IFilmService _filmService;
        private readonly IProjectionService _projectionService;

        private AbonneHomeViewModel _viewModel;

        public AbonneHomeControl(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository, IFilmService filmService, IProjectionService projectionService)
        {
            InitializeComponent();
            _abonneService = abonneService;
            _realisateurRepository = realisateurRepository;
            _acteurRepository = acteurRepository;
            _filmService = filmService;
            _projectionService = projectionService;


            _viewModel = new(filmService);
            DataContext = _viewModel;
        }

      

        private void btnReserverUnePlace_Click(object sender, RoutedEventArgs e)
        {
            var reservationView = new ReservationView(_filmService, _projectionService, User);
            reservationView.ShowDialog();

        }

        private void btnVoirPreferance_Click(object sender, RoutedEventArgs e)
        {
            var listPreferanceView = new ListPreferencesView(_abonneService, _realisateurRepository, _acteurRepository, _filmService, User);
            if ((bool)listPreferanceView.ShowDialog())
                MessageBox.Show("Votre préférence ont été mis à jour avec succès", "Préférence", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnNoteUnFilm_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}