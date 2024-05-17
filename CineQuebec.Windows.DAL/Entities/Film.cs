using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Exceptions.AbonneExceptions.DateAdhesion;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film : Entity, IComparable
    {
        #region CONSTANTES

        public const byte NB_MIN_CARACTERES_USERNAME = 2;
        public const byte NB_MAX_CARACTERES_USERNAME = 100;
        public const byte NB_MIN_DUREE = 30;
        public const string PLACEHOLDER_DESC = "Commodi animi eius nihil nostrum veritatis eaque eligendi ut. Eius et quia doloribus quasi eaque eos veritatis nobis. Culpa alias ut excepturi. Ut laborum inventore eos. Dolorem commodi quibusdam rerum consequatur.";


        #endregion CONSTANTES

        #region ATTRIBUTS

        private string _titre;
        private Categories _categorie;
        private DateTime _dateSortie;
        private int _duree;
        private bool _estAffiche;
        private Realisateur _realisateur;


        #endregion ATTRIBUTS

        #region PROPRIÉTÉS ET INDEXEURS


        private Acteur _acteurs;

        public Acteur Acteurs
        {
            get { return _acteurs; }
            set { _acteurs = value; }
        }


        //TODO : Créer le constructeur qu prend en paramètre le réalisateur.
        public Realisateur Realisateur
        {
            get { return _realisateur; }
            set { _realisateur = value; }
        }


        public string Titre
        {
            get { return _titre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new TitreNullException("Le titre ne peut pas etre vide ou null");
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
                //if (value <= 0)
                //{
                //    throw new ArgumentOutOfRangeException("La durée ne peut pas être négative");
                //}
                _duree = value;
            }
        }

        public string DureeToString 
        { 
            get 
            {
                int heures = Duree / 60;
                int minutesRestantes = Duree % 60;

                return $"{heures:0}h {minutesRestantes:00 mins}";
            } 
        }

        public string Image { get { return $"https://picsum.photos/seed/{Id}/200/300"; } }

        public DateTime DateSortie
        {
            get { return _dateSortie; }
            set
            {
                if (!DateTime.TryParse(value.ToString(), out _)) throw new InvalidDateAdhesionException($"Le date {value} n'est pas valid");
                _dateSortie = value;
            }
        }
        public string Year { get { return $"({DateSortie.Year})"; } }
        public string DateSortieToString { get { return $"{DateSortie.ToShortDateString()}"; }  }

        public Categories Categorie
        {
            get { return _categorie; }
            set
            {
                if (!Enum.IsDefined(value)) throw new CategorieUndefinedException("La catégorie doit etre dfinie");
                _categorie = value;
            }
        }


        public bool EstAffiche
        {
            get { return _estAffiche; }
            set { _estAffiche = value; }
        }

       

        #endregion PROPRIÉTÉS ET INDEXEURS

        #region CONSTRUCTEURS

        public Film(string titre, DateTime dateSortie, int duree, Categories categorie, bool estAffiche = false)
        {
            Titre = titre;
            DateSortie = dateSortie;
            Duree = duree;
            Categorie = categorie;
            EstAffiche = estAffiche;
        }

        public Film(string titre, DateTime dateSortie, int duree, Categories categorie, ObjectId id, bool estAffiche = false)
        {
            Id = id;
            Titre = titre;
            DateSortie = dateSortie;
            Duree = duree;
            Categorie = categorie;
            EstAffiche = estAffiche;
        }

        public Film()
        {
        }

        #endregion CONSTRUCTEURS

        #region MÉTHODES

        public override string ToString()
        {
            return $"{Titre} ({DateSortie.Year})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Film other)
            {
                return Id.Equals(other.Id);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (!(obj is Film))
                throw new ArgumentException("L'object doit être un film", "obj");
            Film autreFilm = obj as Film;
            return ToString().CompareTo(autreFilm.ToString());
        }


        #endregion MÉTHODES
    }
}