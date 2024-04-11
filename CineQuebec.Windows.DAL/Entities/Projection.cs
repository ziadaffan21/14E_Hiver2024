using CineQuebec.Windows.Exceptions.EntitysExceptions;
using CineQuebec.Windows.Exceptions.ProjectionException;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Projection : Entity
    {
        #region CONSTANTES

        public const int NB_PLACE_MIN = 1;

        #endregion CONSTANTES

        #region ATTRIBUTS

        private DateTime _date;

        private int _placeDisponible;

        private ObjectId _idFilm;

        #endregion ATTRIBUTS

        #region PROPRIÉTÉS ET INDEXEURS

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (!DateTime.TryParse(value.ToString(), out _)) throw new DateProjectionException($"La date de projection doit être plus grand que {DateTime.Today}");
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

        #endregion PROPRIÉTÉS ET INDEXEURS

        #region CONSTRUCTEURS
        public Projection()
        {
            Date = DateTime.Today;
        }

        #endregion CONSTRUCTEURS

        #region MÉTHODES

        public override string ToString()
        {
            return $"{Date.Day} {Utils.Utils.GetMoisNom(Date).ToUpper()} {Date.Hour:00}:{Date.Minute:00}";
        }
        
        #endregion
    }
}