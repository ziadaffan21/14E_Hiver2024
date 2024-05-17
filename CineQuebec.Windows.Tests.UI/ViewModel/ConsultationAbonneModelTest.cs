using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ViewModel
{
    public class ConsultationAbonneModelTest
    {
        private readonly Mock<IAbonneService> _mockAbonneService;
        private readonly ConsultationAbonnesViewModel _viewModel;

        public ConsultationAbonneModelTest()
        {
            // Arrange
            _mockAbonneService = new Mock<IAbonneService>();
            _viewModel = new ConsultationAbonnesViewModel(_mockAbonneService.Object);
        }

        [Fact]
        public void Load_ShouldPopulateAbonnesCollection()
        {
            // Arrange
            var fakeAbonnes = new List<Abonne>
            {
                new Abonne { Id = new MongoDB.Bson.ObjectId(), Username = "Abonne1" },
                new Abonne { Id = new MongoDB.Bson.ObjectId(), Username = "Abonne2" }
            };

            _mockAbonneService.Setup(service => service.GetAllAbonnes()).Returns(fakeAbonnes);

            // Act
            _viewModel.Load(null, null);

            // Assert
            Assert.Equal(2, _viewModel.Abonnes.Count);
            Assert.Equal("Abonne1", _viewModel.Abonnes[0].Username);
            Assert.Equal("Abonne2", _viewModel.Abonnes[1].Username);
        }

        [Fact]
        public void Load_ShouldClearExistingAbonnesBeforeLoadingNewOnes()
        {
            // Arrange
            var fakeAbonnes = new List<Abonne>
            {
                new Abonne { Id = new MongoDB.Bson.ObjectId(), Username = "Abonne1" },
                new Abonne { Id = new MongoDB.Bson.ObjectId(), Username = "Abonne2" }
            };

            _mockAbonneService.Setup(service => service.GetAllAbonnes()).Returns(fakeAbonnes);

            // Act
            _viewModel.Abonnes.Add(new Abonne { Id = new MongoDB.Bson.ObjectId(), Username = "OldAbonne" }); // Pre-load with an old entry
            _viewModel.Load(null, null);

            // Assert
            Assert.DoesNotContain(_viewModel.Abonnes, abonne => abonne.Username == "OldAbonne");
            Assert.Equal(2, _viewModel.Abonnes.Count);
        }
    }
}
