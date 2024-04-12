using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IAbonneService
    {
        List<Abonne> GetAllAbonnes();

        Task<bool> Add(Abonne abonne);

        Task<bool> GetAbonneConnexion(string username, string password);
        Task<Abonne> GetAbonne(ObjectId id);
    }
}