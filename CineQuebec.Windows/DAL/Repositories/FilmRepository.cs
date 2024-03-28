using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class FilmRepository
    {
        private const string FILMS = "Films";
        private readonly IMongoClient _mongoBDClinet;
        private readonly IMongoDatabase _mongoDataBase;
        public FilmRepository(IMongoClient mongoClient = null)
        {
            _mongoBDClinet = mongoClient ?? DataBaseUtils.OuvrirConnexion();
            _mongoDataBase = DataBaseUtils.ConnectDatabase(_mongoBDClinet);
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
