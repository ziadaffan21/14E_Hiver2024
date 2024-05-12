using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL
{
    public class DataBaseSeeder : IDataBaseSeeder
    {
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;
        private readonly IDataBaseUtils _dataBaseUtils;
        private readonly Film film1 = new Film("The Shawshank Redemption", DateTime.Now, 120, Categories.ANIMATION);
        private readonly Film film2 = new Film("The Godfather", DateTime.Now, 120, Categories.ADVENTURE);

        public DataBaseSeeder(IDataBaseUtils dataBaseUtils, IMongoClient mongoDBClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoDBClient = mongoDBClient ?? _dataBaseUtils.OuvrirConnexion();
            _database = _dataBaseUtils.ConnectDatabase(_mongoDBClient);
            _dataBaseUtils = dataBaseUtils;
        }

        public async Task Seed()
        {
            //await SeedFilms();
            await SeedAbonnes();
            await SeedActeur();
            await SeedRealisateur();
        }

        private async Task SeedFilms()
        {
            var filmsCollection = _database.GetCollection<BsonDocument>("Films");

            await filmsCollection.DeleteManyAsync(new BsonDocument());

            var films = new List<Film>
                    {
                        film1,
                        film2
                    };

            var bsonFilms = films.Select(film => film.ToBsonDocument()).ToList();

            await filmsCollection.InsertManyAsync(bsonFilms);
        }

        private async Task SeedAbonnes()
        {
            var abonneCollection = _database.GetCollection<BsonDocument>("Abonnes");
            await abonneCollection.DeleteManyAsync(new BsonDocument());

            var salt = PasswodHasher.CreateSalt();

            var abonnes = new List<Abonne>
                {
                    new Abonne("admin",PasswodHasher.HashPassword("12345",salt),salt,DateTime.Now)
                    {
                        isAdmin = true,
                    },
                    new Abonne("user1",PasswodHasher.HashPassword("12345",salt),salt,DateTime.Now)
                };

            var bsonAbonnes = abonnes.Select(abonne => abonne.ToBsonDocument()).ToList();
            await abonneCollection.InsertManyAsync(bsonAbonnes);
        }

        private async Task SeedActeur()
        {
            var acteurCollection = _database.GetCollection<BsonDocument>("Acteurs");
            await acteurCollection.DeleteManyAsync(new BsonDocument());

            var acteurs = new List<Acteur>
            {
                new Acteur("Leonardo","DiCaprio",DateTime.Now),
                new Acteur("Meryl","Streep",DateTime.Now),
            };

            var bsonActeur = acteurs.Select(a => a.ToBsonDocument()).ToList();
            await acteurCollection.InsertManyAsync(bsonActeur);
        }

        private async Task SeedRealisateur()
        {
            var realisateurCollection = _database.GetCollection<BsonDocument>("Realisateurs");
            await realisateurCollection.DeleteManyAsync(new BsonDocument());

            var realisateurs = new List<Realisateur>
            {
                new Realisateur("Martin", "Scorsese", DateTime.Now),
                new Realisateur("Steven", "Spielberg", DateTime.Now),
                new Realisateur("Quentin", "Tarantino", DateTime.Now),
                new Realisateur("Christopher", "Nolan", DateTime.Now),
                new Realisateur("James", "Cameron", DateTime.Now),
                new Realisateur("George", "Lucas", DateTime.Now),
                new Realisateur("Peter", "Jackson", DateTime.Now),
                new Realisateur("James", "Marshall", DateTime.Now)
            };

            var bsonRealisateur = realisateurs.Select(a => a.ToBsonDocument()).ToList();
            await realisateurCollection.InsertManyAsync(bsonRealisateur);
        }
    }
}