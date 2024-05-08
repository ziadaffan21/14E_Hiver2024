using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IProjectionRepository
    {
        Task AjouterProjection(Projection projection);

        List<Projection> ReadProjections();

        Task<List<Projection>> ReadProjectionsById(object idFilm);

        Task<List<Projection>> GetProjectionsByName(string Name);

        Task<Projection> GetProjectionByDateAndFilmId(DateTime dateProjection, string titreFilm);
        Task AjouterReservation(ObjectId pojectionId, ObjectId userId);
    }
}