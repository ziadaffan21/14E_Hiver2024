using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class ProjectionRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private const string PROJECTION = "Projections";

        public ProjectionRepository(IMongoClient mongoClient = null)
        {
            _mongoClient = mongoClient ?? DataBaseUtils.OuvrirConnexion();
            _mongoDatabase = DataBaseUtils.ConnectDatabase(_mongoClient);
        }
        public List<Projection> ReadProjections()
        {
            var projections = new List<Projection>();

            try
            {
                var collection = _mongoDatabase.GetCollection<Projection>(PROJECTION);
                projections = collection.Aggregate().ToList();
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la lecture de données des projections");
            }


            return projections;
        }

        public async Task AjouterProjection(Projection projection)
        {
            var tableProjection = _mongoDatabase.GetCollection<Projection>(PROJECTION);
            try
            {
                await tableProjection.InsertOneAsync(projection);
            }
            catch (Exception)
            {

                throw new MongoDataConnectionException("Une erreur s'est produite lors de l'ajout de la projection.");
            }
        }
        public List<Projection> ReadProjectionsById(Object idFilm)
        {
            //TODO : Trouver comment fair eune requete avec id à la BD.
            List<Projection> projections = ReadProjections();
            List<Projection> projectionsFiltre = new();

            foreach (var projection in projections)
            {
                if (projection.IdFilm.Equals(idFilm))
                {
                    projectionsFiltre.Add(projection);
                }
            }
            return projectionsFiltre;

        }
    }
}
