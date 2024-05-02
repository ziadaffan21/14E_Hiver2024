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
        Task<Abonne> AddActeurInAbonne(Abonne abonne, Acteur acteur);
        Task<Abonne> AddRealisateurInAbonne(Abonne abonne, Realisateur realisateur);
        Task<Abonne> AddCategorieInAbonne(Abonne abonne, Categories categorie);
        Task<Abonne> AddFilmInAbonne(Abonne abonne, Film film);
        Task<Abonne> RemoveActeurInAbonne(Abonne abonne, Acteur acteur);
        Task<Abonne> RemoveRealisateurInAbonne(Abonne abonne, Realisateur realisateur);
        Task<Abonne> RemoveCategorieInAbonne(Abonne abonne, Categories categorie);
        Task<Abonne> RemoveFilmInAbonne(Abonne abonne, Film film);
    }
}