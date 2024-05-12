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
    public class OberservableFilm : PropertyNotifier
    {
        private Film _film;


        #region PROPRIÉTÉS ET INDEXEURS

        public string Titre
        {
            get { return _film.Titre; }
            set
            {
                if (_film.Titre != value)
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
                if (_film.Duree != value)
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
                if (_film.Categorie != value)
                {
                    _film.Categorie = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _indexCategorie;

        public int IndexCategorie
        {
            get { return _indexCategorie; }
            set
            {
                if (_indexCategorie != value)
                {
                    _indexCategorie = value;
                    Categorie = value != -1 ?(Categories)value:0;
                    OnPropertyChanged();
                }
            }
        }
        #endregion PROPRIÉTÉS ET INDEXEURS


        #region MÉTHODES
        public OberservableFilm(Film film)
        {
            _film = film is null ? new() : film;
            IndexCategorie=film is null?-1:(int)Categorie;
        }

        internal Film value()
        {
            return _film;
        }

        internal bool IsValid()
        {
            return Enum.IsDefined(Categorie) && Duree > 0 && !string.IsNullOrWhiteSpace(Titre);
        }

        #endregion MÉTHODES
    }
}
