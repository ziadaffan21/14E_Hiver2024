using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    internal class ReservationViewModel : PropertyNotifier
    {
        private readonly IProjectionService _projectionService;
        private readonly IFilmService _filmService;
        private string _gpoProjections;
        private string _gpoFilm;
        private Film _selectedFilm = new();
        private Film _selectedProjection = new();
        private ObservableCollection<Film> _films = new ObservableCollection<Film>();

        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set
            {
                if (_films != value)
                {
                    _films = value;
                    OnPropertyChanged(nameof(Films));
                }
            }
        }


        public Abonne User { get; set; }

        public ObservableCollection<Projection> Projections { get; set; } = [];

        public ICommand AddReservationCommand { get; init; }



        public Film SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }

        public Film SelectedProjection
        {
            get => _selectedProjection;
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }


        public string GpoFilm
        {
            get { return _gpoFilm; }
            set { _gpoFilm = value; }
        }

        public string GpoProjections
        {
            get { return _gpoProjections; }
            set { _gpoProjections = value; }
        }


        public ReservationViewModel(IProjectionService projectionService, IFilmService filmService, Abonne user)
        {
            _projectionService = projectionService;
            _filmService = filmService;
            User = user;
            AddReservationCommand = new DelegateCommand(AddReservation);
        }

        private async void AddReservation()
        {
            try
            {

                MessageBoxResult reponse = MessageBox.Show($"Voulez vous réserver une place pour {SelectedFilm}", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (reponse != MessageBoxResult.Yes)
                {
                    return;
                }

                if (SelectedFilm != null)
                {

                    await _projectionService.AjouterReservation(SelectedProjection.Id, User.Id);
                    MessageBox.Show("Réservation completée avec succées.", "Réservation");
                }
                else
                {
                    MessageBox.Show("Veuillez choisir une projection valide");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerFilms();
        }

        private async void ChargerFilms()
        {
            var filmsCharge = await _filmService.GetAllFilms();

            //TODO : Mettre les films dans Films
            Films = new(filmsCharge);


            for (int i = 0; i < Films.Count; i++)
            {
                var film = Films[i];
                if (!film.EstAffiche)
                {
                    Films.Remove(film);
                }
            }
        }

        internal void Unloaded(object sender, RoutedEventArgs e)
        {

        }

        internal async void ChargerProjection(object sender, SelectionChangedEventArgs e)
        {
            var projectionsCharge = await _projectionService.GetProjectionByName(SelectedFilm.Titre);

            //TODO : Mettre les projections dans Projections
            Projections = new(projectionsCharge);


            //Filtrage des projections déja réservé
            for (int i = 0; i < Projections.Count; i++)
            {
                var projection = Projections[i];
                if (projection.DejaReserve(User.Id))
                {
                    Projections.Remove(projection);
                }
            }

            if (Projections.Count <= 0)
            {
                GpoProjections = $"Aucune projections pour {SelectedFilm.Titre}";
            }
        }
    }
}
