using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class ActeursPrefereeUserControlModel : PropertyNotifier
    {
        private IAbonneService _abonneService;
        private IActeurRepository _acteurRepository;
        private Abonne _abonne = new();
        public event Action<string> ErrorOccurred;
        private ObservableCollection<Acteur> _acteursPreferee = [];
        private Acteur _selectedActeur = new();
        private Acteur _deleteSelectedActeur = new();
        public ICommand AddActeurCommand { get; init; }
        public ICommand DeleteActeurCommand { get; init; }
        public ObservableCollection<Acteur> Acteurs { get; init; } = [];

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
                OnPropertyChanged(nameof(DeleteSelectedActeur));
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

        public ActeursPrefereeUserControlModel(IAbonneService abonneService, IActeurRepository acteurRepository, Abonne abonne)
        {
            _abonneService = abonneService;
            _acteurRepository = acteurRepository;
            _abonne = abonne;
            AddActeurCommand = new DelegateCommand(AddActeur);
            DeleteActeurCommand = new DelegateCommand(DeleteActeur);

        }

        internal void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerActeurs();
            ChargerActeursPreferee();

        }
        private async void ChargerActeurs()
        {
            Acteurs.Clear();
            foreach (var acteur in await _acteurRepository.GetAll())
            {
                Acteurs.Add(acteur);
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
        public async void DeleteActeur()
        {
            try
            {
                if (DeleteSelectedActeur is null || DeleteSelectedActeur.Id == ObjectId.Empty)
                    throw new SelectedActeurNullException("Veuillez selectionner un acteur pour enlever de la liste");

                await _abonneService.RemoveActeurInAbonne(_abonne, DeleteSelectedActeur);
                ActeursPreferee.Remove(DeleteSelectedActeur);
                DeleteSelectedActeur = null;
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
    }
}