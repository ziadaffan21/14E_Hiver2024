using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System.Text.Json;
using System;
using System.IO;
using System.Text.Json.Nodes;

namespace CineQuebec.Windows.DAL
{
    public class DataBaseSeeder : IDataBaseSeeder
    {
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;
        private readonly IDataBaseUtils _dataBaseUtils;
        private readonly Film film1 = new Film("The Shawshank Redemption", DateTime.Now, 120, Categories.ANIMATION);
        private readonly Film film2 = new Film("The Godfather", DateTime.Now, 120, Categories.ADVENTURE);


        private string CheminSeed(string nom)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            basePath = basePath.Replace(" ", "_");

            string fullPath = Path.Combine( "Ressources", "Seeds", $"{nom}Seed.json");
            return fullPath;
        }

        public DataBaseSeeder(IDataBaseUtils dataBaseUtils, IMongoClient mongoDBClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoDBClient = mongoDBClient ?? _dataBaseUtils.OuvrirConnexion();
            _database = _dataBaseUtils.ConnectDatabase(_mongoDBClient);
            _dataBaseUtils = dataBaseUtils;
        }

        public async Task Seed()
        {

            

            await SeedFilms();
            await SeedAbonnes();
            await SeedActeur();
            await SeedRealisateur();
            await SeedProjection();
        }

        private async Task SeedFilms()
        {
            var filmsCollection = _database.GetCollection<BsonDocument>("Films");

            await filmsCollection.DeleteManyAsync(new BsonDocument());

            var chemin = CheminSeed("Films");

            var films = new List<Film>();

            if (!File.Exists(chemin))
            {
                return;
            }


            var jsonContent = File.ReadAllText(chemin);
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDoc.RootElement;

            foreach (JsonElement Element in root.EnumerateArray())
            {
                Film film = ConvertToFilm(Element);
                films.Add(film);
            }

            var bson = films.Select(film => film.ToBsonDocument()).ToList();
            await filmsCollection.InsertManyAsync(bson);
        }

        private static Film ConvertToFilm(JsonElement Element)
        {
            ObjectId id = new ObjectId(Element.GetProperty("_id").GetProperty("$oid").GetString());
            string titre = Element.GetProperty("Titre").GetString();
            int duree = Element.GetProperty("Duree").GetInt32();
            string dateString = Element.GetProperty("DateSortie").GetProperty("$date").GetString();
            var date = DateTime.Parse(dateString);
            int categorie = Element.GetProperty("Categorie").GetInt32();
            bool estAffiche = Element.GetProperty("EstAffiche").GetBoolean();


            var film = new Film(titre, date, duree, (Categories)categorie, id, estAffiche);
            return film;
        }

        private async Task SeedAbonnes()
        {
            var abonneCollection = _database.GetCollection<BsonDocument>("Abonnes");
            await abonneCollection.DeleteManyAsync(new BsonDocument());

            var abonnes = new List<Abonne>();

            var chemin = CheminSeed("Abonnes");

            if (!File.Exists(chemin))
            {
                return;
            }


            var jsonContent = File.ReadAllText(chemin);
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDoc.RootElement;

            foreach (JsonElement Element in root.EnumerateArray())
            {
                ObjectId id = new ObjectId(Element.GetProperty("_id").GetProperty("$oid").GetString());
                string username = Element.GetProperty("Username").GetString();
                string passwordBase64 = Element.GetProperty("Password").GetProperty("$binary").GetProperty("base64").GetString();
                byte[] passwordBytes = Convert.FromBase64String(passwordBase64);
                string saltBase64 = Element.GetProperty("Salt").GetProperty("$binary").GetProperty("base64").GetString();
                byte[] saltBytes = Convert.FromBase64String(saltBase64);
                bool isAdmin = Element.GetProperty("isAdmin").GetBoolean();

                // Creating the Abonne instance
                string dateString = Element.GetProperty("DateAdhesion").GetProperty("$date").GetString();
                var date = DateTime.Parse(dateString);
                

                var abonne = new Abonne(id,username, passwordBytes, saltBytes, date, isAdmin);
                abonnes.Add(abonne);
            }

            var bson = abonnes.Select(abonne => abonne.ToBsonDocument()).ToList();
            await abonneCollection.InsertManyAsync(bson);
        }

        private async Task SeedActeur()
        {
            var acteurCollection = _database.GetCollection<BsonDocument>("Acteurs");
            await acteurCollection.DeleteManyAsync(new BsonDocument());
            var chemin = CheminSeed("Acteurs");

            var acteurs = new List<Acteur>();


            if (!File.Exists(chemin))
            {
                return;
            }


            var jsonContent = File.ReadAllText(chemin);
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDoc.RootElement;

            foreach (JsonElement Element in root.EnumerateArray())
            {
                ObjectId id = new ObjectId(Element.GetProperty("_id").GetProperty("$oid").GetString());
                string nom = Element.GetProperty("Nom").GetString();
                string prenom = Element.GetProperty("Prenom").GetString();
                string dateString = Element.GetProperty("Naissance").GetProperty("$date").GetString();
                var naissance = DateTime.Parse(dateString);


                var acteur = new Acteur(prenom,nom, naissance,id);
                acteurs.Add(acteur);
            }

            var bson = acteurs.Select(acteurs => acteurs.ToBsonDocument()).ToList();
            await acteurCollection.InsertManyAsync(bson);

        }

        private async Task SeedProjection()
        {
            var projectionCollection = _database.GetCollection<BsonDocument>("Projections");
            await projectionCollection.DeleteManyAsync(new BsonDocument());


            var projections = new List<Projection>();
            var chemin = CheminSeed("Projections");


            if (!File.Exists(chemin))
            {
                return;
            }


            var jsonContent = File.ReadAllText(chemin);
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDoc.RootElement;

            foreach (JsonElement Element in root.EnumerateArray())
            {
                ObjectId id = new ObjectId(Element.GetProperty("_id").GetProperty("$oid").GetString());
                string dateString = Element.GetProperty("Date").GetProperty("$date").GetString();
                var date = DateTime.Parse(dateString);
                int nbPlaces = Element.GetProperty("NbPlaces").GetInt32();
                var film = ConvertToFilm(Element.GetProperty("Film"));

                var reservationsArray = Element.GetProperty("Reservations");


                List<ObjectId> reservations = new();
                foreach (var reservation in reservationsArray.EnumerateArray())
                {
                    var userId = reservation.GetProperty("$oid").GetString();
                    reservations.Add(new(userId));
                }

                var projection = new Projection(date, nbPlaces, film, id,reservations);
                projections.Add(projection);
            }

            var bson = projections.Select(projection => projection.ToBsonDocument()).ToList();
            await projectionCollection.InsertManyAsync(bson);

        }

        private async Task SeedRealisateur()
        {
            var realisateurCollection = _database.GetCollection<BsonDocument>("Realisateurs");
            await realisateurCollection.DeleteManyAsync(new BsonDocument());

            var realisateurs = new List<Realisateur>();

            var chemin = CheminSeed("Realisateurs");

            if (!File.Exists(chemin))
            {
                return;
            }


            var jsonContent = File.ReadAllText(chemin);
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDoc.RootElement;

            foreach (JsonElement Element in root.EnumerateArray())
            {
                ObjectId id = new ObjectId(Element.GetProperty("_id").GetProperty("$oid").GetString());
                string nom = Element.GetProperty("Nom").GetString();
                string prenom = Element.GetProperty("Prenom").GetString();
                string dateString = Element.GetProperty("Naissance").GetProperty("$date").GetString();
                var naissance = DateTime.Parse(dateString);


                var realisateur = new Realisateur(prenom, nom, naissance, id);
                realisateurs.Add(realisateur);
            }

            var bson = realisateurs.Select(acteurs => acteurs.ToBsonDocument()).ToList();
            await realisateurCollection.InsertManyAsync(bson);
        }
    }
}