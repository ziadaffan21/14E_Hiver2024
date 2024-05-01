using CineQuebec.Windows.DAL.Entities;

namespace CineQuebec.Windows.DAL.Data
{
    public class Acteur : Personne
    {
        public Acteur(string prenom, string nom, DateTime naissance) : base(prenom, nom, naissance)
        {
        }
        public override string ToString()
        {
            return $"{Prenom} {Nom}";
        }
    }
}