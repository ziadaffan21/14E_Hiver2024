using CineQuebec.Windows.DAL.Entities;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Acteur : Personne
    {
        public Acteur()
        {
        }

        public Acteur(string prenom, string nom, DateTime naissance, ObjectId? id = null) : base(prenom, nom, naissance)
        {
            Nom = nom;
            Prenom = prenom;
            Naissance = naissance;


            if (id != null)
            {
            Id = (ObjectId)id;
            }
        }

        public override string ToString()
        {
            return $"{Prenom} {Nom}";
        }

        public override bool Equals(object obj)
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