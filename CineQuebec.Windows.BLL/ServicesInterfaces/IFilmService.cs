using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.ServicesInterfaces
{
    public interface IFilmService
    {
        Task<List<Film>> GetAllFilms();

        Task AjouterFilm(Film film);

        Task ModifierFilm(Film film);
        Task SupprimerFilm(ObjectId id);
    }
}