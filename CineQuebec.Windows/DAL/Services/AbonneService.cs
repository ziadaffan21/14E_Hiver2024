using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories;
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
        private readonly AbonneRepository _abonneRepository;

        public AbonneService(AbonneRepository abonneRepository)
        {
            _abonneRepository = abonneRepository;
        }
        public List<Abonne> GetAllAbonnes()
        {
            return _abonneRepository.ReadAbonnes();
        }
    }
}
