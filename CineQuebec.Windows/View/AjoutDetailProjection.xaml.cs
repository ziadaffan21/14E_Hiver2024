using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Logique d'interaction pour AjoutDetailProjection.xaml
    /// </summary>
    public partial class AjoutDetailProjection : Window
    {
        private Projection _projection;
        private string message;
        private readonly DatabasePeleMele databasePeleMele = new DatabasePeleMele();


        public AjoutDetailProjection()
        {
            InitializeComponent();
            _projection = new Projection();
            DataContext = _projection;
            
        }

        private async void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValiderForm(DateTime.Today))
                {
                    await GestionFilmAbonne.AjouterProjection(_projection);
                    MessageBox.Show(Resource.ajoutReussiProjection, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show(message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
                    message = "";
                }
            }
            catch (MongoDataConnectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(Resource.erreurGenerique, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool ValiderForm(DateTime dateAudjourdhui)
        {
            if(calendrier.SelectedDate is null ||  calendrier.SelectedDate < dateAudjourdhui)
            {
                message += $"\nLa date sélectionnée doit être plus grande ou égale à {dateAudjourdhui}";
            }
            if (string.IsNullOrWhiteSpace(txtPlace.Text))
                message += "\nLe nombre de place ne peut pas être vide";
            if (!int.TryParse(txtPlace.Text.Trim(),out _))
                message += $"\nLe nombre de place doit être un nombre";
            else
            {
                if (int.Parse(txtPlace.Text.Trim()) < 0)
                    message += "\nLe nombre de place ne peut pas être inférieur à 0";
            }
            if (cboCategories.SelectedIndex == -1)
                message += "\nVous devez assigner un film";
            if (string.IsNullOrWhiteSpace(message))
                return true;
            else
                return false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiserFormulaireAjout();
        }

        private void InitialiserFormulaireAjout()
        {
            cboCategories.ItemsSource = GestionFilmAbonne.ReadFilms();
            cboCategories.Focus();
            txtPlace.Focus();
                        
        }

        private void cboCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboCategories.SelectedIndex != -1)
            {
                Film film = cboCategories.SelectedItem as Film;
                if (film != null)
                {
                    _projection.IdFilm=film.Id;
                }
            }
        }
    }
}
