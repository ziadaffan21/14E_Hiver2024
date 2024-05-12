using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ListPreferencesView.xaml
    /// </summary>
    public partial class ListPreferencesView : Window
    {
        private readonly ListPreferencesViewModel _viewModel;

        public ListPreferencesView(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository, IFilmService filmService, Abonne abonne = null)
        {
            InitializeComponent();
            _viewModel = new ListPreferencesViewModel(abonneService, realisateurRepository, acteurRepository, filmService, abonne);
            DataContext = _viewModel;
            Loaded += _viewModel.Loaded;
            ((ListPreferencesViewModel)this.DataContext).ErrorOccurred += HandleError;
            Unloaded += ListPreferencesView_Unloaded;
        }

        private void ListPreferencesView_Unloaded(object sender, RoutedEventArgs e)
        {
            ((ListPreferencesViewModel)this.DataContext).ErrorOccurred -= HandleError;
        }

        private void HandleError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}