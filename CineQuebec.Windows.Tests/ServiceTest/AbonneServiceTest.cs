using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using MongoDB.Bson;
using Moq;
using System.Runtime.Intrinsics.X86;

namespace CineQuebec.Windows.Tests.ServiceTest
{
    public class AbonneServiceTest
    {
        [Fact]
        public void GetAllAbonnes_ShouldReturnListOfAbonnes()
        {
            // Arrange
            var mockRepository = new Mock<IAbonneRepository>();
            mockRepository.Setup(repo => repo.ReadAbonnes())
                          .Returns(new List<Abonne>());

            var service = new AbonneService(mockRepository.Object);

            // Act
            var result = service.GetAllAbonnes();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Abonne>>(result);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async Task Add_ShouldReturnTrueIfSuccessfullyAdded(bool actionValide, bool resultatAttendu)
        {
            // Arrange
            var mockRepository = new Mock<IAbonneRepository>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<Abonne>()))
                          .ReturnsAsync(actionValide);

            var service = new AbonneService(mockRepository.Object);
            var abonneToAdd = new Mock<Abonne>(); // Create a dummy abonne object to add

            // Act
            var result = await service.Add(abonneToAdd.Object);

            // Assert
            Assert.Equal(result, resultatAttendu);
        }

        [Fact]
        public async Task GetAbonne_ShouldReturnAbonne()
        {
            // Arrange
            var mockRepository = new Mock<IAbonneRepository>();
            ObjectId id = ObjectId.GenerateNewId();

            var service = new AbonneService(mockRepository.Object);

            // Act
            var result = await service.GetAbonne(id);

            // Assert
            mockRepository.Verify(x=>x.GetAbonne(id));
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async Task GetAbonneConnexion_ShouldReturnTrueOrFalse(bool actionValide, bool resultatAttendu)
        {
            // Arrange
            var mockRepository = new Mock<IAbonneRepository>();
            string username = "testUser";
            string password = "testPassword";
            mockRepository.Setup(repo => repo.GetAbonneConnexion(username, password))
                          .ReturnsAsync(actionValide); // Configurer le mock pour retourner le résultat attendu

            var service = new AbonneService(mockRepository.Object);

            // Act
            var result = await service.GetAbonneConnexion(username, password);

            // Assert
            Assert.Equal(result, resultatAttendu);
        }
    }
}