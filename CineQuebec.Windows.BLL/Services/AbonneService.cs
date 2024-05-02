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
            return _abonneRepository.ReadAbonnes();
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

        public async Task<bool> AddActeurInAbonne(ObjectId abonneId, Acteur acteur)
        {
            var abonne = await GetAbonne(abonneId);
            if (abonne.Acteurs.Contains(acteur))
                throw new ActeurAlreadyExistInRealisateurList($"L'acteur {acteur.Prenom} {acteur.Nom} exite déjà dans la liste acteurs");
            if (abonne.Acteurs.Count >= 5)
                throw new NumberActeursOutOfRange("Vous ne pouvez pas ajouter plus que 5 acteurs");


            abonne.Acteurs.Add(acteur);
            var result = await _abonneRepository.UpdateAbonne(abonne);
            if (!result)
                return false;

            return true;
        }

        public async Task<bool> AddRealisateurInAbonne(ObjectId abonneId, Realisateur realisateur)
        {
            var abonne = await GetAbonne(abonneId);
            if(abonne.Realisateurs.Contains(realisateur))
                throw new RealisateurAlreadyExistInRealisateurList($"Le réalisateur {realisateur.Prenom} {realisateur.Nom} exite déjà dans la liste acteurs");
            if (abonne.Realisateurs.Count >= 5)
                throw new NumberRealisateursOutOfRange("Vous ne pouvez pas ajouter plus que 5 realisateurs");


            abonne.Realisateurs.Add(realisateur);
            var result = await _abonneRepository.UpdateAbonne(abonne);
            if (!result)
                return false;

            return true;
        }

        public async Task<bool> AddCategorieInAbonne(ObjectId abonneId, Categories categorie)
        {
            var abonne = await GetAbonne(abonneId);
            if(abonne.CategoriesPrefere.Contains(categorie))
                throw new CategorieAlreadyExistInRealisateurList($"La catégorie {categorie.ToString()} exite déjà dans la liste acteurs");
            if (abonne.CategoriesPrefere.Count >= 3)
                throw new NumberCategoriesOutOfRange("Vous pouvez pas ajouter plus que 3 categories");


            abonne.CategoriesPrefere.Add(categorie);
            var result = await _abonneRepository.UpdateAbonne(abonne);
            if (!result)
                return false;

            return true;
        }

        public async Task<bool> AddFilmInAbonne(ObjectId abonneId, Film film)
        {
            var abonne = await GetAbonne(abonneId);

            if(abonne.Films.Contains(film))
                throw new FilmAlreadyExistInRealisateurList($"Le film {film.Titre} exite déjà dans la liste acteurs");


            abonne.Films.Add(film);
            var result = await _abonneRepository.UpdateAbonne(abonne);
            if (!result)
                return false;

            return true;
        }

        public async Task<bool> RemoveActeurInAbonne(ObjectId abonneId, Acteur acteur)
        {
            var abonne = await GetAbonne(abonneId);

            if (abonne.Acteurs.Remove(acteur))
            {
                var result = await _abonneRepository.UpdateAbonne(abonne);
                if (!result)
                    return false;
            }
            return true;
        }

        public async Task<bool> RemoveRealisateurInAbonne(ObjectId abonneId, Realisateur realisateur)
        {
            var abonne = await GetAbonne(abonneId);

            if (abonne.Realisateurs.Remove(realisateur))
            {
                var result = await _abonneRepository.UpdateAbonne(abonne);
                if (!result)
                    return false;
            }
            return true;
        }

        public async Task<bool> RemoveCategorieInAbonne(ObjectId abonneId, Categories categorie)
        {
            var abonne = await GetAbonne(abonneId);

            if (abonne.CategoriesPrefere.Remove(categorie))
            {
                var result = await _abonneRepository.UpdateAbonne(abonne);
                if (!result)
                    return false;
            }
            return true;
        }

        public async Task<bool> RemoveFilmInAbonne(ObjectId abonneId, Film film)
        {
            var abonne = await GetAbonne(abonneId);

            if (abonne.Films.Remove(film))
            {
                var result = await _abonneRepository.UpdateAbonne(abonne);
                if (!result)
                    return false;
            }
            return true;
        }
    }
}