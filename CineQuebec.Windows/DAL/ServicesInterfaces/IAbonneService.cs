using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IAbonneService
    {
        List<Abonne> GetAllAbonnes();
    }
}