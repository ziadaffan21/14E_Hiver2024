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

            for (int i = 0; i < Films.Count; i++) 
            {
                var film = Films[i];
                if (!film.EstAffiche)
                {
                    Films.Remove(film);
                }
            }

            if (Films.Count > 0)
            {
            lstFilms.ItemsSource = Films;
            }
            else
            {
                gpoFilms.Header = "Aucun Films à l'affiche";
            }
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
            var projectionsCharge = await _projectionService.GetProjectionByName(film.Titre);
            Projections = projectionsCharge;


            //Filtrage des projections déja réservé
            for (int i = 0; i < Projections.Count; i++)
            {
                var projection = Projections[i];
                if (projection.DejaReserve(User.Id))
                {
                    Projections.Remove(projection);
                }
            }

            if (Projections.Count > 0)
            {
                lstProjections.ItemsSource = Projections;

            }
            else
            {
                gpoProjections.Header = $"Aucune projections pour {film.Titre}";
            }
        }

        private void btConfirmer_Click(object sender, RoutedEventArgs e)
        {
            Film film = (Film)lstFilms.SelectedItem;
            MessageBoxResult reponse = MessageBox.Show($"Voulez vous réserver une place pour {film}", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (reponse != MessageBoxResult.Yes)
            {
                return;
            }

            if (lstProjections.SelectedIndex >= 0)
            {
                var projection = lstProjections.SelectedItem as Projection;
                _ = EnvoyerReservation(projection);
                Close();
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