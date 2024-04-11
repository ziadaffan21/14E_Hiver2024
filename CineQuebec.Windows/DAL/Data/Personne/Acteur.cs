using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions.DateAdhesion;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;
using System;
using System.Collections.Generic;
using CineQuebec.Windows.DAL.Data.Personne;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data.Personne
{
    public class Acteur : Personne
    {
        #region CONSTRUCTEURS
        public Acteur()
        {

        }

        public Acteur(string prenom, string nom, DateTime naissance)
        {
            Prenom = prenom;
            Nom = nom;
            Naissance = naissance;
        }

        #endregion

        #region MÉTHODES
        public override string ToString()
        {
            return $"{Prenom} {Nom}";
        }
        #endregion
    }
}
