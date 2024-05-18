using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.EntitysExceptions;
using CineQuebec.Windows.View;
using CineQuebec.Windows.ViewModel.Event;
using CineQuebec.Windows.ViewModel.ObservableClass;
using MongoDB.Bson;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class ConsultationFilmsProjectionsModel:PropertyNotifier
    {
        public ObservableCollection<Film> Films { get; init; }
        public ObservableCollection<Projection> Projections { get; init; }
        public Film SelectedFilm { get; init; }
        public Projection SelectedProjection { get; init; }

        private readonly IFilmService _filmService;
        private readonly IProjectionService _projectionService;
        public Action<string> ErrorOccured;


        public ConsultationFilmsProjectionsModel(IFilmService filmService,IProjectionService projectionService, IEventAggregator eventAggregator)
        {
            _filmService = filmService;
            _projectionService = projectionService;
            Films = new ObservableCollection<Film>();
            Projections = new ObservableCollection<Projection>();
            eventAggregator.GetEvent<AddModifierFilmEvent>().Subscribe(Films.Add);
            eventAggregator.GetEvent<AddModifierProjectionEvent>().Subscribe(Projections.Add);
        }

        public async void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                Films.Clear();
                Projections.Clear();

                foreach (Film film in await _filmService.GetAllFilms())
                {
                    Films.Add(film);
                }
            }
            catch (Exception ex)
            {

                ErrorOccured?.Invoke(ex.Message);
            }


        }

        public async void LoadProjectionFilm(ObjectId id)
        {
            if (!ObjectId.TryParse(id.ToString(), out _)) throw new InvalidGuidException($"L'id {id} est invalid");
            Projections.Clear();

            foreach (Projection projection in await _projectionService.GetProjectionsById(id))
            {
                Projections.Add(projection);
            }
        }

        public async void SupprimerProjection(Projection projection)
        {
            MessageBoxResult reponse = MessageBox.Show($"Voulez vous supprimer la projection de {projection}","Confirmation",MessageBoxButton.YesNo,MessageBoxImage.Question);

            if (reponse == MessageBoxResult.Yes)
            {
                await _projectionService.SupprimerProjection(projection.Id);
                Load(null,null);
                LoadProjectionFilm(projection.Film.Id);
                MessageBox.Show("La projection a été supprimé avec succées");

            }
        }

        public async void SupprimerFilm(Film film)
        {
            MessageBoxResult reponse = MessageBox.Show($"Voulez vous supprimer la projection de {film}", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (reponse == MessageBoxResult.Yes)
            {
                await _filmService.SupprimerFilm(film.Id);
                Load(null, null);
                MessageBox.Show("Le film a été supprimé avec succées");
            }

        }

        public void ModifierProjection(IEventAggregator eventAggregator, Projection projection)
        {
            if (projection.Date < DateTime.Now)
            {
                MessageBox.Show("Impossible de modifier une projection qui est déja passé.", "Modification",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            FormulaireProjection detailProjection = new FormulaireProjection(_projectionService, _filmService, eventAggregator, projection);
           bool reponse = (bool)detailProjection.ShowDialog();

            if (reponse)
            {
                Load(null, null);
                LoadProjectionFilm(projection.Film.Id);
            }
        }

        public void ModifierFIlm(IEventAggregator eventAggregator, Film film)
        {
            DetailFilm detailFilm = new DetailFilm(_filmService, eventAggregator, film);

            if ((bool)detailFilm.ShowDialog())
            {
                Load(null, null);
            }
        }
    }
}
