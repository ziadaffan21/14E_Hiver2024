using CineQuebec.Windows.DAL.Data;
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
        private readonly IProjectionService _projectionService;

        public ConsultationAbonnesControl(IAbonneService abonneService, IProjectionService projectionService)
        {
            _viewModel = new ConsultationAbonnesViewModel(abonneService);
            DataContext = _viewModel;

            Loaded += _viewModel.Load;
            InitializeComponent();
            _projectionService = projectionService;
        }

        private void lstUtilisisateurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lstUtilisisateurs.SelectedIndex = -1;
        }

        private void lstUtilisisateurs_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstUtilisisateurs.SelectedIndex != -1)
            {
                Abonne abonne=lstUtilisisateurs.SelectedItem as Abonne;
                DetailAbonnes detailAbonnes=new DetailAbonnes(abonne, _projectionService);
                if ((bool)detailAbonnes.ShowDialog())
                {
                    Recompense recompense = new Recompense();
                    recompense.ShowDialog();
                }
            }
        }
    }
}