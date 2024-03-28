using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Data.Personne;

namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IDatabasePeleMele
    {
        Task AjouterFilm(Film film);
        Task AjouterProjection(Projection projection);
        Task ModifierFilm(Film film);
        List<Abonne> ReadAbonnes();
        List<Acteur> ReadActeurs();
        List<Film> ReadFilms();
        List<Projection> ReadProjections();
    }
}