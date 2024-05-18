using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.ViewModel;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour FilmDetailsView.xaml
    /// </summary>
    public partial class FilmDetailsView : Window
    {
        public FilmDetailsViewModel _viewModel { get; set; }

        public FilmDetailsView(Film film)
        {



            _viewModel = new(film);
            DataContext = _viewModel;

            InitializeComponent();
            InitialiserDetails();
        }

        private void InitialiserDetails()
        {
            Title = _viewModel.Film.Titre;
            txtTitre.Text = _viewModel.Film.Titre;

            //TODO : Remplacer par une proprieté
            txtDescription.Text = Film.PLACEHOLDER_DESC;

            FormaterAffichage();
        }

        private async void FormaterAffichage()
        {
            var film = _viewModel.Film;


            bool hasUpcomingProjections = await _viewModel.HasUpcomingProjections();

            if (hasUpcomingProjections)
            {
                txtIndisponible.Visibility = Visibility.Collapsed;
            }


            if (await _viewModel.PeutNoter())
            {
                btNoter.Visibility = Visibility.Visible;
            }
            if (!await _viewModel.PeutReserver())
            {
                btReserver.Visibility = Visibility.Collapsed;
                txtIndisponible.Visibility = Visibility.Visible;
                txtIndisponible.Text = $"Aucune projections à venir";
            }
        }

        private void btNoter_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btReserver_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateInterface()
        {
            //TODO : Implementer la mise à jour de l'interface
        }
    }
}
