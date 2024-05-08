using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IFilmService
    {
        Task<List<Film>> GetAllFilms();

        Task AjouterFilm(Film film);

        Task ModifierFilm(Film film);
    }
}