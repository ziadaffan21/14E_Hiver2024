using CineQuebec.Windows.DAL.Data;
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

        public async Task<bool> GetAbonneConnexion(string username, string password)
        {
            return await _abonneRepository.GetAbonneConnexion(username, password);
        }
    }
}