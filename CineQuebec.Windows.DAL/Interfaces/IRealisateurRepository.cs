using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IRealisateurRepository
    {
        Task<List<Realisateur>> GetAll();
    }
}