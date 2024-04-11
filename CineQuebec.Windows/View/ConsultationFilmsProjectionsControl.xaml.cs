using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ConsultationFilmsControl.xaml
    /// </summary>
    public partial class ConsultationFilmsProjectionsControl : UserControl
    {
        private readonly IFilmService _filmService;
        private readonly IProjectionService _projectionService;

        public ConsultationFilmsProjectionsControl(IFilmService filmService, IProjectionService projectionService)
        {
            _filmService = filmService;
            _projectionService = projectionService;
            InitializeComponent();
            ChargerFilmProjection();
        }

        private void ChargerFilmProjection()
        {
            try
            {
                lstFilms.ItemsSource = _filmService.GetAllFilms();
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

        /// <summary>
        /// Événement lancé lors de lu double click d'un élément dans la liste des films
        /// </summary>
        /// <param name="sender">ListBox contenant les films</param>
        /// <param name="e"></param>
        private void lstFilm_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            AfficherDetailsFilm();
        }

        private void AfficherDetailsFilm()
        {
            if (lstFilms.SelectedItem != null)
            {
                Film film = lstFilms.SelectedItem as Film;
                DetailFilm detailFilm = new DetailFilm(_filmService, film);
                if ((bool)detailFilm.ShowDialog())
                    ChargerFilmProjection();
                
            }
        }

        private void lstFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Film selectedFilm = lstFilms.SelectedItem as Film;
            if (selectedFilm is not null)
                lstProjections.ItemsSource = _projectionService.ReadProjectionsById(selectedFilm.Id);
        }

        private void btnAjoutFilm_Click(object sender, RoutedEventArgs e)
        {
            DetailFilm detailFilm = new DetailFilm(_filmService);
            if ((bool)detailFilm.ShowDialog())
                ChargerFilmProjection();
        }

        private void btnAjoutProjection_Click(object sender, RoutedEventArgs e)
        {
            AjoutDetailProjection detailProjection = new AjoutDetailProjection(_projectionService, _filmService);
            if ((bool)detailProjection.ShowDialog())
                ChargerFilmProjection();
        }
    }
}