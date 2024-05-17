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
        public ObservableCollection<Film> Films { get; init; } =new();
        public ObservableCollection<Projection> Projections { get; init; }=new();
        public Film SelectedFilm { get; init; }
        public Projection SelectedProjection { get; init; }

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
            }
            catch (Exception ex)
            {

                ErrorOccured?.Invoke(ex.Message);
            }


        }

        internal async void LoadProjectionFilm(ObjectId id)
        {
            if (!ObjectId.TryParse(id.ToString(), out _)) throw new InvalidGuidException($"L'id {id} est invalid");
            Projections.Clear();

            foreach (Projection projection in await _projectionService.GetProjectionsById(id))
            {
                Projections.Add(projection);
            }
        }

        internal async void SupprimerProjection(Projection projection)
        {
            MessageBoxResult reponse = MessageBox.Show($"Voulez vous supprimer la projection de {projection}","Confirmation",MessageBoxButton.YesNo,MessageBoxImage.Hand);

            if (reponse == MessageBoxResult.Yes)
            {
                await _projectionService.SupprimerProjection(projection.Id);
                Load(null,null);
                LoadProjectionFilm(projection.Film.Id);
                MessageBox.Show("La projection a été supprimé avec succées");

            }
        }

        internal async void SupprimerFilm(Film film)
        {
            MessageBoxResult reponse = MessageBox.Show($"Voulez vous supprimer la projection de {film}", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Hand);

            if (reponse == MessageBoxResult.Yes)
            {
                await _filmService.SupprimerFilm(film.Id);
                Load(null, null);
                MessageBox.Show("Le film a été supprimé avec succées");
            }

        }

        internal void ModifierProjection(IEventAggregator eventAggregator, Projection projection)
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

        internal void ModifierFIlm(IEventAggregator eventAggregator, Film film)
        {
            DetailFilm detailFilm = new DetailFilm(_filmService, eventAggregator, film);

            if ((bool)detailFilm.ShowDialog())
            {
                Load(null, null);
            }
        }
    }
}
