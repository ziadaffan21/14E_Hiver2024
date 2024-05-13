using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour FilmDetailsView.xaml
    /// </summary>
    public partial class FilmDetailsView : Window
    {

        private Film _film;

        public Film Film
        {
            get { return _film; }
            set { _film = value; }
        }

        public Abonne User { get; set; }

        IProjectionService ProjectionService { get; set; }

        public FilmDetailsView(Film film)
        {
            Film = film;
            DataContext = Film;
            ProjectionService = AbonneHomeControl.ProjectionService;
            User = AbonneHomeControl.CurrentUser;
            InitializeComponent();
            InitialiserDetails();
        }

        private void InitialiserDetails()
        {
            Title = Film.Titre;
            txtTitre.Text = Film.Titre;



            //TODO : Remplacer par une proprieté
            txtDescription.Text = Film.PLACEHOLDER_DESC;

            UriBuilder uriBuilder = new UriBuilder("https://picsum.photos/seed/1/200/300");
            FilmImage.Source = new BitmapImage(uriBuilder.Uri);
            FormaterAffichage();
        }

        private void FormaterAffichage()
        {
            if (Film.EstAffiche)
            {
                //TODO : Checker si projections
                if (true)
                {
                    txtIndisponible.Visibility = Visibility.Collapsed;
                }
                else
                {
                    txtIndisponible.Visibility = Visibility.Visible;
                    txtIndisponible.Text = $"Disponible le {Film.DateSortie.Day} {Utils.GetMoisNom(Film.DateSortie)} {Film.DateSortie.Year}";
                }
            }
        }

        private void btNoter_Click(object sender, RoutedEventArgs e)
        {
            OuvrirFormNoter();
        }

        private void OuvrirFormNoter()
        {
            NoterView noterView = new NoterView(Film);
            noterView.Show();
        }

        private void btReserver_Click(object sender, RoutedEventArgs e)
        {
            ReservationView reservationView = new(ProjectionService, Film, User);
            bool? resultat = reservationView.ShowDialog();
            if (resultat == true)
            {
                UpdateInterface();
            }
        }

        private void UpdateInterface()
        {
            //TODO : Implementer la mise à jour de l'interface
        }
    }
}
