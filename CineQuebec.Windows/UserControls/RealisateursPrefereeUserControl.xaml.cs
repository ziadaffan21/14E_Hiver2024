using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour UserControlRealisateurs.xaml
    /// </summary>
    public partial class RealisateursPrefereeUserControl : UserControl
    {
        private RealisateursPrefereeUserControlModel _viewModel;

        public RealisateursPrefereeUserControl(IAbonneService abonneService, IRealisateurRepository realisateurRepository, Abonne abonne = null)
        {
            InitializeComponent();
            _viewModel = new RealisateursPrefereeUserControlModel(abonneService, realisateurRepository, abonne);
            DataContext = _viewModel;
            Loaded += _viewModel.Loaded;
            ((RealisateursPrefereeUserControlModel)this.DataContext).ErrorOccurred += HandleError;
            this.Unloaded += UserControlRealisateurs_Unloaded;
        }

        private void HandleError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserControlRealisateurs_Unloaded(object sender, RoutedEventArgs e)
        {
            ((RealisateursPrefereeUserControlModel)this.DataContext).ErrorOccurred -= HandleError;
        }
    }
}
