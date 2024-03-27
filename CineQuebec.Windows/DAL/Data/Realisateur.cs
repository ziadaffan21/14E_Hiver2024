using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Realisateur: Personne
    {
        #region CONSTRUCTEURS
        public Realisateur()
        {

        }

        public Realisateur(string prenom, string nom, DateTime naissance)
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
