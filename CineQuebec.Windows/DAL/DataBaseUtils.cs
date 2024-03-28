using CineQuebec.Windows.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL
{
    public static class DataBaseUtils
    {
        private const string CONNECTION_STRING_NAME = "Mongo";
        private const string DATABASE_STRING_NAME = "Database";
        public static IMongoClient OuvrirConnexion()
        {
            MongoClient dbClient = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
                dbClient = new MongoClient(connectionString);
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la connexion au serveur de base de donnée");
            }

            return dbClient;
        }

        public static IMongoDatabase ConnectDatabase(IMongoClient mongoDBClient)
        {
            IMongoDatabase db = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings[DATABASE_STRING_NAME].ConnectionString;
                db = mongoDBClient.GetDatabase(connectionString);
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la connexion à la base de donnée");
            }

            return db;
        }
    }
}
