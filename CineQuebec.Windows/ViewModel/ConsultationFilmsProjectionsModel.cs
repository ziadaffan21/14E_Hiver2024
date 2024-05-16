using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.EntitysExceptions;
using CineQuebec.Windows.ViewModel.Event;
using CineQuebec.Windows.ViewModel.ObservableClass;
using MongoDB.Bson;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineQuebec.Windows.ViewModel
{
    public class ConsultationFilmsProjectionsModel:PropertyNotifier
    {
        public ObservableCollection<Film> Films { get; init; } =new();
        public ObservableCollection<Projection> Projections { get; init; }=new();
        private readonly IFilmService _filmService;
        private readonly IProjectionService _projectionService;
        public Action<string> ErrorOccured;


        public ConsultationFilmsProjectionsModel(IFilmService filmService,IProjectionService projectionService, IEventAggregator eventAggregator)
        {
            _filmService = filmService;
            _projectionService = projectionService;
            eventAggregator.GetEvent<AddModifierFilmEvent>().Subscribe(Films.Add);
            eventAggregator.GetEvent<AddModifierProjectionEvent>().Subscribe(Projections.Add);

        }

        internal async void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                Films.Clear();
                Projections.Clear();

                foreach (Film film in await _filmService.GetAllFilms())
                {
                    Films.Add(film);
                }

                foreach (Projection projection in _projectionService.GetAllProjections())
                {
                    Projections.Add(projection);
                }
            }
            catch (Exception ex)
            {

                ErrorOccured?.Invoke(ex.Message);
            }


        }

        internal async void LoadProjectionFilm(ObjectId id)
        {
            try
            {
                if (!ObjectId.TryParse(id.ToString(), out _)) throw new InvalidGuidException($"L'id {id} est invalid");
                Projections.Clear();
                foreach (Projection projection in await _projectionService.GetProjectionsById(id))
                {
                    Projections.Add(projection);
                }
            }
            catch (Exception ex)
            {

                ErrorOccured?.Invoke(ex.Message);
            }

        }
    }
}
