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
    public class AbonneRepository
    {
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;
        private const string ABONNE = "Abonnes";


        public AbonneRepository(IMongoClient mongoDBClient = null)
        {
            _mongoDBClient = mongoDBClient ?? DataBaseUtils.OuvrirConnexion();
            _database = DataBaseUtils.ConnectDatabase(_mongoDBClient);
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
    }
}
