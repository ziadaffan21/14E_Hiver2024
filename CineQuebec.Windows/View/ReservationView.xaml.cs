using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;

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
    }
}
