using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.Exceptions.ProjectionException;
using CineQuebec.Windows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.ViewModel.ObservableClass
{
    public class ObservableProjection : PropertyNotifier
    {
        private Projection _projection = new();

        public ObservableProjection()
        {
            
        }
        public ObservableProjection(Projection projection)
        {
            _projection = projection != null ? projection : new();
        }


        #region PROPRIÉTÉS ET INDEXEURS

        public DateTime Date
        {
            get { return _projection.Date; }
            set
            {
                if (_projection.Date != value)
                {
                    _projection.Date = value;
                    OnPropertyChanged();
                }
            }
        }

        public int PlaceDisponible
        {
            get { return _projection.NbPlaces; }
            set
            {
                if (_projection.NbPlaces != value)
                {
                    _projection.NbPlaces = value;
                    OnPropertyChanged();
                }
            }
        }

        public Film Film
        {
            get { return _projection.Film; }
            set
            {
                if (_projection.Film != value)
                {
                    _projection.Film = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion PROPRIÉTÉS ET INDEXEURS

        #region MÉTHODES
        public Projection Value()
        {
            return _projection;
        }

        public bool IsValid()
        {
            return Film is not null && PlaceDisponible >= Projection.NB_PLACE_MIN && Date >= DateTime.Now;
        }
        #endregion MÉTHODES
    }
}
