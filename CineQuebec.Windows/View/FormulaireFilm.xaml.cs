using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.FilmExceptions;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using System.Text;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour DetailFilm.xaml
    /// </summary>
    public partial class DetailFilm : Window
    {
        private Film _film;
        public Etat Etat;
        private readonly IFilmService _filmService;
        private StringBuilder sb = new();

        public DetailFilm(IFilmService filmService, Film film = null)
        {
            InitializeComponent();
            _filmService = filmService;
            _film = film;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboCategories.ItemsSource = UtilEnum.GetAllDescriptions<Categories>();
            InitialiserFormulaire();
        }

        private void InitialiserFormulaire()
        {
            switch (Etat)
            {
                case Etat.Ajouter:
                    txtTitre.Text = "";
                    cboCategories.SelectedIndex = -1;
                    dateSortie.SelectedDate = DateTime.Now;
                    txtDuree.Text = "";
                    lblTitre.Text = "Ajouter un film";
                    btnAjouterModifier.Content = "Ajouter";
                    break;

                case Etat.Modifier:
                    txtTitre.Text = _film.Titre;
                    cboCategories.SelectedIndex = (int)_film.Categorie;
                    dateSortie.SelectedDate = _film.DateSortie;
                    txtDuree.Text = _film.Duree.ToString();
                    lblTitre.Text = "Modifier un film";
                    btnAjouterModifier.Content = "Modifier";
                    break;

                default:
                    break;
            }
        }

        private async void btnAjouterModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValiderForm())
                {
                    switch (Etat)
                    {
                        case Etat.Ajouter:
                            _film = new Film(txtTitre.Text, (DateTime)dateSortie.SelectedDate, int.Parse(txtDuree.Text), (Categories)cboCategories.SelectedIndex);
                            await _filmService.AjouterFilm(_film);
                            DialogResult = true;
                            MessageBox.Show(Resource.ajoutReussi, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                        case Etat.Modifier:
                            _film.Titre = txtTitre.Text;
                            _film.Categorie = (Categories)cboCategories.SelectedIndex;
                            _film.DateSortie = (DateTime)dateSortie.SelectedDate;
                            _film.Duree = int.Parse(txtDuree.Text);
                            DialogResult = true;
                            await _filmService.ModifierFilm(_film);
                            MessageBox.Show(Resource.modificationReussi, Resource.modification, MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                        default:
                            break;
                    }
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