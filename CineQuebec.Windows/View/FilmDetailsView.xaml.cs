using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Data;
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

        private Film _film;
        private INoteService _noteService;

        public Film Film
        {
            get { return _film; }
            set { _film = value; }
        }


        public FilmDetailsView(INoteService noteService, Film film)
        {
            Film = film;
            InitializeComponent();
            _noteService = noteService;
        }

        private void NoteButton_Click(object sender, RoutedEventArgs e)
        {
            OuvrirFormNoter();
        }

        private void OuvrirFormNoter()
        {
            NoterView noterView = new NoterView(_noteService, Film.Id);
            noterView.Show();
        }
    }
}
