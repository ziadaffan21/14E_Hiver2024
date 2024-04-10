using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions.DateAdhesion;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Password;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne : Entity, IAbonne
    {
        #region CONSTANTES
        public const byte NB_MIN_CARACTERES_USERNAME = 2;
        public const byte NB_MAX_CARACTERES_USERNAME = 50;
        public const byte NB_MAX_CARACTERES_PASSWORD = 50;
        public const byte NB_MIN_CARACTERES_PASSWORD = 2;
        #endregion

        #region ATTRIBUTS
        private string? _username;
        private DateTime _dateAdhesion;
        private byte[] _password;
        #endregion

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

        public byte[] Password
        {
            get { return _password; }
            set { _password = value; }
        }

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
        #endregion

        #region CONSTRUCTEURS
        public Abonne(string username)
        {
            Username = username;
        }
        public Abonne(string username, DateTime dateAdhesion) : this(username)
        {
            DateAdhesion = dateAdhesion;
        }
        public Abonne(string username, byte[] password, byte[] salt, DateTime dateAdhesion) : this(username, dateAdhesion)
        {
            Password = password;
            Salt = salt;
        }
        #endregion

        #region MÉTHODES
        public override string ToString()
        {
            return $"{Username}";
        }

        #endregion

        #region ÉVÉNEMENTS

        #endregion
    }
}
