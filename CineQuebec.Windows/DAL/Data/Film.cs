using CineQuebec.Windows.DAL.Enums;
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
    public class Film : Entity
    {
        #region CONSTANTES
        const byte NB_MIN_CARACTERES_USERNAME = 2;
        const byte NB_MAX_CARACTERES_USERNAME = 15;
        #endregion

        #region ATTRIBUTS
        private string _titre;
        private Categories _categorie;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        public string Titre
        {
            get { return _titre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new TitreNullException("Le titre ne peut pas etre vide ou null");
                if (value.Trim().Length < 2 || value.Trim().Length > 50) throw new TitreLengthException($"Le titre doit etre entre {NB_MIN_CARACTERES_USERNAME} et {NB_MAX_CARACTERES_USERNAME} caractères.");
                _titre = value;
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
        public Film(string titre, Categories categorie)
        {
            Titre = titre;
            Categorie = categorie;
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
