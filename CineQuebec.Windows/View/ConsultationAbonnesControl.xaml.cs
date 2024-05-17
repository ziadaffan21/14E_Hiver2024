using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ConsultationAbonnesControl.xaml
    /// </summary>
    public partial class ConsultationAbonnesControl : UserControl
    {
        private readonly ConsultationAbonnesViewModel _viewModel;

        public ConsultationAbonnesControl(IAbonneService abonneService)
        {
            _viewModel=new ConsultationAbonnesViewModel(abonneService);
            DataContext = _viewModel;

            Loaded += _viewModel.Load;
            InitializeComponent();
        }

        private void lstUtilisisateurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstUtilisisateurs.SelectedIndex = -1;
        }
    }
}