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
    public class FormulaireProjectionViewModel:PropertyNotifier
    {
        private ObservableProjection _projection;
        private List<Film> _films;
        private IEventAggregator _eventAggregator;
        public ICommand SaveCommand { get; init; }
        public Action<string> ErrorOcuured;
        public Action<bool> AjoutModif;
        public ObservableProjection Projection {
            get { return _projection; } 
            set { _projection = value;
                OnPropertyChanged();
            }
        
        }

        public List<Film > Films { get { return _films; } private set { _films = value;  OnPropertyChanged(); } }

        private readonly IProjectionService _projectionService;

        private readonly IFilmService _filmService;


        public FormulaireProjectionViewModel(IProjectionService projectionService, IFilmService filmService,IEventAggregator eventAggregator)
        {
            _projectionService = projectionService;
            _filmService = filmService;
            _eventAggregator = eventAggregator;
            Projection = new();
            Projection.PropertyChanged += ReEvaluateButtonState;
            SaveCommand = new DelegateCommand(Save, CanSave);            
            GetAllFIlm();
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

        private void Save()
        {
            try
            {
                _projectionService.AjouterProjection(Projection.value());
                _eventAggregator.GetEvent<AddModifierProjectionEvent>().Publish(Projection.value());
                AjoutModif?.Invoke(true);

            }
            catch (ExistingProjectionException ex)
            {
                ErrorOcuured?.Invoke(ex.Message);
            }
            
        }
    }
}
