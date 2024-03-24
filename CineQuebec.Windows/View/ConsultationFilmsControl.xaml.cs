using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
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
    /// Logique d'interaction pour ConsultationFilmsControl.xaml
    /// </summary>
    public partial class ConsultationFilmsControl : UserControl
    {
        public ConsultationFilmsControl()
        {
            try
            {
                InitializeComponent();
                lstFilms.ItemsSource = GestionFilmAbonne.ReadFilms();
            }
            catch(MongoDataConnectionException err) {
                MessageBox.Show(err.Message, Resource.erreur, MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(Resource.erreurGenerique, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


     

        /// <summary>
        /// Événement lancé lors de lu double click d'un élément dans la liste des films
        /// </summary>
        /// <param name="sender">ListBox contenant les films</param>
        /// <param name="e"></param>
        private void lstFilm_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            AfficherDetailsFilm();
        }

        private void AfficherDetailsFilm()
        {
            if (lstFilms.SelectedItem != null)
            {
                Film film =lstFilms.SelectedItem as Film;
                DetailFilm detailFilm = new DetailFilm(film);
                if ((bool)detailFilm.ShowDialog())
                {
                    lstFilms.ItemsSource = GestionFilmAbonne.ReadFilms();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DetailFilm detailFilm = new DetailFilm();
            if ((bool)detailFilm.ShowDialog())
            {
                lstFilms.ItemsSource = GestionFilmAbonne.ReadFilms();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AjoutDetailProjection detailProjection = new AjoutDetailProjection();
            if ((bool)detailProjection.ShowDialog())
                MessageBox.Show("POIRRRR");
        }
    }
}
