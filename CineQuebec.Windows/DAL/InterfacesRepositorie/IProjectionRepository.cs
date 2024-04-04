using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IProjectionRepository
    {
        Task AjouterProjection(Projection projection);
        List<Projection> ReadProjections();
        List<Projection> ReadProjectionsById(object idFilm);
    }
}