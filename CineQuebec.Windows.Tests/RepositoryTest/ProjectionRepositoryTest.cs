﻿using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using Moq;

namespace CineQuebec.Windows.DAL.Tests.RepositoryTest
{
    public class ProjectionRepositoryTest
    {
        [Fact]
        public void GetAllAbonnes_ShouldReturnListOfAbonnes()
        {
            // Arrange
            var expectedProjections = new List<Projection>
                {
                    new Projection (DateTime.Now,120,new Film("titre",DateTime.Now,120,DAL.Enums.Categories.ACTION)),
                    new Projection (DateTime.Now,120,new Film("titre2",DateTime.Now,120,DAL.Enums.Categories.ACTION))
                };
            var mockRepository = new Mock<IProjectionRepository>();
            mockRepository.Setup(repo => repo.ReadProjections())
                          .Returns(expectedProjections);

            // Act
            var result = mockRepository.Object.ReadProjections();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Projection>>(result);
            Assert.Equal(expectedProjections, result);
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
        public async Task GetAllFilms_ShouldReturnListOfFilms()
        {
            // Arrange
            var expectedFilms = new List<Film>
        {
            new Film("Titre", DateTime.Now, 120, DAL.Enums.Categories.ACTION),
            new Film("Titre1", DateTime.Now, 120, DAL.Enums.Categories.ACTION)
        };
            var mockRepository = new Mock<IFilmRepository>();
            mockRepository.Setup(repo => repo.ReadFilms())
                       .ReturnsAsync(expectedFilms);

            // Act
            var result = await mockRepository.Object.ReadFilms();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Film>>(result);
            Assert.Equal(expectedFilms, result);
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