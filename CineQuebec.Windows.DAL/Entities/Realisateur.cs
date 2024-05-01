using CineQuebec.Windows.DAL.Entities;

namespace CineQuebec.Windows.DAL.Data
{
    public class Realisateur : Personne
    {
        public Realisateur(string? prenom, string? nom, DateTime naissance) : base(prenom, nom, naissance)
        {
        }
        public override string ToString()
        {
            return $"{Prenom} {Nom}";
        }
    }
}