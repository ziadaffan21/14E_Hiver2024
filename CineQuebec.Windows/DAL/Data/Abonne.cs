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
        const byte NB_MIN_CARACTERES_USERNAME = 2;
        const byte NB_MAX_CARACTERES_USERNAME = 50;
        const byte NB_MAX_CARACTERES_PASSWORD = 50;
        const byte NB_MIN_CARACTERES_PASSWORD = 2;
        #endregion

        #region ATTRIBUTS
        private string? _username;
        private DateTime _dateAdhesion;
        private string _password;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new UsernameNullException("Le username ne peut pas etre vide");
                if (value.Trim().Length < 2 || value.Trim().Length > 15)
                    throw new UsernameLengthException($"Le username doit etre entre {NB_MIN_CARACTERES_USERNAME} et {NB_MAX_CARACTERES_USERNAME} caractères");
                _username = value.Trim();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new PasswordNullException("Le password ne peut pas etre vide");
                if (value.Trim().Length < 2 || value.Trim().Length > 15)
                    throw new PasswordLengthException($"Le password doit etre entre {NB_MIN_CARACTERES_PASSWORD} et {NB_MAX_CARACTERES_PASSWORD} caractères");
                _password = value.Trim();
            }
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
        public Abonne(string username, string password, DateTime dateAdhesion) : this(username, dateAdhesion)
        {
            Password = password;
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
