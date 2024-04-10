using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using CineQuebec.Windows.Exceptions.EntitysExceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var abonnes = new List<Abonne>();

            try
            {
                var collection = _database.GetCollection<Abonne>(ABONNE);
                abonnes = collection.Aggregate().ToList();
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la lecture de données d'abonnés");
            }
            return abonnes;
        }

        public async Task<bool> Add(Abonne abonne)
        {
            ArgumentNullException.ThrowIfNull(abonne);


            var collection = _database.GetCollection<Abonne>(ABONNE);
            var existingAbonne = collection.Find(x => x.Username == abonne.Username).FirstOrDefault();

            if (existingAbonne is null)
                await collection.InsertOneAsync(abonne);
            else throw new ExistingAbonneException($"L'abonne avec le username '{abonne.Username}' exite déjà");

            return true;

        }

        public async Task<Abonne> GetAbonne(ObjectId id)
        {

            if (Guid.TryParse(id.ToString(), out _))
                throw new InvalidGuidException($"L'id {id} n'est pas valide.");

            Abonne abonne = null;

            var collection = _database.GetCollection<Abonne>(ABONNE);
            abonne = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();



            return abonne;
        }

        public async Task<bool> GetAbonneConnexion(string username, string password)
        {
            var collection = _database.GetCollection<Abonne>(ABONNE);
            Abonne abonne = await collection.Find(x => x.Username == username).FirstOrDefaultAsync();

            if (abonne is null)
                return false;

            return PasswodHasher.VerifyHash(password, abonne.Salt, abonne.Password);
        }

    }
}
