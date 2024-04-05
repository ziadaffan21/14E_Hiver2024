using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.ServicesInterfaces;
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
    }
}
