using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.DAL.Exceptions.FilmExceptions;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.ViewModel.ObservableClass;
using Prism.Events;
using CineQuebec.Windows.ViewModel.Event;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;

namespace CineQuebec.Windows.ViewModel
{
    public class FormulaireFilmViewModel : PropertyNotifier
    {
        private ObservableFilm _film;
        private IEventAggregator _eventAggregator;
        private string[] _descriptions;
        public ICommand SaveCommand { get; init; }
        public ObservableFilm Film
        {
            get { return _film; }
            set
            {
                _film = value;
                OnPropertyChanged();
            }

        }

        public string[] Descriptions
        {
            get { return _descriptions; }
            set { _descriptions = value; OnPropertyChanged(); }
        }

        public Action<bool> AjoutModif;
        public Action<string> errorOccured;


        private readonly IFilmService _filmService;


        public FormulaireFilmViewModel(IFilmService filmService, IEventAggregator eventAggregator,Film film = null)
        {
            _filmService = filmService;
            Film = new(film);
            _eventAggregator = eventAggregator;
            Film.PropertyChanged += ReEvaluateButtonState;
            Descriptions = UtilEnum.GetAllDescriptions<Categories>();
            SaveCommand = film is null ? new DelegateCommand(Ajout, CanSave) : new DelegateCommand(Save, CanSave);
        }

        public async void Ajout()
        {
            try
            {
                ValiderForm();
                await _filmService.AjouterFilm(Film.value());
                AjoutModif?.Invoke(true);
                //MessageBox.Show(Resource.ajoutReussi, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                _eventAggregator.GetEvent<AddModifierFilmEvent>().Publish(Film.value());

            }
            catch (Exception ex)
            {
                errorOccured?.Invoke(ex.Message);
            }

        }

        private void ReEvaluateButtonState(object sender, PropertyChangedEventArgs e)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool CanSave()
        {

            return Film.IsValid();

        }

        public async void Save()
        {
            try
            {
                ValiderForm();
                await _filmService.ModifierFilm(Film.value());
                AjoutModif?.Invoke(false);

            }
            catch (Exception ex)
            {
                errorOccured?.Invoke(ex.Message);
            }
        }

        public void ValiderForm()
        {

            if (string.IsNullOrWhiteSpace(Film.Titre.Trim()) || Film.Titre.Trim().Length > 100 || Film.Titre.Trim().Length <2)
                throw new TitreNullException("Le titre doit etre entre 2 et 100 caractères.");
            if (Film.IndexCategorie == -1)
                throw new CategorieUndefinedException("La catégorie doit etre définie");
            if (Film.Duree <= 30)
                throw new ArgumentOutOfRangeException("La durée du film doit etre plus grande que 30 minutes.");
        }
    }
}
