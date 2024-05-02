using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IFilmRepository
    {
        Task AjouterFilm(Film film);

        Task ModifierFilm(Film film);

        Task<List<Film>> ReadFilms();

        Task<Film> GetFilmByTitre(string titre);
    }
}