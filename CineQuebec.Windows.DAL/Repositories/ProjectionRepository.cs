﻿using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
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

        public async Task AjouterReservation(ObjectId projectionId, ObjectId userId)
        {
            var tableProjection = _mongoDatabase.GetCollection<Projection>(PROJECTION);
            var filter = Builders<Projection>.Filter.Eq(p => p.Id, projectionId); // Assurez-vous d'ajuster la propriété Id selon votre modèle de données
            var update = Builders<Projection>.Update.AddToSet(p => p.Reservations, userId);
            var projection = await GetProjectionById(projectionId);


            //Si les places sont remplie, la fonction est annulé
            if (projection.PlaceDisponible())
            {
                return;
            }

            var reponse = await tableProjection.UpdateOneAsync(filter, update);           
        }

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

            var projection = await projectionCollection.Find(filter).FirstOrDefaultAsync();


            return projection;
        }

        public async Task<Projection> GetProjectionById(ObjectId projectionId)
        {
            var projectionCollection = _mongoDatabase.GetCollection<Projection>(PROJECTION);
            var filter = Builders<Projection>.Filter.Eq(p => p.Id, projectionId); 
            var projection = await projectionCollection.Find(filter).FirstOrDefaultAsync();

            return projection;
        }

        public async Task<List<Projection>> GetProjectionsByName(string name)
        {
            var collection = _mongoDatabase.GetCollection<Projection>(PROJECTION);
            var filter = Builders<Projection>.Filter.Eq(p => p.Film.Titre, name);

            var projections = await collection.Find(filter).ToListAsync();

            return projections;
        }

      
    }
}