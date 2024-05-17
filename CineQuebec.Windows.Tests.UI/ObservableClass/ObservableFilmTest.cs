using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.ViewModel.ObservableClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ObservableClass
{
    public class ObservableFilmTest
    {
        [Fact]
        public void Constructor_ValidFilm_CopiesProperties()
        {
            // Arrange
            var film = new Film
            {
                Titre = "Test Title",
                Duree = 120,
                DateSortie = DateTime.Now,
                Categorie = Categories.ACTION
            };

            // Act
            var observableFilm = new ObservableFilm(film);

            // Assert
            Assert.Equal(film.Titre, observableFilm.Titre);
            Assert.Equal(film.Duree, observableFilm.Duree);
            Assert.Equal(film.DateSortie, observableFilm.DateSortie);
            Assert.Equal((int)film.Categorie, observableFilm.IndexCategorie);
        }
        [Fact]
        public void IsValid_ValidFilm_ReturnsTrue()
        {
            // Arrange
            var film = new Film
            {
                Titre = "Valid Title",
                Duree = 90,
                DateSortie = DateTime.Now,
                Categorie = Categories.COMEDY
            };

            var observableFilm = new ObservableFilm(film);

            // Act
            var isValid = observableFilm.IsValid();

            // Assert
            Assert.True(isValid);
        }

    }


}

