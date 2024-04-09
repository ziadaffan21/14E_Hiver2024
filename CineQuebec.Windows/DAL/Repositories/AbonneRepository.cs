using CineQuebec.Windows.DAL.Data;
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

            try
            {
                var collection = _database.GetCollection<Abonne>(ABONNE);
                var existingAbonne = collection.Find(x => x.Username == abonne.Username).FirstOrDefault();
                if (existingAbonne is null)
                    await collection.InsertOneAsync(abonne);
                else throw new ExistingAbonneException($"L'abonne avec le username '{abonne.Username}' exite déjà");

                return true;
            }
            catch (Exception)
            {
                throw ;
            }
        }

        public async Task<Abonne> GetAbonne(ObjectId id)
        {
            ArgumentNullException.ThrowIfNull(id);

            if (Guid.TryParse(id.ToString(), out _))
                throw new InvalidGuidException($"L'id {id} n'est pas valide.");

            Abonne abonne = null;
            try
            {
                var collection = _database.GetCollection<Abonne>(ABONNE);
                abonne = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw ;
            }

            return abonne;
        }
    }
}
