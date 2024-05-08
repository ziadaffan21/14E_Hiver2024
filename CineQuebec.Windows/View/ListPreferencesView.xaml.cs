using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
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
        private readonly ListPreferencesViewModel _viewModel;

        public Abonne User { get; set; }

        public ListPreferencesView(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository, IFilmService filmService, Abonne abonne = null)
        {
            InitializeComponent();
            _abonneService = abonneService;
            _realisateurRepository = realisateurRepository;
            _acteurRepository = acteurRepository;
            _filmService = filmService;
            _viewModel = new ListPreferencesViewModel(_abonneService, _realisateurRepository, _acteurRepository, _filmService, abonne);
            DataContext = _viewModel;
            User = abonne;
            Loaded += _viewModel.Loaded;
            ((ListPreferencesViewModel)this.DataContext).ErrorOccurred += HandleError;
            this.Unloaded += ListPreferencesView_Unloaded;
        }

        private void ListPreferencesView_Unloaded(object sender, RoutedEventArgs e)
        {
            ((ListPreferencesViewModel)this.DataContext).ErrorOccurred -= HandleError;
        }

        private void HandleError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void ListPreferencesView_Loaded(object sender, RoutedEventArgs e)
        {
            User = await _abonneService.GetAbonne(User.Id);
        }

        private async void InitializeListsAndComboBoxes()
        {
            User = await _abonneService.GetAbonne(User.Id);
            lstActeurs.ItemsSource = User.Acteurs;
            lstFilms.ItemsSource = User.Films;
            lstCategories.ItemsSource = User.CategoriesPrefere;
        }

        private async void btnAddActeur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedActeur = cbActeurs.SelectedItem as Acteur;
                if (selectedActeur != null)
                {
                    var result = await _abonneService.AddActeurInAbonne(User, selectedActeur);
                    if (result is not null)
                    {
                        User = result;
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
            catch (ActeurAlreadyExistInActeursList ex)
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
                    var result = await _abonneService.AddCategorieInAbonne(User, (Categories)selectedCategorie);
                    if (result is not null)
                    {
                        User = result;
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
            catch (CategorieAlreadyExistInCategoriesList ex)
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
                    var result = await _abonneService.AddFilmInAbonne(User, selectedFilm);
                    if (result is not null)
                    {
                        User = result;
                        cbFilms.SelectedIndex = -1;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_film_ajout, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FilmAlreadyExistInFilmsList ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
                cbFilms.SelectedIndex = -1;
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
                    var result = await _abonneService.RemoveRealisateurInAbonne(User, selectedRealisateur);
                    if (result is not null)
                    {
                        User = result;
                        lstRealisateurs.SelectedItem = null;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_realisateur_supression, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnRemoveActeur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedActeur = lstActeurs.SelectedItem as Acteur;
                if (selectedActeur != null)
                {
                    var result = await _abonneService.RemoveActeurInAbonne(User, selectedActeur);
                    if (result is not null)
                    {
                        User = result;
                        lstActeurs.SelectedItem = null;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_acteur_supression, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnRemoveFilms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedFilm = lstFilms.SelectedItem as Film;
                if (selectedFilm != null)
                {
                    var result = await _abonneService.RemoveFilmInAbonne(User, selectedFilm);
                    if (result is not null)
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
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnRemoveCategorie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCategorie = lstCategories.SelectedIndex;
                if (selectedCategorie != -1)
                {
                    var result = await _abonneService.RemoveCategorieInAbonne(User, (Categories)selectedCategorie);
                    if (result is not null)
                    {
                        User = result;
                        lstCategories.SelectedIndex = -1;
                        InitializeListsAndComboBoxes();
                    }
                }
                else
                    MessageBox.Show(Resource.selection_un_categorie_supression, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}