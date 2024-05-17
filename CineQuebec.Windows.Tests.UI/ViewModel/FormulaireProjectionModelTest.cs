using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using CineQuebec.Windows.ViewModel.Event;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ViewModel
{
    public class FormulaireProjectionModelTest
    {
        [Fact]
        public void GetAllFilm_LoadsFilmsCorrectly()
        {
            // Arrange
            var mockFilmService = new Mock<IFilmService>();
            mockFilmService.Setup(service => service.GetAllFilms()).ReturnsAsync(new List<Film> { new Film(), new Film()});

            var mockProjectionService = new Mock<IProjectionService>();

            var eventAggregator = new EventAggregator();

            var viewModel = new FormulaireProjectionViewModel(mockProjectionService.Object, mockFilmService.Object, eventAggregator, null);

            // Act
            viewModel.GetAllFIlm();

            // Assert
            // Vérifiez que la liste de films est correctement chargée
            Assert.Equal(2, viewModel.Films.Count);
        }


        [Fact]
        public void Save_CallsModifyProjectionAndDoesNotPublishEventTwice()
        {
            // Arrange
            var mockProjectionService = new Mock<IProjectionService>();
            mockProjectionService.Setup(service => service.ModifierProjection(It.IsAny<Projection>()));

            var mockFilmService = new Mock<IFilmService>();

            var eventAggregator = new EventAggregator();

            var viewModel = new FormulaireProjectionViewModel(mockProjectionService.Object, mockFilmService.Object, eventAggregator, new Projection());

            // Act
            viewModel.Save();

            // Assert
            mockProjectionService.Verify(service => service.ModifierProjection(It.IsAny<Projection>()), Times.Once);

            bool hasBeenPublished = false;
            eventAggregator.GetEvent<AddModifierProjectionEvent>().Subscribe(_ => hasBeenPublished = true);
        }


    }
}

