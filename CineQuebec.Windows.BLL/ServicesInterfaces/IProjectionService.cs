using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IProjectionService
    {
        List<Projection> GetAllProjections();

        Task AjouterProjection(Projection projection);

        Task<List<Projection>> GetProjectionsById(ObjectId idFilm);

        Task<List<Projection>> GetProjectionByName(string titre);
        Task AjouterReservation(ObjectId pojectionId, ObjectId userId);
    }
}