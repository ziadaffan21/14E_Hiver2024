﻿using CineQuebec.Windows.Exceptions.ProjectionException;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data
{
    public class Projection : Entity
    {
        #region CONSTANTES

        public const int NB_PLACE_MIN = 50;

        #endregion CONSTANTES

        #region ATTRIBUTS

        private DateTime _date;

        private int _nbPlaces;

        private Film _film;

        #endregion ATTRIBUTS

        #region PROPRIÉTÉS ET INDEXEURS

        public DateTime Date
        {
            get { return _date; }
            set
            {
                //if (!DateTime.TryParse(value.ToString(), out _)) throw new DateProjectionException($"La date de projection doit être plus grand que {DateTime.Today}");
                _date = value;
            }
        }

        public int NbPlaces
        {
            get { return _nbPlaces; }
            set
            {
                if (!int.TryParse(value.ToString(), out _) || value < NB_PLACE_MIN) throw new PlaceDisponibleException($"Le nombre de place doit être plus grand que {NB_PLACE_MIN}");
                _nbPlaces = value;
            }
        }

        public Film Film
        {
            get { return _film; }
            set
            {
                if (value is null) throw new ArgumentNullException($"L'id {_film} est invalid");
                _film = value;
            }
        }

        public List<ObjectId> Reservations { get; set; }

        #endregion PROPRIÉTÉS ET INDEXEURS

        #region CONSTRUCTEURS

        public Projection()
        {
            Date = DateTime.Today;
            NbPlaces = NB_PLACE_MIN;
            Reservations = new();
        }

        public Projection(DateTime date, int placeDisponible, Film film)
        {
            Date = date;
            NbPlaces = placeDisponible;
            Film = film;
            Reservations = new();
        }


        #endregion CONSTRUCTEURS

        #region MÉTHODES

        public override string ToString()
        {
            return $"({Film.Titre}) {Date.Day} {Utils.Utils.GetMoisNom(Date).ToUpper()} {Date.Hour:00}:{Date.Minute:00}";
        }

        #endregion MÉTHODES
    }
}