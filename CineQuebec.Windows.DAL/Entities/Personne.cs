using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Exceptions.ActeurExceptions.DateNaissance;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;

namespace CineQuebec.Windows.DAL.Entities
{
    public abstract class Personne : Entity
    {
        #region CONSTANTES

        private const byte NB_CARACTERE_MAX_NOM = 100;
        private const byte NB_CARACTERE_MIN_NOM = 2;
        private const byte NB_CARACTERE_MAX_PRENOM = 100;
        private const byte NB_CARACTERE_MIN_PRENOM = 5;

        #endregion CONSTANTES

        #region ATTRIBUTS

        private string _prenom;
        private string _nom;
        private DateTime _naissance;

        #endregion ATTRIBUTS

        #region PROPRIÉTÉS ET INDEXEURS

        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new PrenomActeurNullException($"Le prenom ne peut pas etre null");
                if (value.Trim().Length < NB_CARACTERE_MIN_PRENOM || value.Trim().Length > NB_CARACTERE_MAX_PRENOM) throw new PrenomLengthException($"Le prenom doit etre entre {NB_CARACTERE_MIN_PRENOM} et {NB_CARACTERE_MAX_PRENOM}");
                _prenom = value;
            }
        }

        public string Nom
        {
            get { return _nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new NomActeurNullException($"Le nom ne peut pas etre null");
                if (value.Trim().Length < NB_CARACTERE_MIN_NOM || value.Trim().Length > NB_CARACTERE_MAX_NOM) throw new NomLengthException($"Le nom doit etre entre {NB_CARACTERE_MIN_NOM} et {NB_CARACTERE_MAX_NOM}");
                _nom = value;
            }
        }

        public DateTime Naissance
        {
            get { return _naissance; }
            set
            {
                if (!DateTime.TryParse(value.ToString(), out _)) throw new InvalidDateNaissanceException($"Le date {value} est invalid");
                _naissance = value;
            }
        }

        public Personne()
        {
        }

        protected Personne(string prenom, string nom, DateTime naissance)
        {
            Prenom = prenom;
            Nom = nom;
            Naissance = naissance;
        }

        #endregion PROPRIÉTÉS ET INDEXEURS
    }
}