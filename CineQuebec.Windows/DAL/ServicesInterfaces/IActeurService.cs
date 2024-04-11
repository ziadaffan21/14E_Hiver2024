using CineQuebec.Windows.DAL.Data.Personne;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IActeurService
    {
        List<Acteur> GetAllActeurs();
    }
}