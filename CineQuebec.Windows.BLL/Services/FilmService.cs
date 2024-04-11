using CineQuebec.Windows.DAL.Data;
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
            await _filmRepository.AjouterFilm(film);
        }

        public List<Film> GetAllFilms()
        {
            return _filmRepository.ReadFilms();
        }

        public async Task ModifierFilm(Film film)
        {
            await _filmRepository.ModifierFilm(film);
        }
    }
}