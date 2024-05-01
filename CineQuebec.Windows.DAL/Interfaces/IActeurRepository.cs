using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IActeurRepository
    {
        Task<List<Acteur>> GetAll();
    }
}