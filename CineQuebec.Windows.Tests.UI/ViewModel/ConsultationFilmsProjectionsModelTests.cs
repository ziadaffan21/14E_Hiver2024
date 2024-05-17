using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.EntitysExceptions;
using CineQuebec.Windows.ViewModel;
using CineQuebec.Windows.ViewModel.Event;
using MongoDB.Bson;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ViewModel
{
    public class ConsultationFilmsProjectionsModelTests
    {
        private readonly Mock<IFilmService> _mockFilmService;
        private readonly Mock<IProjectionService> _mockProjectionService;
        private readonly Mock<IEventAggregator> _mockEventAggregator;
        private readonly ConsultationFilmsProjectionsModel _viewModel;

        public ConsultationFilmsProjectionsModelTests()
        {
            _mockFilmService = new Mock<IFilmService>();
            _mockProjectionService = new Mock<IProjectionService>();
            _mockEventAggregator = new Mock<IEventAggregator>();

            // Mocking the event aggregator to return mock events
            var filmEventMock = new Mock<AddModifierFilmEvent>();
            var projectionEventMock = new Mock<AddModifierProjectionEvent>();
            _mockEventAggregator.Setup(ea => ea.GetEvent<AddModifierFilmEvent>()).Returns(filmEventMock.Object);
            _mockEventAggregator.Setup(ea => ea.GetEvent<AddModifierProjectionEvent>()).Returns(projectionEventMock.Object);

            _viewModel = new ConsultationFilmsProjectionsModel(
                _mockFilmService.Object,
                _mockProjectionService.Object,
                _mockEventAggregator.Object);
        }

        [Fact]
        public async Task Load_ShouldPopulateFilmsCollection_WhenCalled()
        {
            // Arrange
            var films = new List<Film> { new Film { Id = ObjectId.GenerateNewId(), Titre = "Film 1" } };
            _mockFilmService.Setup(service => service.GetAllFilms()).ReturnsAsync(films);

            // Act
            _viewModel.Load(null, null);
            await Task.Delay(100); // Small delay to ensure async method completes

            // Assert
            Assert.Single(_viewModel.Films);
            Assert.Equal("Film 1", _viewModel.Films[0].Titre);
        }

        [Fact]
        public async Task LoadProjectionFilm_ShouldPopulateProjectionsCollection_WhenCalledWithValidId()
        {
            // Arrange
            var filmId = ObjectId.GenerateNewId();
            var projections = new List<Projection>
            {
                new Projection { Id = ObjectId.GenerateNewId(), Film = new Film { Id = filmId } }
            };
            _mockProjectionService.Setup(service => service.GetProjectionsById(filmId)).ReturnsAsync(projections);

            // Act
            _viewModel.LoadProjectionFilm(filmId);
            await Task.Delay(100); // Small delay to ensure async method completes

            // Assert
            Assert.Single(_viewModel.Projections);
            Assert.Equal(filmId, _viewModel.Projections[0].Film.Id);
        }


        //[Fact]
        //public async Task SupprimerProjection_ShouldCallServiceAndReloadData_WhenUserConfirmsDeletion()
        //{
        //    // Arrange
        //    var projection = new Projection { Id = ObjectId.GenerateNewId(), Film = new Film { Id = ObjectId.GenerateNewId() } };
        //    _mockProjectionService.Setup(service => service.SupprimerProjection(projection.Id)).Returns(Task.CompletedTask);

        //    // Act
        //    _viewModel.SupprimerProjection(projection);
        //    await Task.Delay(100);

        //    // Assert
        //    _mockProjectionService.Verify(service => service.SupprimerProjection(projection.Id), Times.Once);
        //    _mockFilmService.Verify(service => service.GetAllFilms(), Times.Once);
        //    _mockProjectionService.Verify(service => service.GetProjectionsById(projection.Film.Id), Times.Once);
        //}

        //[Fact]
        //public async Task SupprimerFilm_ShouldCallServiceAndReloadData_WhenUserConfirmsDeletion()
        //{
        //    // Arrange
        //    var film = new Film { Id = ObjectId.GenerateNewId() };
        //    _mockFilmService.Setup(service => service.SupprimerFilm(film.Id)).Returns(Task.CompletedTask);

        //    // Act
        //    _viewModel.SupprimerFilm(film);
        //    await Task.Delay(100); // Small delay to ensure async method completes

        //    // Assert
        //    _mockFilmService.Verify(service => service.SupprimerFilm(film.Id), Times.Once);
        //    _mockFilmService.Verify(service => service.GetAllFilms(), Times.Once);
        //}

        //[Fact]
        //public void ModifierProjection_ShouldShowErrorMessage_WhenProjectionDateIsInThePast()
        //{
        //    // Arrange
        //    var pastProjection = new Projection { Date = DateTime.Now.AddDays(-1) };

        //    // Act
        //    _viewModel.ModifierProjection(_mockEventAggregator.Object, pastProjection);

        //    // Assert
        //    // We can check that an error message was shown by asserting that some mock method was called,
        //    // or by checking the state change caused by the message box (if applicable).
        //    // Here, we'll assume a method was called on the mock.
        //    // Note: In practice, showing a message box would need to be handled in a way that's testable.
        //}

    }
}
