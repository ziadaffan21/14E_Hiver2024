using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CineQuebec.Windows.DAL.Utils;
using System.Threading.Tasks;
using CineQuebec.Windows.Exceptions.EntitysExceptions;
using CineQuebec.Windows.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.Interfaces;

namespace CineQuebec.Windows.DAL.Data
{
    public class Projection : Entity, IProjection
    {
        #region CONSTANTES
        const byte NB_PLACE_MIN = 0;
        #endregion

        #region ATTRIBUTS
        private DateTime _date;

        private int _placeDisponible;

        private ObjectId _idFilm;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (!DateTime.TryParse(value.ToString(), out _) ) throw new DateProjectionException($"La date de projection doit être plus grand que {DateTime.Today}");
                _date = value;
            }
        }
        public int PlaceDisponible
        {
            get { return _placeDisponible; }
            set
            {
                if (!int.TryParse(value.ToString(), out _) || value < NB_PLACE_MIN) throw new PlaceDisponibleException($"La quantité de place doit être plus grand que {NB_PLACE_MIN}");
                _placeDisponible = value;
            }
        }

        public ObjectId IdFilm
        {
            get { return _idFilm; }
            set
            {
                if (!ObjectId.TryParse(value.ToString(), out _)) throw new InvalidGuidException($"L'id {IdFilm} est invalid");
                _idFilm = value;

            }
        }
        #endregion

        #region CONSTRUCTEURS
        //public Projection(DateTime date, ushort placeDispo,string idFilm)
        //{
        //    Date=date;
        //    PlaceDisponible=placeDispo;
        //    IdFilm = ObjectId.Parse(idFilm);
        //}
        public Projection()
        {
            Date = DateTime.Today;
        }
        #endregion

        #region MÉTHODES
        public override string ToString()
        {

            return $"{Date.Day} {Utils.Utils.GetMoisNom(Date)} {Date.Year} {Date.Hour:00}:{Date.Minute:00}";
        }
        #endregion
    }
}
