using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Data.Personne;
using CineQuebec.Windows.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CineQuebec.Windows.DAL
{
    public class DatabasePeleMele
    {
        private const string CONNECTION_STRING_NAME = "Mongo";
        private const string DATABASE_STRING_NAME = "Database";
        private const string ABONNE = "Abonnes";
        private const string FILMS = "Films";
        private const string PROJECTION = "Projections";
        private const string ACTEURS = "Acteurs";
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
                string connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
                dbClient = new MongoClient(connectionString);
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la connexion au serveur de base de donnée");
            }

            return dbClient;
        }

        private IMongoDatabase ConnectDatabase()
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

        public List<Abonne> ReadAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                var collection = database.GetCollection<Abonne>(ABONNE);
                abonnes = collection.Aggregate().ToList();
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la lecture de données d'abonnés");
            }
            return abonnes;
        }

        public List<Film> ReadFilms()
        {
            var films = new List<Film>();
            try
            {
                var collection = database.GetCollection<Film>(FILMS);
                films = collection.Aggregate().ToList();
            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la lecture de données des films");

            }
            return films;
        }

        public async Task ModifierFilm(Film film)
        {
            var tableFilm = database.GetCollection<Film>(FILMS);
            var filter = Builders<Film>.Filter.Eq(f => f.Id, film.Id);
            try
            {
                await tableFilm.ReplaceOneAsync(filter, film);
            }
            catch (Exception)
            {
                
                throw new MongoDataConnectionException("Une erreur s'est produite lors de la modification du film.");
            }
        }

        public async Task AjouterFilm(Film film)
        {
            var tableFilm = database.GetCollection<Film>(FILMS);
            try
            {
                await tableFilm.InsertOneAsync(film);
            }
            catch (Exception)
            {

                throw new MongoDataConnectionException("Une erreur s'est produite lors de la modification du film.");
            }
        }

        public async Task AjouterProjection(Projection projection)
        {
            var tableProjection = database.GetCollection<Projection>(PROJECTION);
            try
            {
                await tableProjection.InsertOneAsync(projection);
            }
            catch (Exception)
            {

                throw new MongoDataConnectionException("Une erreur s'est produite lors de l'ajout de la projection.");
            }
        }
        public List<Acteur> ReadActeurs()
        {
            var acteurs = new List<Acteur>();

            try
            {
                var tableActeur = database.GetCollection<Acteur>(ACTEURS);
                acteurs =  tableActeur.Aggregate().ToList();

            }
            catch (Exception)
            {
                throw new MongoDataConnectionException("Une erreur s'est produite lors de l'ajout de la projection.");
            }

            return acteurs;
        }
    }
}
