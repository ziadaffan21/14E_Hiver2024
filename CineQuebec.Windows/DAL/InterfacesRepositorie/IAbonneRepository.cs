using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IAbonneRepository
    {
        List<Abonne> ReadAbonnes();
    }
}