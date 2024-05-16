using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.FilmExceptions;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private const string FILMS = "Films";
        private const string PROJECTION = "Projections";
        private readonly IMongoClient _mongoBDClinet;
        private readonly IDataBaseUtils _dataBaseUtils;

        private readonly IMongoDatabase _mongoDataBase;

        public FilmRepository(IDataBaseUtils dataBaseUtils, IMongoClient mongoClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoBDClinet = mongoClient ?? _dataBaseUtils.OuvrirConnexion();
            _mongoDataBase = _dataBaseUtils.ConnectDatabase(_mongoBDClinet);
        }

        public async Task<List<Film>> ReadFilms()
        {
            var collection = _mongoDataBase.GetCollection<Film>(FILMS);

            return await collection.Aggregate().ToListAsync();
        }

        public async Task ModifierFilm(Film film)
        {
            var tableFilm = _mongoDataBase.GetCollection<Film>(FILMS);
            var filter = Builders<Film>.Filter.Eq(f => f.Id, film.Id);

            var duplicateFilter = Builders<Film>.Filter.And(Builders<Film>.Filter.Eq(f => f.Titre, film.Titre), Builders<Film>.Filter.Ne(f => f.Id, film.Id));
            var duplicateCount = tableFilm.CountDocuments(duplicateFilter);

            if (duplicateCount > 0)
                throw new ExistingFilmException($"Un film avec le titre {film.Titre} existe déjà.");

            await tableFilm.ReplaceOneAsync(filter, film);
        }

        public async Task AjouterFilm(Film film)
        {
            var tableFilm = _mongoDataBase.GetCollection<Film>(FILMS);

            await tableFilm.InsertOneAsync(film);
        }

        public async Task<Film> GetFilmByTitre(string titre)
        {
            var tableFilm = _mongoDataBase.GetCollection<Film>(FILMS);
            Film film = await tableFilm.Find(f => f.Titre.ToUpper() == titre.ToUpper()).FirstOrDefaultAsync();

            return film;
        }

        public async Task SupprimerFilm(ObjectId id)
        {
            var tableFilm = _mongoDataBase.GetCollection<Film>(FILMS);
            var tableProjections = _mongoDataBase.GetCollection<Projection>(PROJECTION);

            await tableFilm.FindOneAndDeleteAsync(f => f.Id == id);
            await tableProjections.DeleteManyAsync(f => f.Film.Id == id);
        }

    }
}