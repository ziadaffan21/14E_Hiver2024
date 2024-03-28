using CineQuebec.Windows.DAL.Data.Personne;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Services
{
    public class ActeurService : IActeurService
    {
        private readonly ActeurRepository _acteurRepository;

        public ActeurService(ActeurRepository acteurRepository)
        {
            _acteurRepository = acteurRepository;
        }

        public List<Acteur> GetAllActeurs()
        {
            return _acteurRepository.ReadActeurs();
        }
    }
}
