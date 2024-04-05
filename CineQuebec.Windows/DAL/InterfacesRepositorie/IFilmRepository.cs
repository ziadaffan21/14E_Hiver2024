using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IFilmRepository
    {
        Task AjouterFilm(Film film);
        Task ModifierFilm(Film film);
        List<Film> ReadFilms();
    }
}