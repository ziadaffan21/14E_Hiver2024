using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.Services;
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
    /// Logique d'interaction pour UserControlActeursView.xaml
    /// </summary>
    public partial class ActeursPrefereeUserControl : UserControl
    {
        private ActeursPrefereeUserControlModel _viewModel;

        public ActeursPrefereeUserControl(IAbonneService abonneService, IActeurRepository acteurRepository, Abonne abonne = null)
        {
            InitializeComponent();
            _viewModel = new ActeursPrefereeUserControlModel(abonneService, acteurRepository, abonne);
            DataContext = _viewModel;
            Loaded += _viewModel.Loaded;
            ((ActeursPrefereeUserControlModel)this.DataContext).ErrorOccurred += HandleError;
            this.Unloaded += UserControlActeurs_Unloaded;
        }

        private void UserControlActeurs_Unloaded(object sender, RoutedEventArgs e)
        {
            ((ActeursPrefereeUserControlModel)this.DataContext).ErrorOccurred -= HandleError;
        }

        private void HandleError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
