using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.FilmExceptions;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.Services;
using Moq;

namespace CineQuebec.Windows.Tests.ServiceTest
{
    public class FilmServiceTest
    {
        [Fact]
        public async Task AjouterFilm_ShouldThrowExistingFilmException_WhenFilmAlreadyExists()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            var existingFilm = new Film("XXX", DateTime.Now, 10, Categories.ANIMATION);
            mockRepository.Setup(repo => repo.GetFilmByTitre(existingFilm.Titre))
                          .ReturnsAsync(existingFilm);

            var service = new FilmService(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ExistingFilmException>(() => service.AjouterFilm(existingFilm));
        }

        [Fact]
        public async Task AjouterFilm_ShouldAddFilm_WhenFilmDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            mockRepository.Setup(repo => repo.GetFilmByTitre(It.IsAny<string>()))
                          .ReturnsAsync((Film)null);

            var service = new FilmService(mockRepository.Object);
            var newFilm = new Film("XXX", DateTime.Now, 10, Categories.ANIMATION);

            // Act
            await service.AjouterFilm(newFilm);

            // Assert
            mockRepository.Verify(repo => repo.AjouterFilm(newFilm));
        }

        [Fact]
        public async Task GetAllFilms_ShouldReturnListOfFilms()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            var expectedFilms = new List<Film>();
            mockRepository.Setup(repo => repo.ReadFilms())
                         .ReturnsAsync(expectedFilms);

            var service = new FilmService(mockRepository.Object);

            // Act
            var result = await service.GetAllFilms();

            // Assert
            Assert.Equal(expectedFilms, result);
        }

        [Fact]
        public async Task ModifierFilm_ShouldThrowArgumentNullException_WhenFilmIsNull()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            var service = new FilmService(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.ModifierFilm(null));
        }

        [Fact]
        public async Task ModifierFilm_ShouldCallModifierFilmInRepository_WhenFilmIsNotNull()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            var service = new FilmService(mockRepository.Object);
            var film = new Film("XXX", DateTime.Now, 10, Categories.ANIMATION);

            // Act
            await service.ModifierFilm(film);

            // Assert
            mockRepository.Verify(repo => repo.ModifierFilm(film)); // Verify that the method ModifierFilm in the repository was called once with the film parameter
        }
    }
}