using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel.Event;
using CineQuebec.Windows.ViewModel.ObservableClass;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class FormulaireProjectionViewModel : PropertyNotifier
    {
        private ObservableProjection _projection;
        private List<Film> _films;
        private IEventAggregator _eventAggregator;
        public ICommand SaveCommand { get; init; }
        public Action<string> ErrorOcuured;
        public Action<bool> AjoutModif;
        public ObservableProjection Projection
        {
            get { return _projection; }
            set
            {
                _projection = value;
                OnPropertyChanged();
            }

        }


        private DateTime _projectionTime;

        /// <summary>
        /// Représente le temps de la projection
        /// </summary>
        public DateTime ProjectionTime
        {
            get { return _projectionTime; }
            set
            {
                _projectionTime = value;
                OnPropertyChanged();
            }
        }


        public List<Film> Films { get { return _films; } private set { _films = value; OnPropertyChanged(); } }

        private readonly IProjectionService _projectionService;

        private readonly IFilmService _filmService;


        public FormulaireProjectionViewModel(IProjectionService projectionService, IFilmService filmService, IEventAggregator eventAggregator, Projection projection = null)
        {
            _projectionService = projectionService;
            _filmService = filmService;
            _eventAggregator = eventAggregator;
            Projection = new(projection);
            Projection.PropertyChanged += ReEvaluateButtonState;
            SaveCommand = projection is null ? new DelegateCommand(Ajout, CanSave) : new DelegateCommand(Save, CanSave);
            GetAllFIlm();


            //On inialise l'horloge à maintenant.
            ProjectionTime = DateTime.Now;
        }

        public void Save()
        {
            try
            {
                //Au moment de la sauvegarde, on change l'heure de la projection pour celle dans ProjectionTime
                var date = Projection.Date;
                var time = ProjectionTime.TimeOfDay;
                Projection.Date = new(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);


                _projectionService.ModifierProjection(Projection.Value());
                _eventAggregator.GetEvent<AddModifierProjectionEvent>().Publish(Projection.Value());
                AjoutModif?.Invoke(true);

            }
            catch (ExistingProjectionException ex)
            {
                ErrorOcuured?.Invoke(ex.Message);
            }
        }

        private void ReEvaluateButtonState(object sender, PropertyChangedEventArgs e)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public async void GetAllFIlm()
        {
            try
            {
                Films = await _filmService.GetAllFilms();

            }
            catch (Exception ex)
            {
                ErrorOcuured?.Invoke(ex.Message);
            }
        }

        private bool CanSave()
        {
            return Projection.IsValid();
        }

        public void Ajout()
        {
            try
            {
                //Au moment de la sauvegarde, on change l'heure de la projection pour celle dans ProjectionTime
                var date = Projection.Date;
                var time = ProjectionTime.TimeOfDay;
                Projection.Date = new(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);


                _projectionService.AjouterProjection(Projection.Value());
                _eventAggregator.GetEvent<AddModifierProjectionEvent>().Publish(Projection.Value());
                AjoutModif?.Invoke(true);

            }
            catch (ExistingProjectionException ex)
            {
                ErrorOcuured?.Invoke(ex.Message);
            }

        }
    }
}
