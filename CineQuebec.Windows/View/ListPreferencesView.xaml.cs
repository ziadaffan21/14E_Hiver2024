using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.AbonneExceptions;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using CineQuebec.Windows.Ressources.i18n;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ListPreferencesView.xaml
    /// </summary>
    public partial class ListPreferencesView : Window
    {
        private readonly IAbonneService _abonneService;
        private readonly IRealisateurRepository _realisateurRepository;
        private readonly IActeurRepository _acteurRepository;
        private readonly IFilmService _filmService;

        public Abonne User { get; set; }

        public ListPreferencesView(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository, IFilmService filmService, Abonne abonne = null)
        {
            InitializeComponent();
            _abonneService = abonneService;
            _realisateurRepository = realisateurRepository;
            _acteurRepository = acteurRepository;
            _filmService = filmService;
            User = abonne;
            Loaded += ListPreferencesView_Loaded;
        }

        private async void ListPreferencesView_Loaded(object sender, RoutedEventArgs e)
        {
            User = await _abonneService.GetAbonne(User.Id);
            cbActeurs.ItemsSource = await ChargerActeurs();
            cbRealisateurs.ItemsSource = await ChargerRealisateurs();
            cbFilms.ItemsSource = await ChargerFilms();
            cbCategorie.ItemsSource = ChargerCategorie();
            lstRealisateurs.ItemsSource = User.Realisateurs;
            lstActeurs.ItemsSource = User.Acteurs;
            lstCategories.ItemsSource = User.CategoriesPrefere;
            lstFilms.ItemsSource = User.Films;
        }

        private async void InitializeListsAndComboBoxes()
        {
            User = await _abonneService.GetAbonne(User.Id);
            lstRealisateurs.ItemsSource = User.Realisateurs;
            lstActeurs.ItemsSource = User.Acteurs;
            lstFilms.ItemsSource = User.Films;
            lstCategories.ItemsSource = User.CategoriesPrefere;
        }

        private static IEnumerable<Categories> ChargerCategorie()
        {
            return Enum.GetValues(typeof(Categories)).Cast<Categories>();
        }

        private async Task<IEnumerable<Film>> ChargerFilms()
        {
            return await _filmService.GetAllFilms();
        }

        private async Task<IEnumerable<Realisateur>> ChargerRealisateurs()
        {
            return await _realisateurRepository.GetAll();
        }

        private async Task<IEnumerable<Acteur>> ChargerActeurs()
        {
            return await _acteurRepository.GetAll();
        }

        private async void btnAddRealisateur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRealisateur = cbRealisateurs.SelectedItem as Realisateur;
                if (selectedRealisateur != null)
                {
                    var result = await _abonneService.AddRealisateurInAbonne(User.Id, selectedRealisateur);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        cbRealisateurs.SelectedIndex = -1;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                {
                    MessageBox.Show(Resource.selection_un_realisateur_ajout, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (NumberRealisateursOutOfRange ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                cbRealisateurs.SelectedIndex = -1;
            }
            catch (RealisateurAlreadyExistInRealisateurList ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                cbRealisateurs.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddActeur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedActeur = cbActeurs.SelectedItem as Acteur;
                if (selectedActeur != null)
                {
                    var result = await _abonneService.AddActeurInAbonne(User.Id, selectedActeur);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        cbActeurs.SelectedIndex = -1;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                {
                    MessageBox.Show(Resource.selection_un_acteur_ajout, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (NumberActeursOutOfRange ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                cbActeurs.SelectedIndex = -1;
            }
            catch (ActeurAlreadyExistInRealisateurList ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                cbActeurs.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddCategorie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCategorie = cbCategorie.SelectedIndex;
                if (selectedCategorie != -1)
                {
                    var result = await _abonneService.AddCategorieInAbonne(User.Id, (Categories)selectedCategorie);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        cbCategorie.SelectedIndex = -1;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                {
                    MessageBox.Show(Resource.selection_un_cat_ajout, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (NumberCategoriesOutOfRange ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                cbCategorie.SelectedIndex = -1;
            }
            catch (CategorieAlreadyExistInRealisateurList ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                cbCategorie.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddFilms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedFilm = cbFilms.SelectedItem as Film;
                if (selectedFilm != null)
                {
                    var result = await _abonneService.AddFilmInAbonne(User.Id, selectedFilm);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        cbFilms.SelectedIndex = -1;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_film_ajout, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FilmAlreadyExistInRealisateurList ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnRemoveRealisateur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRealisateur = lstRealisateurs.SelectedItem as Realisateur;
                if (selectedRealisateur != null)
                {
                    var result = await _abonneService.RemoveRealisateurInAbonne(User.Id, selectedRealisateur);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        lstRealisateurs.SelectedItem = null;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_realisateur_supression, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void btnRemoveActeur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedActeur = lstActeurs.SelectedItem as Acteur;
                if (selectedActeur != null)
                {
                    var result = await _abonneService.RemoveActeurInAbonne(User.Id, selectedActeur);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        lstActeurs.SelectedItem = null;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_acteur_supression, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void btnRemoveFilms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedFilm = lstFilms.SelectedItem as Film;
                if (selectedFilm != null)
                {
                    var result = await _abonneService.RemoveFilmInAbonne(User.Id, selectedFilm);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        lstFilms.SelectedItem = null;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_film_supression, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void btnRemoveCategorie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCategorie = lstCategories.SelectedIndex;
                if (selectedCategorie != -1)
                {
                    var result = await _abonneService.RemoveCategorieInAbonne(User.Id, (Categories)selectedCategorie);
                    if (result)
                    {
                        User = await _abonneService.GetAbonne(User.Id);
                        lstCategories.SelectedIndex = -1;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_categorie_supression, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}