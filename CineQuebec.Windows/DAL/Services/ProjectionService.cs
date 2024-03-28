using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Services
{
    public class ProjectionService : IProjectionService
    {
        private readonly ProjectionRepository _projectionRepository;

        public ProjectionService(ProjectionRepository projectionRepository)
        {
            _projectionRepository = projectionRepository;
        }

        public async Task AjouterProjection(Projection projection)
        {
            await _projectionRepository.AjouterProjection(projection);
        }

        public List<Projection> GetAllProjections()
        {
            return _projectionRepository.ReadProjections();
        }

        public List<Projection> ReadProjectionsById(ObjectId idFilm)
        {
            return _projectionRepository.ReadProjectionsById(idFilm);
        }
    }
}
