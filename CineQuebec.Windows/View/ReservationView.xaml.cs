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
        private Abonne _user;
        
        public Abonne User
        {
            get { return _user; }
            set { _user = value; }
        }

        private ReservationViewModel _viewmodel { get; set; }

        public ReservationView(IProjectionService projectionService, Film film, Abonne user)
        {


            
            _viewmodel = new(projectionService, film, user,this);
            DataContext = _viewmodel;
            InitializeComponent();
            Loaded += _viewmodel.Loaded;
            Unloaded += _viewmodel.Unloaded;
            lstProjections.SelectionChanged += _viewmodel.ReevaluateButton;
        }


        private void btConfirmer_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = true;
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