using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.ViewModel;
using CineQuebec.Windows.ViewModel.ObservableClass;
using MongoDB.Bson;
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
        private NoterFilmViewModel _viewModele;

        public NoterView(INoteService noteService, ObjectId idFilm)
        {
            InitializeComponent();
            _viewModele = new NoterFilmViewModel(noteService, idFilm);
            DataContext = _viewModele;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
