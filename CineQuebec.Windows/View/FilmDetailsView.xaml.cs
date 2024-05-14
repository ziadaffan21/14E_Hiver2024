using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.ViewModel;
using CineQuebec.Windows.ViewModel.ObservableClass;
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
    /// Logique d'interaction pour FilmDetailsView.xaml
    /// </summary>
    public partial class FilmDetailsView : Window
    {
        private readonly FilmDetailsViewModel _viewModel;
        private Film _film;
        private INoteService _noteService;



        public FilmDetailsView(INoteService noteService, Film film)
        {
            InitializeComponent();
            _film = film;
            _noteService = noteService;
            _viewModel = new FilmDetailsViewModel(noteService, film);
            DataContext = _viewModel;
            Loaded += _viewModel.OnLoad;
            ((FilmDetailsViewModel)this.DataContext).SuccessMessage += HandleSuccess;
            this.Unloaded += FilmDetailsViewModel_Unloaded;
        }

        private void FilmDetailsViewModel_Unloaded(object sender, RoutedEventArgs e)
        {
            ((FilmDetailsViewModel)this.DataContext).SuccessMessage -= HandleSuccess;
        }

        private void HandleSuccess(string successMessage)
        {
            MessageBox.Show(successMessage, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
