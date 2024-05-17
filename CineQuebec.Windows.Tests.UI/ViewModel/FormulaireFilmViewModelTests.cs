using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
using CineQuebec.Windows.ViewModel;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ViewModel
{
    public class FormulaireFilmViewModelTests
    {
        [Fact]
        public void Ajout_ShouldInvokeAjouterFilmMethodOfFilmService_WhenDataIsValid()
        {
            // Arrange
            var mockFilmService = new Mock<IFilmService>();
            var mockEventAggregator = new Mock<IEventAggregator>();
            var viewModel = new FormulaireFilmViewModel(mockFilmService.Object, mockEventAggregator.Object);
            viewModel.Film.Titre = "Valid Titre";
            viewModel.Film.IndexCategorie = 0; // Assuming 0 is a valid category index
            viewModel.Film.Duree = 120; // Assuming a valid duration

            // Act
            viewModel.Ajout();

            // Assert
            mockFilmService.Verify(service => service.AjouterFilm(It.IsAny<Film>()), Times.Once);
        }

        [Fact]
        public void Save_ShouldInvokeModifierFilmMethodOfFilmService_WhenDataIsValid()
        {
            // Arrange
            var mockFilmService = new Mock<IFilmService>();
            var mockEventAggregator = new Mock<IEventAggregator>();
            var existingFilm = new Film(); // Assuming an existing film
            var viewModel = new FormulaireFilmViewModel(mockFilmService.Object, mockEventAggregator.Object, existingFilm);
            viewModel.Film.Titre = "Valid Titre";
            viewModel.Film.IndexCategorie = 0; // Assuming 0 is a valid category index
            viewModel.Film.Duree = 120; // Assuming a valid duration

            // Act
            viewModel.Save();

            // Assert
            mockFilmService.Verify(service => service.ModifierFilm(It.IsAny<Film>()), Times.Once);
        }

        [Theory]
        [InlineData("Invalid Titre")] // Invalid category index
        public void ValiderForm_ShouldThrowCategorieUndefinedException_WhenIndexCategorieIsInvalid(string titre)
        {
            // Arrange
            var mockFilmService = new Mock<IFilmService>();
            var mockEventAggregator = new Mock<IEventAggregator>();
            var viewModel = new FormulaireFilmViewModel(mockFilmService.Object, mockEventAggregator.Object);
            viewModel.Film.Titre = titre;
            viewModel.Film.IndexCategorie = -1; // Invalid category index
            viewModel.Film.Duree = 120; // Valid duration

            // Act & Assert
            Assert.Throws<CategorieUndefinedException>(() => viewModel.ValiderForm());
        }

        [Theory]
        [InlineData("Valid Titre")] // Valid titre
        public void ValiderForm_ShouldThrowArgumentOutOfRangeException_WhenDureeIsInvalid(string titre)
        {
            // Arrange
            var mockFilmService = new Mock<IFilmService>();
            var mockEventAggregator = new Mock<IEventAggregator>();
            var viewModel = new FormulaireFilmViewModel(mockFilmService.Object, mockEventAggregator.Object);
            viewModel.Film.Titre = titre;
            viewModel.Film.IndexCategorie = 0; // Valid category index
            viewModel.Film.Duree = 20; // Invalid duration

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => viewModel.ValiderForm());
        }


    }
}
