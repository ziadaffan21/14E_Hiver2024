using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class ActeurRepository : IActeurRepository
    {
        private const string ACTEUR = "Acteurs";
        private readonly IMongoClient _mongoBDClinet;
        private readonly IDataBaseUtils _dataBaseUtils;

        private readonly IMongoDatabase _mongoDataBase;

        public ActeurRepository(IDataBaseUtils dataBaseUtils, IMongoClient mongoClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoBDClinet = mongoClient ?? _dataBaseUtils.OuvrirConnexion();
            _mongoDataBase = _dataBaseUtils.ConnectDatabase(_mongoBDClinet);
        }

        public async Task<List<Acteur>> GetAll()
        {
            var collection = _mongoDataBase.GetCollection<Acteur>(ACTEUR);
            return await collection.Aggregate().ToListAsync();
        }
    }
}
