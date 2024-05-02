using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using Moq;

namespace CineQuebec.Windows.Tests.RepositoryTest
{
    public class FilmRepositoryTest
    {
        [Fact]
        public async void GetAllAbonnes_ShouldReturnListOfAbonnes()
        {
            // Arrange
            var expectedFilms = new List<Film>
                {
                    new Film ("Titre",DateTime.Now,120,DAL.Enums.Categories.ACTION),
                    new Film("Titre1",DateTime.Now,120,DAL.Enums.Categories.ACTION)
                };
            var mockRepository = new Mock<IFilmRepository>();
            mockRepository.Setup(repo => repo.ReadFilms())
                          .ReturnsAsync(expectedFilms);

            // Act
            var result = mockRepository.Object.ReadFilms();


            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Film>>(result);
            Assert.Equal(expectedFilms, (IEnumerable<Film>)result);
        }

        [Fact]
        public async Task AjouterFilm_ShouldReturnTask_WhenFilmIsAdded()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            var film = new Film("Titre", DateTime.Now, 120, DAL.Enums.Categories.ACTION);
            mockRepository.Setup(repo => repo.AjouterFilm(film)).Returns(Task.CompletedTask);

            // Act
            await mockRepository.Object.AjouterFilm(film);

            // Assert
            mockRepository.Verify(repo => repo.AjouterFilm(film), Times.Once);
        }

        [Fact]
        public async Task ModifierFilm_ShouldReturnTask_WhenFilmIsModified()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            var film = new Film("Titre", DateTime.Now, 120, DAL.Enums.Categories.ACTION);
            mockRepository.Setup(repo => repo.ModifierFilm(film)).Returns(Task.CompletedTask);

            // Act
            await mockRepository.Object.ModifierFilm(film);

            // Assert
            mockRepository.Verify(repo => repo.ModifierFilm(film), Times.Once);
        }

        [Fact]
        public async void GetAllFilms_ShouldReturnListOfFilms()
        {
            // Arrange
            var expectedFilms = new List<Film>
                {
                    new Film("Titre", DateTime.Now, 120, DAL.Enums.Categories.ACTION),
                    new Film("Titre1", DateTime.Now, 120, DAL.Enums.Categories.ACTION)
                };
            var mockRepository = new Mock<IFilmRepository>();
            mockRepository.Setup(repo => repo.ReadFilms()).ReturnsAsync(expectedFilms);

            // Act
            var result = mockRepository.Object.ReadFilms();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Film>>(result);
            Assert.Equal(expectedFilms, (IEnumerable<Film>)result);
        }

        [Fact]
        public async Task GetFilmByTitre_ShouldReturnFilm_WhenTitreExists()
        {
            // Arrange
            var mockRepository = new Mock<IFilmRepository>();
            var expectedFilm = new Film("Titre", DateTime.Now, 120, DAL.Enums.Categories.ACTION);
            mockRepository.Setup(repo => repo.GetFilmByTitre("Titre")).ReturnsAsync(expectedFilm);

            // Act
            var result = await mockRepository.Object.GetFilmByTitre("Titre");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedFilm, result);
        }


    }
}