using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour AjoutDetailProjection.xaml
    /// </summary>
    public partial class AjoutDetailProjection : Window
    {
        private Projection _projection;
        StringBuilder sb = new();

        private readonly IProjectionService _projectionService;

        private readonly IFilmService _filmService;

        public AjoutDetailProjection(IProjectionService projectionService, IFilmService filmService)
        {
            InitializeComponent();
            _projection = new Projection();
            _projectionService = projectionService;
            _filmService = filmService;
            DataContext = _projection;
            calendrier.DisplayDateStart = DateTime.Now;
        }

        private async void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValiderForm())
                {
                    DateTime formatedTime = GetDateAndTime(_projection.Date, (DateTime)horloge.SelectedTime);
                    await _projectionService.AjouterProjection(new Projection(formatedTime,int.Parse(txtPlace.Text),cboFilm.SelectedItem as Film));
                    MessageBox.Show(Resource.ajoutReussiProjection, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show(sb.ToString(), Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (ExistingProjectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MongoDataConnectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(Resource.erreurGenerique, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DateTime GetDateAndTime(DateTime date, DateTime time)
        {

            DateTime newDate = new(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            return newDate;

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool ValiderForm()
        {
            sb.Clear();

            if (calendrier.SelectedDate is null || calendrier.SelectedDate < DateTime.Today)
                sb.AppendLine($"La date sélectionnée doit être plus grande ou égale à {DateTime.Today}.");
            if (horloge.SelectedTime is null)
                sb.AppendLine($"Il faut sélectionner une heure pour la projection.");
            if (cboFilm.SelectedIndex == -1)
                sb.AppendLine($"Vous devez assigner un film");
            if (string.IsNullOrWhiteSpace(txtPlace.Text) || int.Parse(txtPlace.Text) < Projection.NB_PLACE_MIN)
                sb.AppendLine($"Le nombre de place doit être plus grand que {Projection.NB_PLACE_MIN}.");

            if (sb.Length > 0)
                return false;

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiserFormulaireAjout();
        }

        private void InitialiserFormulaireAjout()
        {
            cboFilm.ItemsSource = _filmService.GetAllFilms();
            cboFilm.Focus();
            txtPlace.Focus();
        }

        private void cboFilm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboFilm.SelectedIndex != -1)
            {
                Film film = cboFilm.SelectedItem as Film;
                if (film != null)
                {
                    _projection.Film = film;
                }
            }
        }

        private void horloge_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            MessageBox.Show("OK");
        }
    }
}