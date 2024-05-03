using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
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
            var existingProjection = await _projectionRepository.GetProjectionByDateAndFilmId(projection.Date, projection.Film.Titre);

            if (existingProjection is not null)
                throw new ExistingProjectionException($"Un projection avec la date {projection.Date.Year}/{projection.Date.Month}/{projection.Date.Day} pour le film {projection.Film.Titre} existe déjà.");


            await _projectionRepository.AjouterProjection(projection);
        }

        public List<Projection> GetAllProjections()
        {
            return _projectionRepository.ReadProjections();
        }

        public async Task<List<Projection>> GetProjectionsById(ObjectId idFilm)
        {
            return await _projectionRepository.ReadProjectionsById(idFilm);
        }

        public async Task<List<Projection>> GetProjectionByName(string name)
        {
            return await _projectionRepository.ReadProjectionsByName(name);
        }
    }
}