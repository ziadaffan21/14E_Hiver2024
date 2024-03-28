using CineQuebec.Windows.DAL.Data.Personne;
using CineQuebec.Windows.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class ActeurRepository
    {
        private const string ACTEURS = "Acteurs";
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public ActeurRepository(IMongoClient mongoClient = null)
        {
            _mongoClient = mongoClient ?? DataBaseUtils.OuvrirConnexion();
            _mongoDatabase = DataBaseUtils.ConnectDatabase(_mongoClient);
        }

        public List<Acteur> ReadActeurs()
        {
            var acteurs = new List<Acteur>();

            try
            {
                var tableActeur = _mongoDatabase.GetCollection<Acteur>(ACTEURS);
                acteurs = tableActeur.Aggregate().ToList();

            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de l'ajout de la projection.");
            }

            return acteurs;
        }
    }
}
