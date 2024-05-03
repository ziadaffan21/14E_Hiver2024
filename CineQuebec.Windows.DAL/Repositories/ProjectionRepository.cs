using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IDataBaseUtils _dataBaseUtils;

        private const string PROJECTION = "Projections";

        public ProjectionRepository(IDataBaseUtils dataBaseUtils, IMongoClient mongoClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoClient = mongoClient ?? _dataBaseUtils.OuvrirConnexion();
            _mongoDatabase = _dataBaseUtils.ConnectDatabase(_mongoClient);
        }

        public List<Projection> ReadProjections()
        {
            var collection = _mongoDatabase.GetCollection<Projection>(PROJECTION);
            List<Projection> projections = collection.Aggregate().ToList();

            return projections;
        }

        public async Task AjouterProjection(Projection projection)
        {
            var tableProjection = _mongoDatabase.GetCollection<Projection>(PROJECTION);

            await tableProjection.InsertOneAsync(projection);
        }

        //public Task<List<Projection>> ReadProjectionsById(Object idFilm)
        //{

        //    var filter = Builders<Projection>.Filter.And(
        //           //Builders<Projection>.Filter.Gt(p => p.Date, DateTime.Now),
        //           Builders<Projection>.Filter.Eq(p => p.Film.Id, idFilm)
        //       );

        //    var collection = _mongoDatabase.GetCollection<Projection>(PROJECTION);
        //    var projections = collection.Find(filter).ToListAsync();

        //    return projections;
        //}

        public async Task<List<Projection>> ReadProjectionsById(Object idFilm)
        {
            List<Projection> projections = await Task.Run(() => ReadProjections());

            List<Projection> projectionsFiltre = new();
            
            foreach (var projection in projections)
            {
                if (projection.Film.Id.Equals(idFilm))
                {
                    projectionsFiltre.Add(projection);
                }
            }

            return projectionsFiltre;
        }

        public async Task<Projection> GetProjectionByDateAndFilmId(DateTime dateProjection, string titreFilm)
        {
            var projectionCollection = _mongoDatabase.GetCollection<Projection>(PROJECTION);

            var filter = Builders<Projection>.Filter.And(
                Builders<Projection>.Filter.Eq(p => p.Date, dateProjection),
                Builders<Projection>.Filter.Eq(p => p.Film.Titre, titreFilm)
            );

            var projection = await projectionCollection.FindAsync(filter);

            var projectionList = await projection.ToListAsync();

            return projectionList.FirstOrDefault();
        }

        public async Task<List<Projection>> ReadProjectionsByName(string name)
        {
            var collection = _mongoDatabase.GetCollection<Projection>(PROJECTION);

            // Construct a filter to find projections whose associated film's name matches the provided name
            var filter = Builders<Projection>.Filter.Eq(p => p.Film.Titre, name);

            // Retrieve projections from the database that match the filter
            var projections = await collection.Find(filter).ToListAsync();

            return projections;
        }

    }
}