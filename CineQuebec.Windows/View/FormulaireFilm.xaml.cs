using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.FilmExceptions;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
using Prism.Events;
using System.Text;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour DetailFilm.xaml
    /// </summary>
    public partial class DetailFilm : Window
    {
        private Etat Etat;

        private readonly FormulaireFilmViewModel _viewModel;

        public DetailFilm(IFilmService filmService,IEventAggregator eventAggregator, Film film = null)
        {
            InitializeComponent();
            _viewModel = new FormulaireFilmViewModel(filmService,eventAggregator, film);
            DataContext = _viewModel;
            Etat = film is null? Etat.Ajouter:Etat.Modifier;
            _viewModel.errorOccured += Viewmodel_error;
            _viewModel.AjoutModif += Viewmodel_ajoutmodif;
            Unloaded += DetailFilm_Unloaded;
        }

        private void DetailFilm_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.errorOccured -= Viewmodel_error;
            _viewModel.AjoutModif -= Viewmodel_ajoutmodif;
        }

        private void Viewmodel_ajoutmodif(bool etat)
        {
            if(etat)
                MessageBox.Show(Resource.ajoutReussi, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(Resource.modificationReussi, Resource.modification, MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult=true;
        }

        private void Viewmodel_error(string error)
        {
            MessageBox.Show(error, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateSortie.DisplayDate = DateTime.Now;
            InitialiserFormulaire();
        }

        private void InitialiserFormulaire()
        {
            switch (Etat)
            {
                case Etat.Ajouter:
                    lblTitre.Text = "Ajouter un film";
                    btnAjouterModifier.Content = "Ajouter";
                    break;

                case Etat.Modifier:
                    lblTitre.Text = "Modifier un film";
                    btnAjouterModifier.Content = "Modifier";
                    break;

                default:
                    DialogResult=false;
                    break;
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}