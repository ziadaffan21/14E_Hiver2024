using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bool result = await _abonneRepository.Add(abonne);
            return result;
        }

        public Task<Abonne> GetAbonne(ObjectId id)
        {
            return _abonneRepository.GetAbonne(id);
        }

        public async Task<bool> GetAbonneConnexion(string username, string password)
        {
            return await _abonneRepository.GetAbonneConnexion(username, password);
        }
    }
}
