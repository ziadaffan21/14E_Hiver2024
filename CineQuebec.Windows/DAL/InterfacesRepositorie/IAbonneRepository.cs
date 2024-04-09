using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IAbonneRepository
    {
        List<Abonne> ReadAbonnes();
        Task<bool> Add(Abonne abonne);
        Task<Abonne> GetAbonne(ObjectId id);
    }
}