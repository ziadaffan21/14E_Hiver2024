﻿using Amazon.Util.Internal;
using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Entities;
using CineQuebec.Windows.View;
using CineQuebec.Windows.ViewModel.ObservableClass;
using MongoDB.Bson;
using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Unity;

namespace CineQuebec.Windows.ViewModel
{
    public class FilmDetailsViewModel : PropertyNotifier
    {
        private INoteService _noteService;
        private Film _film;
        private bool _projectionAVenir;

        public ICommand EnregistrerNoteCommand { get; set; }

        public Film Film
        {
            get { return _film; }
            set
            {
                _film = value;
                OnPropertyChanged(nameof(Film));
            }
        }

        public bool ProjectionAVenir
        {
            get => _projectionAVenir;
            set
            {
                _projectionAVenir = value;
                OnPropertyChanged(nameof(ProjectionAVenir));
            }
        }

        private float _noteMoy;

        public float NoteMoy
        {
            get { return _noteMoy; }
            set { 
                _noteMoy = value;
                OnPropertyChanged(nameof(NoteMoy));
            }
        }



        public Abonne User { get; set; }

        IProjectionService ProjectionService { get; set; }

       public ICommand NoterCommand { get; init; }
       public ICommand ReserverCommand { get; init; }

        public FilmDetailsViewModel(Film film, Abonne user, IProjectionService projectionService)
        {
            var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
            _noteService = container.Resolve<INoteService>();


            Film = film;
            User = user;
            ProjectionService = projectionService;
            NoterCommand = new DelegateCommand(OuvrirFormNoter);
            ReserverCommand = new DelegateCommand(OuvrirFormReserver);

            //Méthode temporaire pour ajouter la note moyenne
            InitialiserAsync();
        }

        private async Task InitialiserAsync()
        {
            NoteMoy = await _noteService.GetRatingForFilm(Film.Id);
        }

        public async Task<bool> PeutNoter()
            {
            List<Projection> projections = await ProjectionService.GetProjectionsForUser(Film.Id, User.Id);
            foreach (var projection in projections)
            {
                if (projection.Date < DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> PeutReserver()
        {
            List<Projection> projections = await ProjectionService.GetProjectionsById(Film.Id);

            for (int i = 0; i < projections.Count; i++)
        {
                var projection = projections[i];

                if (projection.Reservations.Contains(User.Id))
        {
                    projections.Remove(projection);
                }
        }

            return projections.Count > 0;
        }

        private void OuvrirFormNoter()
        {
            NoterView noterView = new NoterView(_noteService,Film);
            noterView.Show();
        }

        private void OuvrirFormReserver()
            {
            ReservationView reservationView = new(ProjectionService, Film, User);
            if (reservationView.ShowDialog() == true)
                {
                //TODO Afficher la réservation.
                }
            }

        internal async Task<bool> HasUpcomingProjections()
                {
            var projections = await ProjectionService.GetUpcomingProjections(Film.Id);

            return projections.Count > 0;
        }

    }
}