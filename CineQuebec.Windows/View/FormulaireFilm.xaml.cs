using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.FilmExceptions;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
using System.Text;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour DetailFilm.xaml
    /// </summary>
    public partial class DetailFilm : Window
    {
        //private Film _film;
        private Etat Etat;
        //private readonly IFilmService _filmService;
        private StringBuilder sb = new();

        private readonly FormulaireFilmViewModel _viewModel;

        public DetailFilm(Etat etat,IFilmService filmService, Film film = null)
        {
            InitializeComponent();
            _viewModel = new FormulaireFilmViewModel(filmService, film);
            DataContext = _viewModel;
            Etat = etat;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateSortie.DisplayDate = DateTime.Now;
            InitialiserFormulaire();
        }

        private void InitialiserFormulaire()
        {
            switch (Etat)
            {
                case Etat.Ajouter:
                    lblTitre.Text = "Ajouter un film";
                    btnAjouterModifier.Content = "Ajouter";
                    break;

                case Etat.Modifier:
                    lblTitre.Text = "Modifier un film";
                    btnAjouterModifier.Content = "Modifier";
                    break;

                default:
                    DialogResult=false;
                    break;
            }
        }

        private void btnAjouterModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValiderForm())
                {
                    DialogResult = true;
                }
                else
                    MessageBox.Show(sb.ToString(), Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ExistingFilmException ex)
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

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool ValiderForm()
        {
            sb.Clear();

            if (string.IsNullOrWhiteSpace(txtTitre.Text.Trim()) || txtTitre.Text.Trim().Length < Film.NB_MIN_CARACTERES_USERNAME || txtTitre.Text.Trim().Length > Film.NB_MAX_CARACTERES_USERNAME)
                sb.AppendLine($"Le titre doit etre entre {Film.NB_MIN_CARACTERES_USERNAME} et {Film.NB_MAX_CARACTERES_USERNAME} caractères.");
            if (!DateTime.TryParse(dateSortie.SelectedDate.ToString(), out _))
                sb.AppendLine("La date ne peut pas etre vide.");
            if (cboCategories.SelectedIndex == -1)
                sb.AppendLine("Vous devez assigner une catégorie");
            if (string.IsNullOrWhiteSpace(txtDuree.Text) || int.Parse(txtDuree.Text) <= Film.NB_MIN_DUREE)
                sb.AppendLine($"La durée du film doit etre plus grande que {Film.NB_MIN_DUREE} minutes.");

            if (sb.Length > 0)
                return false;

            return true;
        }
    }
}