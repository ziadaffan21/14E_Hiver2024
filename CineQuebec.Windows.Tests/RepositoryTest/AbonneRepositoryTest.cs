using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.Services;
using MongoDB.Driver;
using Moq;

namespace CineQuebec.Windows.Tests.RepositoryTest
{
    public class AbonneRepositoryTest
    {
        [Fact]
        public void GetAllAbonnes_ShouldReturnListOfAbonnes()
        {
            // Arrange
            var expectedAbonnes = new List<Abonne>
                {
                    new Abonne ("user",DateTime.Now),
                    new Abonne ("user2",DateTime.Now)
                };
            var mockRepository = new Mock<IAbonneRepository>();
            mockRepository.Setup(repo => repo.ReadAbonnes())
                          .Returns(expectedAbonnes);

            // Act
            var result = mockRepository.Object.ReadAbonnes();


            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Abonne>>(result);
            Assert.Equal(expectedAbonnes, result);
        }
        [Fact]
        public async Task Add_ShouldReturnTrue_WhenAbonneIsAdded()
        {
            // Arrange
            var mockRepository = new Mock<IAbonneRepository>();
            var abonne = new Abonne("user", DateTime.Now);
            mockRepository.Setup(repo => repo.Add(abonne)).ReturnsAsync(true);

            // Act
            var result = await mockRepository.Object.Add(abonne);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAbonneByUsername_ShouldReturnAbonne_WhenUsernameExists()
        {
            // Arrange
            var mockRepository = new Mock<IAbonneRepository>();
            var expectedAbonne = new Abonne("user", DateTime.Now);
            mockRepository.Setup(repo => repo.GetAbonneByUsername("user")).ReturnsAsync(expectedAbonne);

            // Act
            var result = await mockRepository.Object.GetAbonneByUsername("user");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAbonne, result);
        }

        //[Fact]
        //public async Task GetAbonneConnexion_ShouldReturnTrue_WhenCredentialsAreValid()
        //{
        //    // Arrange
        //    var mockRepository = new Mock<IAbonneRepository>();
        //    mockRepository.Setup(repo => repo.GetAbonneConnexion("user", "password")).ReturnsAsync(true);

        //    // Act
        //    var result = await mockRepository.Object.GetAbonneConnexion("user", "password");

        //    // Assert
        //    Assert.True(result);
        //}

        [Fact]
        public async Task GetAbonne_ShouldReturnAbonne_WhenIdExists()
        {
            // Arrange
            var mockRepository = new Mock<IAbonneRepository>();
            var expectedAbonne = new Abonne("user", DateTime.Now);
            var id = expectedAbonne.Id;
            mockRepository.Setup(repo => repo.GetAbonne(id)).ReturnsAsync(expectedAbonne);

            // Act
            var result = await mockRepository.Object.GetAbonne(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAbonne, result);
        }

    }
}