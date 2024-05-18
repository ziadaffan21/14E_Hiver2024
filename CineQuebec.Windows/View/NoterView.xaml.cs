using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.BLL.Tests;
using CineQuebec.Windows.DAL.Data;
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
using System.Windows.Shapes;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour NoterView.xaml
    /// </summary>
    public partial class NoterView : Window
    {
        private Film _film;

        private readonly NoterViewModel _viewModel;
        private INoteService _noteService;

        public Film Film
        {
            get { return _film; }
            set { _film = value; }
        }


        public NoterView(INoteService noteService, Film film)
        {
            InitializeComponent();
            _film = film;
            _noteService = noteService;
            _viewModel = new NoterViewModel(noteService, film,this);
            DataContext = _viewModel;
            Loaded += _viewModel.OnLoad;
            ((NoterViewModel)this.DataContext).SuccessMessage += HandleSuccess;
            this.Unloaded += FilmDetailsViewModel_Unloaded;
        }


        private void FilmDetailsViewModel_Unloaded(object sender, RoutedEventArgs e)
        {
            ((NoterViewModel)this.DataContext).SuccessMessage -= HandleSuccess;
        }

        private void HandleSuccess(string successMessage)
        {
            MessageBox.Show(successMessage, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        
    }
}
