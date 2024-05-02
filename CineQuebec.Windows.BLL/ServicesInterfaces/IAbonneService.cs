using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IAbonneService
    {
        List<Abonne> GetAllAbonnes();

        Task<bool> Add(Abonne abonne);

        Task<Abonne> GetAbonneConnexion(string username, string password);
        Task<Abonne> GetAbonne(ObjectId id);
        Task<bool> AddActeurInAbonne(ObjectId abonneId, Acteur acteur);
        Task<bool> AddRealisateurInAbonne(ObjectId abonneId, Realisateur realisateur);
        Task<bool> AddCategorieInAbonne(ObjectId abonneId, Categories categorie);
        Task<bool> AddFilmInAbonne(ObjectId abonneId, Film film);
        Task<bool> RemoveActeurInAbonne(ObjectId abonneId, Acteur acteur);
        Task<bool> RemoveRealisateurInAbonne(ObjectId abonneId, Realisateur realisateur);
        Task<bool> RemoveCategorieInAbonne(ObjectId abonneId, Categories categorie);
        Task<bool> RemoveFilmInAbonne(ObjectId abonneId, Film film);
    }
}