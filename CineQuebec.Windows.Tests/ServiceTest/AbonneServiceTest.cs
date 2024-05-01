using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
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
            var abonneToAdd = new Abonne("XXX",DateTime.Now); // Create a dummy abonne object to add

            // Act
            var result = await service.Add(abonneToAdd);

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
        [Fact]
        public async Task AddActeurInAbonne_AddsActeur_WhenActeursCountDoesNotExceedLimit()
        {
            // Arrange
            var mockRepo = new Mock<IAbonneRepository>();
            var abonneId = new ObjectId();
            var acteur = new Acteur("ActeurName","NomActeur", DateTime.Now);
            mockRepo.Setup(repo => repo.GetAbonne(abonneId)).ReturnsAsync(new Abonne("Abonne", DateTime.Now) { Acteurs = new List<Acteur> { new Acteur("Acteur1","Actuers", DateTime.Now), new Acteur("Acteur2","efwef", DateTime.Now) } });

            var service = new AbonneService(mockRepo.Object); 

            // Act
            var result = await service.AddActeurInAbonne(abonneId, acteur);

            // Assert
            Assert.True(result);
            mockRepo.Verify(repo => repo.GetAbonne(abonneId), Times.Once);
        }
        [Fact]
        public async Task AddRealisateurInAbonne_AddsRealisateur_WhenRealisateursCountDoesNotExceedLimit()
        {
            // Arrange
            var mockRepo = new Mock<IAbonneRepository>();
            var abonneId = new ObjectId();
            var realisateur = new Realisateur("RealisateurName","RealisNom", DateTime.Now);
            mockRepo.Setup(repo => repo.GetAbonne(abonneId)).ReturnsAsync(new Abonne("Abonne", DateTime.Now) { Realisateurs = new List<Realisateur> { new Realisateur("Realisateur1","Realis", DateTime.Now), new Realisateur("Realisateur2", "Realis", DateTime.Now) } });

            var service = new AbonneService(mockRepo.Object);

            // Act
            var result = await service.AddRealisateurInAbonne(abonneId, realisateur);

            // Assert
            Assert.True(result);
            mockRepo.Verify(repo => repo.GetAbonne(abonneId), Times.Once);
        }
        [Fact]
        public async Task AddCategorieInAbonne_AddsCategorie_WhenCategoriesCountDoesNotExceedLimit()
        {
            // Arrange
            var mockRepo = new Mock<IAbonneRepository>();
            var abonneId = new ObjectId();
            var categorie = Categories.ACTION;
            mockRepo.Setup(repo => repo.GetAbonne(abonneId)).ReturnsAsync(new Abonne("Abonne", DateTime.Now) { CategoriesPrefere = new List<Categories> { Categories.ACTION, Categories.COMEDY } });

            var service = new AbonneService(mockRepo.Object);

            // Act
            var result = await service.AddCategorieInAbonne(abonneId, categorie);

            // Assert
            Assert.True(result);
            mockRepo.Verify(repo => repo.GetAbonne(abonneId), Times.Once);
        }
    }
}