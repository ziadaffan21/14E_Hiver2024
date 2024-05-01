using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
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

        private Abonne User { get; set; }
        public ListPreferencesView(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository, Abonne abonne = null)
        {
            InitializeComponent();
            _abonneService = abonneService;
            _realisateurRepository = realisateurRepository;
            _acteurRepository = acteurRepository;
            User = abonne;
            Loaded += ListPreferencesView_Loaded;
        }

        private async void ListPreferencesView_Loaded(object sender, RoutedEventArgs e)
        {
            cbActeurs.ItemsSource = await ChargerActeurs();
            cbRealisateurs.ItemsSource = await ChargerRealisateurs();
            lstRealisateurs.ItemsSource = User.Realisateurs;
            lstActeurs.ItemsSource = User.Acteurs;
        }
        private void InitializeListsAndComboBoxes()
        {
            lstRealisateurs.ItemsSource = User.Realisateurs;
            lstActeurs.ItemsSource = User.Acteurs;
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
            var selectedRealisateur = cbRealisateurs.SelectedItem as Realisateur;
            var result = await _abonneService.AddRealisateurInAbonne(User.Id, selectedRealisateur);
            if (result)
            {
                User = await _abonneService.GetAbonne(User.Id);
                cbRealisateurs.SelectedIndex = -1;
                InitializeListsAndComboBoxes();
            }
        }

        private async void btnAddActeur_Click(object sender, RoutedEventArgs e)
        {
            var selectedActeur = cbActeurs.SelectedItem as Acteur;
            var result = await _abonneService.AddActeurInAbonne(User.Id, selectedActeur);
            if (result)
            {
                User = await _abonneService.GetAbonne(User.Id);
                cbActeurs.SelectedIndex = -1;
                InitializeListsAndComboBoxes();
            }
        }

        private void btnAddCategorie_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
