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

        public ListPreferencesView(IAbonneService abonneService, IRealisateurRepository realisateurRepository, IActeurRepository acteurRepository, IFilmService filmService, Abonne abonne = null)
        {
            InitializeComponent();
            UserControlRealisateurContent.Content = new RealisateursPrefereeUserControl(abonneService, realisateurRepository, abonne);
            UserControlActeurContent.Content = new ActeursPrefereeUserControl(abonneService, acteurRepository, abonne);
            UserControlFilmContent.Content = new FilmsPrefereeUserControl(abonneService, filmService, abonne);
            UserControlCategorieContent.Content = new CategoriePrefereeUserControl(abonneService, abonne);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}