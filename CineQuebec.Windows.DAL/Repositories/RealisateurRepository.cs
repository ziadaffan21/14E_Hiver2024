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
    public class RealisateurRepository : IRealisateurRepository
    {
        private const string REALISATEUR = "Realisateurs";
        private readonly IMongoClient _mongoBDClinet;
        private readonly IDataBaseUtils _dataBaseUtils;

        private readonly IMongoDatabase _mongoDataBase;

        public RealisateurRepository(IDataBaseUtils dataBaseUtils, IMongoClient mongoClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoBDClinet = mongoClient ?? _dataBaseUtils.OuvrirConnexion();
            _mongoDataBase = _dataBaseUtils.ConnectDatabase(_mongoBDClinet);
        }

        public async Task<List<Realisateur>> GetAll()
        {
            var collection = _mongoDataBase.GetCollection<Realisateur>(REALISATEUR);
            return await collection.Aggregate().ToListAsync();
        }
    }
}
