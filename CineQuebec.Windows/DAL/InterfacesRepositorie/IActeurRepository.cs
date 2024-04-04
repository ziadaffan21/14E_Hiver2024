using CineQuebec.Windows.DAL.Data.Personne;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IActeurRepository
    {
        List<Acteur> ReadActeurs();
    }
}