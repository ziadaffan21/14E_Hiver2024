using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    internal class FilmsPrefereeUserControlModel : PropertyNotifier
    {
        private IAbonneService _abonneService;
        private IFilmService _filmService;
        private Abonne _abonne { get; set; } = new();
        private ObservableCollection<Film> _filmsPreferee = [];
        private Film _selectedFilm = new();
        private Film _deleteSelectedFilm = new();

        public event Action<string> ErrorOccurred;
        public ICommand AddFilmCommand { get; init; }
        public ICommand DeleteFilmCommand { get; init; }
        public ObservableCollection<Film> Films { get; init; } = [];
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
        public ObservableCollection<Film> FilmsPreferee
        {
            get => _filmsPreferee;
            set
            {
                _filmsPreferee = value;
                OnPropertyChanged(nameof(FilmsPreferee));
            }
        }

        public FilmsPrefereeUserControlModel(IAbonneService abonneService, IFilmService filmService, Abonne abonne)
        {
            _abonneService = abonneService;
            _filmService = filmService;
            _abonne = abonne;
            AddFilmCommand = new DelegateCommand(AddFilm);
            DeleteFilmCommand = new DelegateCommand(DeleteFilm);

        }

        internal void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerFilms();
            ChargerFilmsPreferee();
        }
        public async void ChargerFilms()
        {
            Films.Clear();
            foreach (var film in await _filmService.GetAllFilms())
            {
                Films.Add(film);
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
        public async void DeleteFilm()
        {
            try
            {
                if (DeleteSelectedFilm is null || DeleteSelectedFilm.Id == ObjectId.Empty)
                    throw new SelectedFilmNullException("Veuillez selectionner un film pour l'enlever de la liste");
                await _abonneService.RemoveFilmInAbonne(_abonne, DeleteSelectedFilm);
                FilmsPreferee.Remove(DeleteSelectedFilm);
                DeleteSelectedFilm = null;
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
    }
}