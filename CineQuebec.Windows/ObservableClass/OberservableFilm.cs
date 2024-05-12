using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Exceptions.AbonneExceptions.DateAdhesion;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
using CineQuebec.Windows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.ObservableClass
{
    public class OberservableFilm:PropertyNotifier
    {
        private Film _film = new();


        #region PROPRIÉTÉS ET INDEXEURS

        public string Titre
        {
            get { return _film.Titre; }
            set
            {
                if( _film.Titre != value)
                {
                    _film.Titre = value;
                    OnPropertyChanged();
                }
            }
        }


        public int Duree
        {
            get { return _film.Duree; }
            set
            {
                if(_film.Duree != value)
                {
                    _film.Duree = value; OnPropertyChanged();
                }
            }
        }

        public DateTime DateSortie
        {
            get { return _film.DateSortie; }
            set
            {
                if (_film.DateSortie != value)
                {
                    _film.DateSortie = value; OnPropertyChanged();
                }
            }
        }

        public Categories Categorie
        {
            get { return _film.Categorie; }
            set
            {
                if( _film.Categorie != value)
                {
                    _film.Categorie = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion PROPRIÉTÉS ET INDEXEURS


        #region MÉTHODES

        internal Film value()
        {
            return _film;
        }

        internal bool isValid()
        {
            return Enum.IsDefined(Categorie) && Duree > 0 && !string.IsNullOrWhiteSpace(Titre);
        }

        #endregion MÉTHODES
    }
}
