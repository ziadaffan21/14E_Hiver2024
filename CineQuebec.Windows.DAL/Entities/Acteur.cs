using CineQuebec.Windows.DAL.Entities;

namespace CineQuebec.Windows.DAL.Data
{
    public class Acteur : Personne
    {
        public Acteur()
        {
            
        }
        public Acteur(string prenom, string nom, DateTime naissance) : base(prenom, nom, naissance)
        {
        }
        public override string ToString()
        {
            return $"{Prenom} {Nom}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Acteur other)
            {
                return Id.Equals(other.Id);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}