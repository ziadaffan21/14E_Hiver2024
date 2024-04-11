using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.Exceptions;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private const string FILMS = "Films";
        private readonly IMongoClient _mongoBDClinet;
        private readonly IDataBaseUtils _dataBaseUtils;

        private readonly IMongoDatabase _mongoDataBase;

        public FilmRepository(IDataBaseUtils dataBaseUtils, IMongoClient mongoClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoBDClinet = mongoClient ?? _dataBaseUtils.OuvrirConnexion();
            _mongoDataBase = _dataBaseUtils.ConnectDatabase(_mongoBDClinet);
        }

        public List<Film> ReadFilms()
        {
            var films = new List<Film>();
            try
            {
                var collection = _mongoDataBase.GetCollection<Film>(FILMS);
                films = collection.Aggregate().ToList();
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la lecture de données des films");
            }
            return films;
        }

        public async Task ModifierFilm(Film film)
        {
            var tableFilm = _mongoDataBase.GetCollection<Film>(FILMS);
            var filter = Builders<Film>.Filter.Eq(f => f.Id, film.Id);
            try
            {
                await tableFilm.ReplaceOneAsync(filter, film);
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la modification du film.");
            }
        }

        public async Task AjouterFilm(Film film)
        {
            var tableFilm = _mongoDataBase.GetCollection<Film>(FILMS);
            try
            {
                await tableFilm.InsertOneAsync(film);
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la modification du film.");
            }
        }
    }
}