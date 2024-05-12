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

namespace CineQuebec.Windows.ViewModel
{
    public class FormulaireFilmViewModel : PropertyNotifier
    {
        private OberservableFilm _film;
        private IEventAggregator _eventAggregator;
        private string[] _descriptions;
        public ICommand SaveCommand { get; init; }
        public OberservableFilm Film
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

        private async void Ajout()
        {
            try
            {
                await _filmService.AjouterFilm(Film.value());
                MessageBox.Show(Resource.ajoutReussi, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                _eventAggregator.GetEvent<AddModifierFilmEvent>().Publish(Film.value());

            }
            catch (ExistingFilmException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MongoDataConnectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void Save()
        {
            try
            {
                await _filmService.ModifierFilm(Film.value());
                MessageBox.Show(Resource.modificationReussi, Resource.modification, MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (ExistingFilmException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MongoDataConnectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
