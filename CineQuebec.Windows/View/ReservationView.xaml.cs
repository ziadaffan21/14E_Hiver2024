using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window
    {
        private readonly IProjectionService _projectionService;
        private readonly IFilmService _filmService;

        public Abonne User { get; set; }

        public List<Film> Films { get; set; }

        public List<Projection> Projections { get; set; }

        public ReservationView(IFilmService filmService, IProjectionService projectionService, Abonne user)
        {
            InitializeComponent();
            _projectionService = projectionService;
            _filmService = filmService;
            User = user;
            _ = ChargerFilmsAsync();
            DataContext = this;
        }

        private async Task ChargerFilmsAsync()
        {
            Films = await _filmService.GetAllFilms();
            lstFilms.ItemsSource = Films;
        }

        private void lstFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var film = (Film)lstFilms.SelectedItem;
            if (film != null)
            {
                gpoProjections.Header = $"Projections ({film.Titre})";
                _ = ChargerProjectionsAsync(film);
            }
            else
            {
                gpoProjections.Header = $"Projections";
            }
        }

        private async Task ChargerProjectionsAsync(Film film)
        {
            var projections = await _projectionService.GetProjectionByName(film.Titre);
            Projections = projections;
            lstProjections.ItemsSource = Projections;
        }

        private void btConfirmer_Click(object sender, RoutedEventArgs e)
        {
            if (lstProjections.SelectedIndex >= 0)
            {
                var projection = lstProjections.SelectedItem as Projection;
                _ = EnvoyerReservation(projection);
            }
            else
            {
                MessageBox.Show("Veuillez choisir une projection valide");
            }
        }

        private async Task EnvoyerReservation(Projection projection)
        {
            
            await _projectionService.AjouterReservation(projection.Id,User.Id);
            MessageBox.Show("Réservation completée avec succées.", "Réservation");
        }

        private void lstProjections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btConfirmer.IsEnabled = lstProjections.SelectedIndex >= 0;
        }
    }
}