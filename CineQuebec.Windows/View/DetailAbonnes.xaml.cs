using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Logique d'interaction pour DetailAbonnes.xaml
    /// </summary>
    public partial class DetailAbonnes : Window
    {
        private readonly DetailAbonnesViewModel _viewModel;

        private IProjectionService _projectionService;
        public DetailAbonnes(Abonne abonne,IProjectionService projectionService)
        {
            _viewModel = new DetailAbonnesViewModel(abonne,projectionService);
            DataContext = _viewModel;
            InitializeComponent();
            Loaded += _viewModel.Load;
            _viewModel.OuvrirRecompense += view_ouvrir_recompense;
            Unloaded += DetailAbonnes_Unloaded;
        }

        private void DetailAbonnes_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.OuvrirRecompense-=view_ouvrir_recompense;
        }

        private void view_ouvrir_recompense(bool result)
        {
            if (result)
                DialogResult = true;

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
