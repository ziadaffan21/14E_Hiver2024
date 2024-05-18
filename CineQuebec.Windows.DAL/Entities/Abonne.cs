﻿using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Exceptions.AbonneExceptions.DateAdhesion;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;
using MongoDB.Bson;
using System.Globalization;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne : Entity,IComparable
    {
        #region CONSTANTES

        public const byte NB_MIN_CARACTERES_USERNAME = 2;
        public const byte NB_MAX_CARACTERES_USERNAME = 50;
        public const byte NB_MAX_CARACTERES_PASSWORD = 50;
        public const byte NB_MIN_CARACTERES_PASSWORD = 2;

        #endregion CONSTANTES

        #region ATTRIBUTS

        private string _username;
        private DateTime _dateAdhesion;
        private bool _isAdmin = false;


        #endregion ATTRIBUTS

        #region PROPRIÉTÉS ET INDEXEURS

        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new UsernameNullException("Le username ne peut pas etre vide");
                if (value.Trim().Length < NB_MIN_CARACTERES_USERNAME || value.Trim().Length > NB_MAX_CARACTERES_USERNAME)
                    throw new UsernameLengthException($"Le username doit etre entre {NB_MIN_CARACTERES_USERNAME} et {NB_MAX_CARACTERES_USERNAME} caractères");
                _username = value.Trim();
            }
        }

        public byte[] Password { get; set; }

        public DateTime DateAdhesion
        {
            get { return _dateAdhesion; }
            set
            {
                if (!DateTime.TryParse(value.ToString(), out _)) throw new InvalidDateAdhesionException($"Le date {DateAdhesion} est invalid");
                _dateAdhesion = value;
            }
        }

        public byte[] Salt { get; set; }


        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }


        public List<Acteur> Acteurs { get; set; } = new List<Acteur>();
        public List<Realisateur> Realisateurs { get; init; } = new List<Realisateur>();
        public List<Film> Films { get; set; } = new List<Film>();
        public List<Categories> CategoriesPrefere { get; set; } = new List<Categories>();

        #endregion PROPRIÉTÉS ET INDEXEURS

        #region CONSTRUCTEURS

        public Abonne()
        {
        }

        public Abonne(string username, DateTime dateAdhesion)
        {
            Username = username;
            DateAdhesion = dateAdhesion;
        }

        public Abonne(string username, byte[] password, byte[] salt, DateTime dateAdhesion) : this(username, dateAdhesion)
        {
            Password = password;
            Salt = salt;
        }


        public Abonne(ObjectId id , string username, byte[] password, byte[] salt, DateTime dateAdhesion, bool isAdmin) : this(username, password, salt, dateAdhesion)
        {
            //Constructeur pour l'ajout à la bd.
            Id = id;
            Username = username;
            Password = password;
            Salt = salt;
            DateAdhesion = dateAdhesion;
            IsAdmin = isAdmin;

        }

        #endregion CONSTRUCTEURS

        #region MÉTHODES

        public override string ToString()
        {
            return $"{Username}";
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (!(obj is Abonne))
                throw new ArgumentException("L'object doit être un abonnée", "obj");
            Abonne autreAbonne = obj as Abonne;
            return ToString().CompareTo(autreAbonne.ToString());
        }

        #endregion MÉTHODES
    }
}