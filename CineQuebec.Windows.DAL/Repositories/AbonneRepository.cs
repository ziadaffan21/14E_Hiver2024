using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using CineQuebec.Windows.Exceptions.EntitysExceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class AbonneRepository : IAbonneRepository
    {
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;
        private readonly IDataBaseUtils _dataBaseUtils;
        private const string ABONNE = "Abonnes";

        public AbonneRepository(IDataBaseUtils dataBaseUtils, IMongoClient mongoDBClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoDBClient = mongoDBClient ?? _dataBaseUtils.OuvrirConnexion();
            _database = _dataBaseUtils.ConnectDatabase(_mongoDBClient);
            _dataBaseUtils = dataBaseUtils;
        }

        public List<Abonne> ReadAbonnes()
        {
            var collection = _database.GetCollection<Abonne>(ABONNE);
            List<Abonne> abonnes = collection.Aggregate().ToList();

            return abonnes;
        }

        public async Task<bool> Add(Abonne abonne)
        {
            var collection = _database.GetCollection<Abonne>(ABONNE);

            await collection.InsertOneAsync(abonne);
            return true;
        }

        public async Task<Abonne> GetAbonneByUsername(string username)
        {
            var collection = _database.GetCollection<Abonne>(ABONNE);
            Abonne abonne = await collection.Find(x => x.Username.ToUpper() == username.ToUpper()).FirstOrDefaultAsync();

            return abonne;
        }


        public async Task<Abonne> GetAbonneConnexion(string username, string password)
        {
            var collection = _database.GetCollection<Abonne>(ABONNE);
            Abonne abonne = await collection.Find(x => x.Username == username).FirstOrDefaultAsync();

            var result = PasswodHasher.VerifyHash(password, abonne.Salt, abonne.Password);
            if (!result)
                return null;
            return abonne;
        }

        public async Task<Abonne> GetAbonne(ObjectId id)
        {
            var collection = _database.GetCollection<Abonne>(ABONNE);
            Abonne abonne = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            return abonne;
        }

        public async Task<Abonne> UpdateOne(Abonne abonne)
        {
            var collection = _database.GetCollection<Abonne>(ABONNE);
            var replace = await collection.ReplaceOneAsync(filter: g => g.Id == abonne.Id, replacement: abonne);
            if (replace.IsAcknowledged)
                return abonne;
            return null;
        }
    }
}