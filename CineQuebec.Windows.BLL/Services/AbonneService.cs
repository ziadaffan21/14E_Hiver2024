using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.AbonneExceptions;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Services
{
    public class AbonneService : IAbonneService
    {
        private readonly IAbonneRepository _abonneRepository;

        public AbonneService(IAbonneRepository abonneRepository)
        {
            _abonneRepository = abonneRepository;
        }

        public List<Abonne> GetAllAbonnes()
        {
            List<Abonne> abonnes= _abonneRepository.ReadAbonnes();
            abonnes.Sort();
            return abonnes;
        }

        public async Task<bool> Add(Abonne abonne)
        {
            var existingAbonne = await _abonneRepository.GetAbonneByUsername(abonne.Username);

            if (existingAbonne is not null)
                throw new ExistingAbonneException($"L'abonne avec le username '{abonne.Username}' exite déjà");

            return await _abonneRepository.Add(abonne);
        }

        public async Task<Abonne> GetAbonneConnexion(string username, string password)
        {
            return await _abonneRepository.GetAbonneConnexion(username, password);
        }

        public async Task<Abonne> GetAbonne(ObjectId id)
        {
            return await _abonneRepository.GetAbonne(id);
        }

        public async Task<Abonne> AddActeurInAbonne(Abonne abonne, Acteur acteur)
        {
            if (abonne.Acteurs.Contains(acteur))
                throw new ActeurAlreadyExistInActeursList($"L'acteur {acteur.Prenom} {acteur.Nom} exite déjà dans la liste acteurs");
            if (abonne.Acteurs.Count >= 5)
                throw new NumberActeursOutOfRange("Vous ne pouvez pas ajouter plus que 5 acteurs");

            abonne.Acteurs.Add(acteur);
            var result = await _abonneRepository.UpdateOne(abonne);

            return result;
        }

        public async Task<Abonne> AddRealisateurInAbonne(Abonne abonne, Realisateur realisateur)
        {
            if (abonne.Realisateurs.Contains(realisateur))
                throw new RealisateurAlreadyExistInRealisateursList($"Le réalisateur {realisateur.Prenom} {realisateur.Nom} exite déjà dans la liste réalisateurs");
            if (abonne.Realisateurs.Count >= 5)
                throw new NumberRealisateursOutOfRange("Vous ne pouvez pas ajouter plus que 5 realisateurs");

            abonne.Realisateurs.Add(realisateur);
            var result = await _abonneRepository.UpdateOne(abonne);

            return result;
        }

        public async Task<Abonne> AddCategorieInAbonne(Abonne abonne, Categories categorie)
        {
            if (abonne.CategoriesPrefere.Contains(categorie))
                throw new CategorieAlreadyExistInCategoriesList($"La catégorie {categorie.ToString()} exite déjà dans la liste catégories");
            if (abonne.CategoriesPrefere.Count >= 3)
                throw new NumberCategoriesOutOfRange("Vous pouvez pas ajouter plus que 3 categories");

            abonne.CategoriesPrefere.Add(categorie);
            var result = await _abonneRepository.UpdateOne(abonne);

            return result;
        }

        public async Task<Abonne> AddFilmInAbonne(Abonne abonne, Film film)
        {
            if (abonne.Films.Contains(film))
                throw new FilmAlreadyExistInFilmsList($"Le film {film.Titre} exite déjà dans la liste films");

            abonne.Films.Add(film);
            var result = await _abonneRepository.UpdateOne(abonne);

            return result;
        }

        public async Task<Abonne> RemoveActeurInAbonne(Abonne abonne, Acteur acteur)
        {
            if (abonne.Acteurs.Remove(acteur))
                return await _abonneRepository.UpdateOne(abonne);

            return abonne;
        }

        public async Task<Abonne> RemoveRealisateurInAbonne(Abonne abonne, Realisateur realisateur)
        {
            if (abonne.Realisateurs.Remove(realisateur))
                return await _abonneRepository.UpdateOne(abonne); ;

            return abonne;
        }

        public async Task<Abonne> RemoveCategorieInAbonne(Abonne abonne, Categories categorie)
        {
            if (abonne.CategoriesPrefere.Remove(categorie))
                return await _abonneRepository.UpdateOne(abonne);

            return abonne;
        }

        public async Task<Abonne> RemoveFilmInAbonne(Abonne abonne, Film film)
        {
            if (abonne.Films.Remove(film))
                return await _abonneRepository.UpdateOne(abonne);

            return abonne;
        }
    }
}