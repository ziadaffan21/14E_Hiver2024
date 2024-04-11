using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Exceptions.AbonneExceptions.DateAdhesion;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film : Entity
    {
        #region CONSTANTES

        public const byte NB_MIN_CARACTERES_USERNAME = 2;
        public const byte NB_MAX_CARACTERES_USERNAME = 100;

        #endregion CONSTANTES

        #region ATTRIBUTS

        private string _titre;
        private Categories _categorie;
        private DateTime _dateSortie;
        private int _duree;

        #endregion ATTRIBUTS

        #region PROPRIÉTÉS ET INDEXEURS

        public string Titre
        {
            get { return _titre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new TitreNullException("Le titre ne peut pas etre vide ou null");
                if (value.Trim().Length < NB_MIN_CARACTERES_USERNAME || value.Trim().Length > NB_MAX_CARACTERES_USERNAME) throw new TitreLengthException($"Le titre doit etre entre {NB_MIN_CARACTERES_USERNAME} et {NB_MAX_CARACTERES_USERNAME} caractères.");
                _titre = value;
            }
        }

        /// <summary>
        /// Durée définie en minutes
        /// </summary>
        public int Duree
        {
            get { return _duree; }
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("La durée ne peut pas être négative");
                }
                _duree = value; 
            
            }
        }

        public DateTime DateSortie
        {
            get { return _dateSortie; }
            set
            {
                if (!DateTime.TryParse(value.ToString(), out _)) throw new InvalidDateAdhesionException($"Le date {value} n'est pas valid");
                _dateSortie = value;
            }
        }

        public Categories Categorie
        {
            get { return _categorie; }
            set
            {
                if (!Enum.IsDefined(value)) throw new CategorieUndefinedException("La catégorie doit etre dfinie");
                _categorie = value;
            }
        }

        #endregion PROPRIÉTÉS ET INDEXEURS

        #region CONSTRUCTEURS

        public Film(string titre, DateTime dateSortie, int duree, Categories categorie)
        {
            Titre = titre;
            DateSortie = dateSortie;
            Duree = duree;
            Categorie = categorie;
        }

        #endregion CONSTRUCTEURS

        #region MÉTHODES

        public override string ToString()
        {
            return $"{Titre} ({DateSortie.Year})";
        }

        #endregion MÉTHODES
    }
}