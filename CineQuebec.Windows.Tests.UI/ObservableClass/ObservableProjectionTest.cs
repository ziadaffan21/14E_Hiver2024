using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.ViewModel.ObservableClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ObservableClass
{
    public class ObservableProjectionTest
    {
        [Fact]
        public void Constructor_ValidProjection_CopiesProperties()
        {
            // Arrange
            var observableProjection = new ObservableProjection
            {
                Date = DateTime.Now,
                PlaceDisponible = 100,
                Film = new Film()
            };

            // Act
            var projection = observableProjection.Value();

            // Assert
            Assert.Equal(projection.Date, observableProjection.Date);
            Assert.Equal(projection.NbPlaces, observableProjection.PlaceDisponible);
            Assert.Same(projection.Film, observableProjection.Film);
        }

        [Fact]
        public void IsValid_ValidProjection_ReturnsTrue()
        {
            // Arrange
            var observableProjection = new ObservableProjection
            {
                Date = DateTime.Now.AddDays(5),
                PlaceDisponible = 50,
                Film = new Film()
            };

            // Act
            var isValid = observableProjection.IsValid();

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_InvalidProjection_ReturnsFalse()
        {
            // Arrange
            var observableProjection = new ObservableProjection
            {
                Date = DateTime.Now.AddYears(5),
                PlaceDisponible = 0,
                Film = null
            };

            var projection = observableProjection.Value();

            // Act
            var isValid = observableProjection.IsValid();

            // Assert
            Assert.False(isValid);
        }
    }
}
