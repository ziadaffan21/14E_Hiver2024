using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
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
        private readonly IFilmService _filmService;

        public DetailFilm(IFilmService filmService,Film film=null)
        {
            InitializeComponent();
            _filmService = filmService;
            _film= film;
            modification = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboCategories.ItemsSource = UtilEnum.GetAllDescriptions<Categories>();
            if (_film is null)
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
            lblTitre.Text = "Ajouter un film";
            btnModifier.Content = "Ajouter";
            btnOK.Content = "Annuler";
        }
        private void InitialiserFormulaireVisualiser()
        {
            txtNom.Text=_film.Titre;
            cboCategories.SelectedIndex = (int)_film.Categorie;
            txtNom.IsEnabled = false;
            cboCategories.IsEnabled = false;
            btnModifier.Content = "Modifier";
            btnOK.Content = "Ok";
        }

        private void InitialiserFormulaireModification()
        {
            txtNom.Text = _film.Titre;
            cboCategories.SelectedIndex = (int)_film.Categorie;
            txtNom.IsEnabled = true;
            cboCategories.IsEnabled = true;
            txtNom.Focus();
            cboCategories.Focus();
            btnModifier.Content = "Enregistrer";
            btnOK.Content = "Annuler";
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (_film is null)
            {
                DialogResult = false;
                return;
            }
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
            if (txtNom.Text.Trim().Length < Film.NB_MIN_CARACTERES_USERNAME || txtNom.Text.Trim().Length > Film.NB_MAX_CARACTERES_USERNAME)
                message += $"\nLe titre doit etre entre {Film.NB_MIN_CARACTERES_USERNAME} et {Film.NB_MAX_CARACTERES_USERNAME} caractères.";
            if (cboCategories.SelectedIndex == -1)
                message += "\nVous devez assigner une catégorie";
            if (string.IsNullOrWhiteSpace(message))
                return true;
            else
                return false;
        }
        private async void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_film is null && ValiderForm())
                {
                    _film = new Film(txtNom.Text, (Categories)cboCategories.SelectedIndex);
                    //await GestionFilmAbonne.AjouterFilm(_film);
                    await _filmService.AjouterFilm(_film);
                    InitialiserFormulaireVisualiser();
                    MessageBox.Show(Resource.ajoutReussi, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                modification = !modification;
                if (modification)
                {
                    InitialiserFormulaireModification();
                }
                else
                {
                    if (ValiderForm())
                    {
                        _film.Titre = txtNom.Text;
                        _film.Categorie = (Categories)cboCategories.SelectedIndex;
                        //  await GestionFilmAbonne.ModifierFilm(_film);
                        await _filmService.ModifierFilm(_film);
                        InitialiserFormulaireVisualiser();
                        MessageBox.Show(Resource.modificationReussi, Resource.modification, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show(message,Resource.erreur,MessageBoxButton.OK,MessageBoxImage.Error);
                    message = "";
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
