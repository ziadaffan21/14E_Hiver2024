using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.Services;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Windows.Tests.ServiceTest
{
    public class ProjectionServiceTest
    {
        [Fact]
        public async Task AjouterProjection_ShouldThrowExistingProjectionException_WhenProjectionAlreadyExists()
        {
            // Arrange
            var mockRepository = new Mock<IProjectionRepository>();
            var existingProjection = new Projection() { Date = DateTime.Today, Film = new Film("XXX",DateTime.Now,10,Categories.ANIMATION) };
            mockRepository.Setup(repo => repo.GetProjectionByDateAndFilmId(existingProjection.Date, existingProjection.Film.Titre))
                          .ReturnsAsync(existingProjection); // Configure the mock to return an existing projection

            var service = new ProjectionService(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ExistingProjectionException>(() => service.AjouterProjection(existingProjection));
        }

        [Fact]
        public async Task AjouterProjection_ShouldAddProjection_WhenProjectionDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IProjectionRepository>();
            mockRepository.Setup(repo => repo.GetProjectionByDateAndFilmId(It.IsAny<DateTime>(), It.IsAny<string>()))
                          .ReturnsAsync((Projection)null);

            var service = new ProjectionService(mockRepository.Object);
            var newProjection = new Projection() { Date = DateTime.Today, Film = new Film("XXX", DateTime.Now, 10, Categories.ANIMATION) };

            // Act
            await service.AjouterProjection(newProjection);

            // Assert
            mockRepository.Verify(repo => repo.AjouterProjection(newProjection)); // Verify that the projection was added to the repository
        }

        [Fact]
        public void GetAllProjections_ShouldReturnListOfProjections()
        {
            // Arrange
            var mockRepository = new Mock<IProjectionRepository>();
            var expectedProjections = new List<Projection> (); // Create a list of expected projections
            mockRepository.Setup(repo => repo.ReadProjections())
                          .Returns(expectedProjections); // Configure the mock to return the list of expected projections

            var service = new ProjectionService(mockRepository.Object);

            // Act
            var result = service.GetAllProjections();

            // Assert
            Assert.Equal(expectedProjections, result); // Verify that the returned list of projections matches the expected list
        }

    }
}