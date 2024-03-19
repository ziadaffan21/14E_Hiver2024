using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne : Entity
    {
        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        private string? _username;
        private DateTime _dateAdhesion;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        public string? Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public DateTime DateAdhesion
        {
            get { return _dateAdhesion; }
            set { _dateAdhesion = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public Abonne(string? username)
        {
            Username = username;
        }

        public Abonne(string username, DateTime dateAdhesion)
        {
            Username = username;
            DateAdhesion = dateAdhesion;
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
