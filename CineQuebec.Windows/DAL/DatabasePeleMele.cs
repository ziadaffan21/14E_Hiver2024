using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL
{
    public class DatabasePeleMele
    {
        private IMongoClient mongoDBClient;
        private IMongoDatabase database;

        public DatabasePeleMele(IMongoClient client = null)
        {
            mongoDBClient = client ?? OuvrirConnexion();
            database = ConnectDatabase();
        }

        private IMongoClient OuvrirConnexion()
        {
            MongoClient dbClient = null;
            try
            {
                dbClient = new MongoClient("mongodb://localhost:27017/");
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite : ${ex}");
            }
            return dbClient;
        }

        private IMongoDatabase ConnectDatabase()
        {
            IMongoDatabase db = null;
            try
            {
                db = mongoDBClient.GetDatabase("TP2DB");
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite : ${ex}");
            }
            return db;
        }

        public List<Abonne> ReadAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                var collection = database.GetCollection<Abonne>("Abonnes");
                abonnes = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite : ${ex}");
            }
            return abonnes;
        }

        public List<Film> ReadFilms()
        {
            var films = new List<Film>();
            try
            {
                var collection = database.GetCollection<Film>("Films");
                films = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite : ${ex}");

            }
            return films;
        }
    }
}
