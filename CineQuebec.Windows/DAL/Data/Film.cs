using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions.DateAdhesion;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film : Entity, IFilm
    {
        #region CONSTANTES
        public const byte NB_MIN_CARACTERES_USERNAME = 2;
        public const byte NB_MAX_CARACTERES_USERNAME = 100;
        #endregion

        #region ATTRIBUTS
        private string _titre;
        private Categories _categorie;
        private DateTime _dateSortie;

        #endregion

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
        #endregion

        #region CONSTRUCTEURS
        public Film()
        {
            
        }
        public Film(string titre, Categories categorie)
        {
            Titre = titre;
            Categorie = categorie;
        }
        public Film(string titre, DateTime dateSortie, Categories categorie) : this(titre, categorie)
        {
            DateSortie = dateSortie;
        }
        #endregion

        #region MÉTHODES
        public override string ToString()
        {
            return $"{Titre}";
        }
        #endregion
    }
}
