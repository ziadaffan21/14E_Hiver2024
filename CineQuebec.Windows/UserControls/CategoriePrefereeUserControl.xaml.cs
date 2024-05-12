using CineQuebec.Windows.DAL.Data;
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
    /// Logique d'interaction pour UserControlCategorieView.xaml
    /// </summary>
    public partial class CategoriePrefereeUserControl : UserControl
    {
        private CategoriePrefereeUserControlModel _viewModel;

        public CategoriePrefereeUserControl(IAbonneService abonneService, Abonne abonne = null)
        {
            InitializeComponent();
            _viewModel = new CategoriePrefereeUserControlModel(abonneService, abonne);
            DataContext = _viewModel;
            Loaded += _viewModel.Loaded;
            ((CategoriePrefereeUserControlModel)this.DataContext).ErrorOccurred += HandleError;
            this.Unloaded += UserControlFilms_Unloaded;
        }

        private void HandleError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserControlFilms_Unloaded(object sender, RoutedEventArgs e)
        {
            ((CategoriePrefereeUserControlModel)this.DataContext).ErrorOccurred -= HandleError;

        }
    }
}
