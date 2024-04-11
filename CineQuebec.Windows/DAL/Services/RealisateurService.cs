using CineQuebec.Windows.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Services
{
    public class RealisateurService
    {
        private readonly RealisateurRepository _realisateurRepository;

        public RealisateurService(RealisateurRepository realisateurRepository)
        {
            _realisateurRepository = realisateurRepository;
        }
    }
}
