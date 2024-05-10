using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class ListPreferencesViewModel : PropertyNotifier
    {
        public event Action<string> ErrorOccurred;
        private readonly IAbonneService _abonneService;
        private readonly IRealisateurRepository _realisateurRepository;
        private readonly IActeurRepository _acteurRepository;
        private readonly IFilmService _filmService;

        private ObservableCollection<Acteur> _acteursPreferee = [];
        private ObservableCollection<Realisateur> _realisateursPreferee = [];
        private ObservableCollection<Film> _filmsPreferee = [];
        private ObservableCollection<Categories> _categoriesPreferee = [];
        private Realisateur _selectedRealisateur = new();
        private Realisateur _deleteSelectedRealisateur = new();
        private Acteur _selectedActeur = new();
        private Acteur _deleteSelectedActeur = new();
        private Film _selectedFilm = new();
        private Film _deleteSelectedFilm = new();
        private Categories _selectedCategorie = new();
        private Categories _deleteSelectedCategorie = new();


        public ICommand AddRealisateurCommand { get; init; }
        public ICommand DeleteRealisateurCommand { get; init; }
        public ICommand AddActeurCommand { get; init; }
        public ICommand DeleteActeurCommand { get; init; }
        public ICommand AddFilmCommand { get; init; }
        public ICommand DeleteFilmCommand { get; init; }
        public ICommand AddCategorieCommand { get; init; }
        public ICommand DeleteCategorieCommand { get; init; }

        public Abonne _abonne { get; set; } = new();
        public Realisateur SelectedRealisateur
        {
            get => _selectedRealisateur;
            set
            {
                _selectedRealisateur = value;
                OnPropertyChanged(nameof(SelectedRealisateur));
            }
        }
        public Realisateur DeleteSelectedRealisateur
        {
            get => _deleteSelectedRealisateur;
            set
            {
                _deleteSelectedRealisateur = value;
                OnPropertyChanged(nameof(DeleteSelectedRealisateur));
            }
        }
        public Acteur SelectedActeur
        {
            get => _selectedActeur;
            set
            {
                _selectedActeur = value;
                OnPropertyChanged(nameof(SelectedActeur));
            }
        }
        public Acteur DeleteSelectedActeur
        {
            get => _deleteSelectedActeur;
            set
            {
                _deleteSelectedActeur = value;
                OnPropertyChanged(nameof(DeleteSelectedRealisateur));
            }
        }
        public Film SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }
        public Film DeleteSelectedFilm
        {
            get => _deleteSelectedFilm;
            set
            {
                _deleteSelectedFilm = value;
                OnPropertyChanged(nameof(DeleteSelectedFilm));
            }
        }
        public Categories SelectedCategorie
        {
            get => _selectedCategorie;
            set
            {
                _selectedCategorie = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }
        public Categories DeleteSelectedCategorie
        {
            get => _deleteSelectedCategorie;
            set
            {
                _deleteSelectedCategorie = value;
                OnPropertyChanged(nameof(DeleteSelectedCategorie));
            }
        }

        public ObservableCollection<Acteur> Acteurs { get; init; } = [];
        public ObservableCollection<Realisateur> Realisateurs { get; init; } = [];
        public ObservableCollection<Film> Films { get; init; } = [];
        public ObservableCollection<Categories> Categories { get; init; } = [];
        public ObservableCollection<Categories> CategoriesPreferee
        {
            get => _categoriesPreferee;

            set
            {
                _categoriesPreferee = value;
                OnPropertyChanged(nameof(CategoriesPreferee));
            }
        }
        public ObservableCollection<Film> FilmsPreferee
        {
            get => _filmsPreferee;
            set
            {
                _filmsPreferee = value;
                OnPropertyChanged(nameof(FilmsPreferee));
            }
        }
        public ObservableCollection<Realisateur> RealisateursPreferee
        {
            get => _realisateursPreferee;

            set
            {
                _realisateursPreferee = value;
                OnPropertyChanged(nameof(RealisateursPreferee));
            }
        }
        public ObservableCollection<Acteur> ActeursPreferee
        {
            get => _acteursPreferee;
            set
            {
                _acteursPreferee = value;
                OnPropertyChanged(nameof(ActeursPreferee));
            }
        }



        public ListPreferencesViewModel(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository, IFilmService filmService, Abonne abonne)
        {
            _abonneService = abonneService;
            _realisateurRepository = realisateurRepository;
            _acteurRepository = acteurRepository;
            _filmService = filmService;
            _abonne = abonne;
            AddRealisateurCommand = new DelegateCommand(AddRealisateur);
            AddActeurCommand = new DelegateCommand(AddActeur);
            AddFilmCommand = new DelegateCommand(AddFilm);
            AddCategorieCommand = new DelegateCommand(AddCategorie);
            DeleteRealisateurCommand = new DelegateCommand(DeleteRealisateur);
            DeleteActeurCommand = new DelegateCommand(DeleteActeur);
            DeleteFilmCommand = new DelegateCommand(DeleteFilm);
            DeleteCategorieCommand = new DelegateCommand(DeleteCategorie);
        }



        public void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerActeurs();
            ChargerCategorie();
            ChargerFilms();
            ChargerRealisateurs();
            ChargerRealisateursPreferee();
            ChargerActeursPreferee();
            ChargerFilmsPreferee();
            ChargerCategoriesPreferee();
        }

        public void ChargerCategoriesPreferee()
        {
            CategoriesPreferee.Clear();
            foreach (var categorie in _abonne.CategoriesPrefere)
            {
                CategoriesPreferee.Add(categorie);
            }
        }

        public void ChargerFilmsPreferee()
        {
            FilmsPreferee.Clear();
            foreach (var film in _abonne.Films)
            {
                FilmsPreferee.Add(film);
            }
        }

        public void ChargerActeursPreferee()
        {
            ActeursPreferee.Clear();
            foreach (var acteur in _abonne.Acteurs)
            {
                ActeursPreferee.Add(acteur);
            }
        }

        public void ChargerRealisateursPreferee()
        {
            RealisateursPreferee.Clear();
            foreach (var realisateur in _abonne.Realisateurs)
            {
                RealisateursPreferee.Add(realisateur);
            }
        }

        private void ChargerCategorie()
        {
            Categories.Clear();
            foreach (var cat in Enum.GetValues(typeof(Categories)).Cast<Categories>())
            {
                Categories.Add(cat);
            }
        }

        public async void ChargerFilms()
        {
            Films.Clear();
            foreach (var film in await _filmService.GetAllFilms())
            {
                Films.Add(film);
            }
        }

        private async void ChargerRealisateurs()
        {
            Realisateurs.Clear();
            foreach (var realisateur in await _realisateurRepository.GetAll())
            {
                Realisateurs.Add(realisateur);
            }
        }

        private async void ChargerActeurs()
        {
            Acteurs.Clear();
            foreach (var acteur in await _acteurRepository.GetAll())
            {
                Acteurs.Add(acteur);
            }
        }
        public async void AddRealisateur()
        {
            try
            {
                if (SelectedRealisateur is null || SelectedRealisateur.Id == ObjectId.Empty)
                    throw new SelectedRealisateurNullException("Veuillez selectionner un réalisateur pour ajouter");

                await _abonneService.AddRealisateurInAbonne(_abonne, SelectedRealisateur);
                RealisateursPreferee.Add(SelectedRealisateur);
                SelectedRealisateur = null;
            }
            catch (Exception ex)
            {

                ErrorOccurred?.Invoke(ex.Message);
            }
        }
        public async void AddActeur()
        {
            try
            {
                if (SelectedActeur is null || SelectedActeur.Id == ObjectId.Empty)
                    throw new SelectedActeurNullException("Veuillez selectionner un acteur pour ajouter");

                await _abonneService.AddActeurInAbonne(_abonne, SelectedActeur);
                ActeursPreferee.Add(SelectedActeur);
                SelectedActeur = null;
            }
            catch (Exception ex)
            {

                ErrorOccurred?.Invoke(ex.Message);
            }
        }
        public async void AddFilm()
        {
            try
            {
                if (SelectedFilm is null || SelectedFilm.Id == ObjectId.Empty)
                    throw new SelectedFilmNullException("Veuillez selectionner un film pour ajouter");

                await _abonneService.AddFilmInAbonne(_abonne, SelectedFilm);
                FilmsPreferee.Add(SelectedFilm);
                SelectedFilm = null;
            }
            catch (Exception ex)
            {

                ErrorOccurred?.Invoke(ex.Message);
            }
        }
        public async void AddCategorie()
        {
            try
            {

                await _abonneService.AddCategorieInAbonne(_abonne, SelectedCategorie);
                CategoriesPreferee.Add(SelectedCategorie);
                SelectedCategorie = new();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
        public async void DeleteRealisateur()
        {
            try
            {
                await _abonneService.RemoveRealisateurInAbonne(_abonne, DeleteSelectedRealisateur);
                RealisateursPreferee.Remove(DeleteSelectedRealisateur);
                DeleteSelectedRealisateur = null;
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
        public async void DeleteActeur()
        {
            try
            {
                await _abonneService.RemoveActeurInAbonne(_abonne, DeleteSelectedActeur);
                ActeursPreferee.Remove(DeleteSelectedActeur);
                DeleteSelectedActeur = null;
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }

        public async void DeleteFilm()
        {
            try
            {
                await _abonneService.RemoveFilmInAbonne(_abonne, DeleteSelectedFilm);
                FilmsPreferee.Remove(DeleteSelectedFilm);
                DeleteSelectedFilm = null;
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
        public async void DeleteCategorie()
        {
            try
            {
                await _abonneService.RemoveCategorieInAbonne(_abonne, DeleteSelectedCategorie);
                CategoriesPreferee.Remove(DeleteSelectedCategorie);
                DeleteSelectedCategorie = new();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
    }
}