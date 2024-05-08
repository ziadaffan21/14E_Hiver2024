using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Ressources.i18n;
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
        public ICommand AddRealisateurCommand { get; init; }
        public Abonne _abonne { get; set; } = new();
        public ObservableCollection<Acteur> Acteurs { get; init; } = new();
        public ObservableCollection<Realisateur> Realisateurs { get; init; } = new();
        public ObservableCollection<Film> Films { get; init; } = new();
        public ObservableCollection<Categories> Categories { get; init; } = new();
        private ObservableCollection<Acteur> _acteursPreferee = new();
        private ObservableCollection<Realisateur> _realisateursPreferee = new();
        private ObservableCollection<Film> _filmsPreferee = new();
        private ObservableCollection<Categories> _categoriesPreferee = new();
        private Realisateur _selectedRealisateur = new();
        public Realisateur SelectedRealisateur
        {
            get => _selectedRealisateur;
            set
            {
                _selectedRealisateur = value;
                OnPropertyChanged(nameof(SelectedRealisateur));
            }
        }

        public ObservableCollection<Categories> CategoriesPreferee
        {
            get
            {
                foreach (var categorie in _abonne.CategoriesPrefere)
                {
                    _categoriesPreferee.Add(categorie);
                }
                return _categoriesPreferee;
            }
            set
            {
                _categoriesPreferee = value;
                OnPropertyChanged(nameof(CategoriesPreferee));
            }
        }
        public ObservableCollection<Film> FilmsPreferee
        {
            get
            {
                foreach (var film in _abonne.Films)
                {
                    _filmsPreferee.Add(film);
                }
                return _filmsPreferee;
            }
            set
            {
                _filmsPreferee = value;
                OnPropertyChanged(nameof(FilmsPreferee));
            }
        }
        public ObservableCollection<Realisateur> RealisateursPreferee
        {
            get
            {
                return _realisateursPreferee;
            }
            set
            {
                _realisateursPreferee = value;
                OnPropertyChanged(nameof(RealisateursPreferee));
            }
        }
        public ObservableCollection<Acteur> ActeursPreferee
        {
            get
            {
                foreach (var acteur in _abonne.Acteurs)
                {
                    _acteursPreferee.Add(acteur);
                }
                return _acteursPreferee;
            }
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
        }

        public void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerActeurs();
            ChargerCategorie();
            ChargerFilms();
            ChargerRealisateurs();
            ChargerRealisateursPreferee();
        }

        private void ChargerRealisateursPreferee()
        {
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

        private async void ChargerFilms()
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
                if(SelectedRealisateur != null && SelectedRealisateur.Id != ObjectId.Empty)
                {
                    await _abonneService.AddRealisateurInAbonne(_abonne, SelectedRealisateur);
                    RealisateursPreferee.Add(SelectedRealisateur);
                    SelectedRealisateur = null;
                }
                else
                {
                    throw new SelectedRealisateurNullException("Veuillez selectionner un réalisateur pour ajouter");
                }

            }
            catch (Exception ex)
            {

                ErrorOccurred?.Invoke(ex.Message);
            }
        }
    }
}