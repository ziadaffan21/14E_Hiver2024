using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IProjectionRepository
    {
        Task AjouterProjection(Projection projection);

        List<Projection> ReadProjections();

        Task<List<Projection>> ReadProjectionsById(object idFilm);

        Task<List<Projection>> ReadProjectionsByName(string Name);

        Task<Projection> GetProjectionByDateAndFilmId(DateTime dateProjection, string titreFilm);
    }
}