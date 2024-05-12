using CineQuebec.Windows.DAL.Data;
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

        public Film Film { get; set; }

        public FilmDetailsView()
        {
            InitializeComponent();
        }
        public FilmDetailsView(FilmDetailsView Film)
        {

        }

        private void NoteButton_Click(object sender, RoutedEventArgs e)
        {
            OuvrirFormNoter();
        }

        private void OuvrirFormNoter()
        {
            
        }
    }
}
