using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class RealisateurRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public RealisateurRepository(IMongoClient mongoClient = null)
        {
            _mongoClient = mongoClient ?? DataBaseUtils.OuvrirConnexion();
            _mongoDatabase = DataBaseUtils.ConnectDatabase(_mongoClient);
        }
    }
}
