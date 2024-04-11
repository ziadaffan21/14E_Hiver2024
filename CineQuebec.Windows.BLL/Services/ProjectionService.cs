using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Services
{
    public class ProjectionService : IProjectionService
    {
        private readonly IProjectionRepository _projectionRepository;

        public ProjectionService(IProjectionRepository projectionRepository)
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