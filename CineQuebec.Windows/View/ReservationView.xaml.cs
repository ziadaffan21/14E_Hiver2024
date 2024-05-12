using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
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



        private ReservationViewModel _viewmodel { get; set; }

        public ReservationView(IFilmService filmService, IProjectionService projectionService, Abonne user)
        {
            _viewmodel = new(projectionService, filmService, user);
            DataContext = _viewmodel;
            InitializeComponent();

            Loaded += _viewmodel.Loaded;
            Unloaded += _viewmodel.Unloaded;
            lstFilms.SelectionChanged += _viewmodel.ChargerProjection;
            lstFilms.ItemsSource = _viewmodel.Films;
        }



        private void lstFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var film = (Film)lstFilms.SelectedItem;
            if (lstFilms.SelectedIndex >= 0)
            {
                gpoProjections.Header = $"Projections ({film.Titre})";
            }
            else
            {
                gpoProjections.Header = $"Projections";
            }
        }


        private void btConfirmer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            //Close();
        }

        private async Task EnvoyerReservation(Projection projection)
        {

            await _projectionService.AjouterReservation(projection.Id, User.Id);
            MessageBox.Show("Réservation completée avec succées.", "Réservation");
        }

        private void lstProjections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btConfirmer.IsEnabled = lstProjections.SelectedIndex >= 0;
        }
    }
}