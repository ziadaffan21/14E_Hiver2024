using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Exceptions.AbonneExceptions;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
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
            var abonneToAdd = new Abonne("XXX", DateTime.Now); // Create a dummy abonne object to add

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
            mockRepository.Verify(x => x.GetAbonne(id));
        }

        [Fact]
        public async Task AddActeurInAbonne_AddsActeur_Successfully()
        {
            // Arrange
            var mockAbonneRepository = new Mock<IAbonneRepository>();
            var abonne = new Abonne { Id = ObjectId.GenerateNewId(), Username = "username", Acteurs = new List<Acteur>() };
            var acteur = new Acteur { Prenom = "John123", Nom = "Doe123" };
            mockAbonneRepository.Setup(repo => repo.UpdateOne(It.IsAny<Abonne>())).ReturnsAsync(abonne);

            var service = new AbonneService(mockAbonneRepository.Object);

            // Act
            var result = await service.AddActeurInAbonne(abonne, acteur);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(abonne.Id, result.Id); // Vérifie que l'ID de l'abonné a été conservé
            Assert.Contains(acteur, result.Acteurs); // Vérifie que l'acteur a été ajouté à la liste des acteurs
            mockAbonneRepository.Verify(repo => repo.UpdateOne(It.IsAny<Abonne>()), Times.Once);
        }

        [Fact]
        public async Task AddActeurInAbonne_ThrowsActeurAlreadyExistInActeursList_WhenActeurExists()
        {
            // Arrange
            var mockAbonneRepository = new Mock<IAbonneRepository>();
            var abonne = new Abonne { Id = ObjectId.GenerateNewId(), Username = "username", Acteurs = new List<Acteur> { new Acteur { Prenom = "John123", Nom = "Doe123" } } };
            var acteur = new Acteur { Prenom = "John123", Nom = "Doe123" };
            mockAbonneRepository.Setup(repo => repo.UpdateOne(It.IsAny<Abonne>()))
                               .ThrowsAsync(new ActeurAlreadyExistInActeursList("L'acteur existe déjà dans la liste des acteurs"));

            var service = new AbonneService(mockAbonneRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ActeurAlreadyExistInActeursList>(() => service.AddActeurInAbonne(abonne, acteur));
        }

        [Fact]
        public async Task AddActeurInAbonne_ThrowsNumberActeursOutOfRange_WhenMaxActeursReached()
        {
            // Arrange
            var mockAbonneRepository = new Mock<IAbonneRepository>();
            var abonne = new Abonne { Id = ObjectId.GenerateNewId(), Acteurs = new List<Acteur> { new Acteur { Id = ObjectId.GenerateNewId(), Prenom = "John123", Nom = "Doe123" }, new Acteur { Id = ObjectId.GenerateNewId(), Prenom = "Jane123", Nom = "Doe123" }, new Acteur { Id = ObjectId.GenerateNewId(), Prenom = "Jim23", Nom = "Doe123" } } };
            var acteur = new Acteur { Prenom = "Jill123", Nom = "Doe123" };
            // Configure le mock pour simuler une exception NumberActeursOutOfRange lorsque le nombre d'acteurs atteint la limite
            mockAbonneRepository.Setup(repo => repo.UpdateOne(It.IsAny<Abonne>()))
                              .ThrowsAsync(new NumberActeursOutOfRange("Le nombre d'acteurs atteint la limite maximale"));

            var service = new AbonneService(mockAbonneRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NumberActeursOutOfRange>(() => service.AddActeurInAbonne(abonne, acteur));
        }
        [Fact]
        public async Task AddRealisateurInAbonne_AddsRealisateur_Successfully()
        {
            // Arrange
            var mockAbonneRepository = new Mock<IAbonneRepository>();
            var abonne = new Abonne { Id = ObjectId.GenerateNewId(), Realisateurs = new List<Realisateur>() };
            var realisateur = new Realisateur { Prenom = "John123", Nom = "Doe123" };
            mockAbonneRepository.Setup(repo => repo.UpdateOne(It.IsAny<Abonne>())).ReturnsAsync(abonne);

            var service = new AbonneService(mockAbonneRepository.Object);

            // Act
            var result = await service.AddRealisateurInAbonne(abonne, realisateur);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(abonne.Id, result.Id); // Vérifie que l'ID de l'abonné a été conservé
            Assert.Contains(realisateur, result.Realisateurs); // Vérifie que le réalisateur a été ajouté à la liste des réalisateurs
            mockAbonneRepository.Verify(repo => repo.UpdateOne(It.IsAny<Abonne>()), Times.Once);
        }

        [Fact]
        public async Task AddRealisateurInAbonne_ThrowsRealisateurAlreadyExistInRealisateursList_WhenRealisateurExists()
        {
            // Arrange
            var mockAbonneRepository = new Mock<IAbonneRepository>();
            var abonne = new Abonne { Id = ObjectId.GenerateNewId(), Realisateurs = new List<Realisateur> { new Realisateur { Prenom = "John123", Nom = "Doe123" }, new Realisateur { Prenom = "John1234", Nom = "Doe1234" } } };
            var realisateur = new Realisateur { Prenom = "John123", Nom = "Doe123" };
            // Configure le mock pour simuler une exception RealisateurAlreadyExistInRealisateursList lorsque le réalisateur existe déjà
            mockAbonneRepository.Setup(repo => repo.UpdateOne(It.IsAny<Abonne>()))
                            .ThrowsAsync(new RealisateurAlreadyExistInRealisateursList("Le réalisateur existe déjà dans la liste réalisateurs"));

            var service = new AbonneService(mockAbonneRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<RealisateurAlreadyExistInRealisateursList>(() => service.AddRealisateurInAbonne(abonne, realisateur));
        }

        [Fact]
        public async Task AddRealisateurInAbonne_ThrowsNumberRealisateursOutOfRange_WhenMaxRealisateursReached()
        {
            // Arrange
            var mockAbonneRepository = new Mock<IAbonneRepository>();
            var abonne = new Abonne { Id = ObjectId.GenerateNewId(), Realisateurs = new List<Realisateur> { new Realisateur { Id = ObjectId.GenerateNewId(), Prenom = "prename1", Nom = "nomRea2" }, new Realisateur { Id = ObjectId.GenerateNewId(), Prenom = "prename2", Nom = "nomRea3" }, new Realisateur { Id = ObjectId.GenerateNewId(), Prenom = "prename3", Nom = "nomRea4" }, new Realisateur { Id = ObjectId.GenerateNewId(), Prenom = "prename4", Nom = "nomRea5" }, new Realisateur { Id = ObjectId.GenerateNewId(), Prenom = "prename5", Nom = "nomRea6" } } };
            var realisateur = new Realisateur { Prenom = "Jil123l", Nom = "Doe123" };
            // Configure le mock pour simuler une exception NumberRealisateursOutOfRange lorsque le nombre de réalisateurs atteint la limite
            mockAbonneRepository.Setup(repo => repo.UpdateOne(It.IsAny<Abonne>()))
                             .ThrowsAsync(new NumberRealisateursOutOfRange("Vous ne pouvez pas ajouter plus que 5 realisateurs"));

            var service = new AbonneService(mockAbonneRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NumberRealisateursOutOfRange>(() => service.AddRealisateurInAbonne(abonne, realisateur));
        }
    }
}