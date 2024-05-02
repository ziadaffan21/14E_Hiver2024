using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IAbonneRepository
    {
        List<Abonne> ReadAbonnes();

        Task<bool> Add(Abonne abonne);

        Task<Abonne> GetAbonneByUsername(string username);

        Task<Abonne> GetAbonneConnexion(string username, string password);
        Task<Abonne> GetAbonne(ObjectId id);
        Task<Abonne> UpdateOne(Abonne abonne);
    }
}