using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IProjectionService
    {
        List<Projection> GetAllProjections();
        Task AjouterProjection(Projection projection);
        List<Projection> ReadProjectionsById(ObjectId idFilm);
    }
}