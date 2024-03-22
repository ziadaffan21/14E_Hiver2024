using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Utils;
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
    /// Logique d'interaction pour DetailFilm.xaml
    /// </summary>
    public partial class DetailFilm : Window
    {

        private Film _film;
        private bool modification;
        private string message;

        public Film Film
        {
            get { return _film; }
            private set { _film = value; }
        }

        public DetailFilm(Film film=null)
        {
            InitializeComponent();
            Film= film;
            modification = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboCategories.ItemsSource = UtilEnum.GetAllDescriptions<Categories>();
            if (Film is null)
            {
                InitialiserFormulaireAjout();
            }
            else
            {
                InitialiserFormulaireVisualiser();
            }
       }

        private void InitialiserFormulaireAjout()
        {
            txtNom.Text = "";
            cboCategories.SelectedIndex = -1;
            txtNom.Focus();
            cboCategories.Focus();
            cboCategories.IsEnabled = true;
            txtNom.IsEnabled = true;
            btnModifier.Content = "Ajouter";
            btnOK.Content = "Annuler";
        }
        private void InitialiserFormulaireVisualiser()
        {
            txtNom.Text=Film.Titre;
            cboCategories.SelectedIndex = (int)Film.Categorie;
            txtNom.IsEnabled = false;
            cboCategories.IsEnabled = false;
            btnModifier.Content = "Modifier";
            btnOK.Content = "Ok";
        }

        private void InitialiserFormulaireModification()
        {
            txtNom.Text = Film.Titre;
            cboCategories.SelectedIndex = (int)Film.Categorie;
            txtNom.IsEnabled = true;
            cboCategories.IsEnabled = true;
            txtNom.Focus();
            cboCategories.Focus();
            btnModifier.Content = "Enregistrer";
            btnOK.Content = "Annuler";
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(!modification)
                DialogResult = true;
            else
            {
                modification = false;
                InitialiserFormulaireVisualiser();
            }
        }

        private bool ValiderForm()
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
                message += "Le nom du film ne peut pas être vide";
            if (cboCategories.SelectedIndex == -1)
                message += "\nVous devez assigner une catégorie";
            if (string.IsNullOrWhiteSpace(message))
                return true;
            else
                return false;
        }
        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                modification = !modification;
                if (modification)
                {
                    InitialiserFormulaireModification();
                }
                else
                {
                    if (ValiderForm())
                    {
                        Film.Titre = txtNom.Text;
                        Film.Categorie = (Categories)cboCategories.SelectedIndex;
                        GestionFilmAbonne.ModifierFilm(Film);
                        InitialiserFormulaireVisualiser();
                    }
                    else
                        MessageBox.Show(message,Resource.erreur,MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            catch(MongoDataConnectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception)
            {
                MessageBox.Show(Resource.erreurGenerique, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
