using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class ReservationViewModel : PropertyNotifier
    {
        private readonly IProjectionService _projectionService;
        private string _gpoProjections;
        private string _gpoFilm;
        private Projection _selectedProjection = new();
        private Film _film;


        public ICommand SaveCommand { get; init; }

        public Film Film
        {
            get { return _film; }
            set { _film = value; }
        }

        public Abonne User { get; set; }

        private ObservableCollection<Projection> _projections;
        public ObservableCollection<Projection> Projections
        {
            get { return _projections; }
            set
            {
                if (_projections != value)
                {
                    _projections = value;
                    OnPropertyChanged(nameof(Projections));
                }
            }
        }



        public Projection SelectedProjection
        {
            get => _selectedProjection;
            set
            {
                _selectedProjection = value;
                OnPropertyChanged(nameof(_selectedProjection));
            }
        }

        private Window _window;

        public Window Window
        {
            get { return _window; }
            set { _window = value; }
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


        public ReservationViewModel(IProjectionService projectionService, Film film, Abonne user, Window window)
        {
            _projectionService = projectionService;
            User = user;
            Film = film;
            SaveCommand = new DelegateCommand(AddReservation, CanAddReservation);
            Window = window; // Store the window instance for later use
        }

        public async void AddReservation()
        {
            try
            {

                MessageBoxResult reponse = MessageBox.Show($"Voulez vous réserver une place pour {SelectedProjection.Date.TimeOfDay}", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (reponse != MessageBoxResult.Yes)
                {
                    return;
                }
                await _projectionService.AjouterReservation(SelectedProjection.Id, User.Id);
                MessageBox.Show("Réservation completée avec succées.", "Réservation");
                Window.DialogResult = true;
                Window.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CanAddReservation()
        {
            return SelectedProjection.Film != null; 
        }

        public void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerProjection();
        }


        internal void Unloaded(object sender, RoutedEventArgs e)
        {

        }

        public async void ChargerProjection()
        {
            var projectionsCharge = await _projectionService.GetUpcomingProjections(Film.Id);

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
                GpoProjections = $"Aucune projections pour {Film.Titre}";
            }
            
        }

        internal void ReevaluateButton(object sender, SelectionChangedEventArgs e)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
