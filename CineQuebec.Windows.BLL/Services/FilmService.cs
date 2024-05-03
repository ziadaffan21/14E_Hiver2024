using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.FilmExceptions;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.ServicesInterfaces;

namespace CineQuebec.Windows.DAL.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task AjouterFilm(Film film)
        {
            var existingFilm = await _filmRepository.GetFilmByTitre(film.Titre);

            if (existingFilm is not null)
                throw new ExistingFilmException($"Un film avec le titre '{film.Titre}' et la date de sortie '{film.DateSortie.Year}/{film.DateSortie.Month}/{film.DateSortie.Day}' existe dèjà.");

            await _filmRepository.AjouterFilm(film);
        }

        public async Task<List<Film>> GetAllFilms()
        {
            return await _filmRepository.ReadFilms();
        }

        public async Task ModifierFilm(Film film)
        {
            if (film is null)
                throw new ArgumentNullException("Film", "Le film ne peut pas etre null.");

            await _filmRepository.ModifierFilm(film);
        }
    }
}