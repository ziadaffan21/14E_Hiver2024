using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Unity.Processors;

namespace CineQuebec.Windows.ViewModel
{
    internal class UserControlRealisateursViewModel : PropertyNotifier
    {
        private IAbonneService _abonneService;
        private IRealisateurRepository _realisateurRepository;
        public ICommand AddRealisateurCommand { get; init; }
        public ICommand DeleteRealisateurCommand { get; init; }
        private ObservableCollection<Realisateur> _realisateursPreferee = [];
        private Realisateur _selectedRealisateur = new();
        private Realisateur _deleteSelectedRealisateur = new();
        public Abonne _abonne { get; set; } = new();
        public event Action<string> ErrorOccurred;
        public ObservableCollection<Realisateur> Realisateurs { get; init; } = [];

        public ObservableCollection<Realisateur> RealisateursPreferee
        {
            get => _realisateursPreferee;

            set
            {
                _realisateursPreferee = value;
                OnPropertyChanged(nameof(RealisateursPreferee));
            }
        }
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

        public UserControlRealisateursViewModel(IAbonneService abonneService, IRealisateurRepository realisateurRepository, Abonne abonne = null)
        {
            _abonne = abonne;
            _abonneService = abonneService;
            _realisateurRepository = realisateurRepository;
            AddRealisateurCommand = new DelegateCommand(AddRealisateur);
            DeleteRealisateurCommand = new DelegateCommand(DeleteRealisateur);

        }

        internal void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerRealisateurs();
            ChargerRealisateursPreferee();
        }
        public void ChargerRealisateursPreferee()
        {
            RealisateursPreferee.Clear();
            foreach (var realisateur in _abonne.Realisateurs)
            {
                RealisateursPreferee.Add(realisateur);
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
        public async void DeleteRealisateur()
        {
            try
            {
                if (SelectedRealisateur is null || SelectedRealisateur.Id == ObjectId.Empty)
                    throw new SelectedRealisateurNullException("Veuillez selectionner un réalisateur pour enlever de la liste");

                await _abonneService.RemoveRealisateurInAbonne(_abonne, DeleteSelectedRealisateur);
                RealisateursPreferee.Remove(DeleteSelectedRealisateur);
                DeleteSelectedRealisateur = null;
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
    }
}