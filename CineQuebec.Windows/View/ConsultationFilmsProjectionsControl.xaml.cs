using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
using CineQuebec.Windows.ViewModel.Event;
using Prism.Events;
using System.Runtime.CompilerServices;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly ConsultationFilmsProjectionsModel _viewModel;

        public ConsultationFilmsProjectionsControl(IFilmService filmService, IProjectionService projectionService, IEventAggregator eventAggregator)
        {
            _filmService = filmService;
            _projectionService = projectionService;
            _eventAggregator = eventAggregator;
            _viewModel = new ConsultationFilmsProjectionsModel(filmService, projectionService, eventAggregator);
            _eventAggregator = eventAggregator;
            DataContext = _viewModel;
            Loaded += _viewModel.Load; 
            _viewModel.ErrorOccured += _viewModel_ErrorOccured;
            Unloaded += ConsultationFilmsProjectionsControl_Unloaded;
            InitializeComponent();
        }

        private void ConsultationFilmsProjectionsControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.ErrorOccured -= _viewModel_ErrorOccured;
        }

        private void _viewModel_ErrorOccured(string error)
        {
            MessageBox.Show(error, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void lstFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Film selectedFilm = lstFilms.SelectedItem as Film;
            if (selectedFilm is not null)
                _viewModel.LoadProjectionFilm(selectedFilm.Id);
            gpoProjections.Header = selectedFilm is not null ? $"Projections ({selectedFilm.Titre})" : "Projections";
        }

        private void btnAjoutFilm_Click(object sender, RoutedEventArgs e)
        {
            DetailFilm detailFilm = new DetailFilm(_filmService, _eventAggregator);
            detailFilm.ShowDialog();

        }

        private void btnAjoutProjection_Click(object sender, RoutedEventArgs e)
        {
            FormulaireProjection detailProjection = new FormulaireProjection(_projectionService,_filmService,_eventAggregator);

            
            bool reponse = (bool)detailProjection.ShowDialog();
            
            lstFilms.SelectedIndex = -1;
        }

        private void btModifierProjection_Click(object sender, RoutedEventArgs e)
        {
            Projection projection = lstProjections.SelectedItem as Projection;
            if (projection != null)
            {
                _viewModel.ModifierProjection(_eventAggregator, projection);
                lstFilms.SelectedIndex = -1;
            }
        }

        private void btSupprimerProjection_Click(object sender, RoutedEventArgs e)
        {
            Projection projection = (Projection)lstProjections.SelectedItem;

            if (projection != null)
            {
                _viewModel.SupprimerProjection(projection);
            }

        }

        private void btModifierFilm_Click(object sender, RoutedEventArgs e)
        {
            Film film = lstFilms.SelectedItem as Film;

            if (film != null)
            {
                
                _viewModel.ModifierFIlm(_eventAggregator, film);
            }
        }

        private void btSupprimerFilm_Click(object sender, RoutedEventArgs e)
        {

            Film film = (Film)lstFilms.SelectedItem;

            if (film != null)
            {
                _viewModel.SupprimerFilm(film);
            }
        }
    }
}