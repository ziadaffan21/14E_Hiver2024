using CineQuebec.Windows.DAL.Entities;
using CineQuebec.Windows.ViewModel.ObservableClass;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.UI.Tests.ObservableClass
{
    public class ObservableNoteTests
    {
        [Fact]
        public void Constructor_ValidNote_CopiesProperties()
        {
            // Arrange
            var note = new ObservableNote
            {
                Id = ObjectId.GenerateNewId(),
                IdFilm = ObjectId.GenerateNewId(),
                IdAbonne = ObjectId.GenerateNewId(),
                NoteValue = 7
            };

            // Act
            Note noteValue = note.Value();

            // Assert
            Assert.Equal(note.Id, noteValue.Id);
            Assert.Equal(note.IdFilm, note.IdFilm);
            Assert.Equal(note.IdAbonne, note.IdAbonne);
            Assert.Equal(note.NoteValue, note.NoteValue);
        }

        [Fact]
        public void IsValid_ValidNote_ReturnsTrue()
        {
            // Arrange
            var note = new ObservableNote
            {
                Id = ObjectId.GenerateNewId(),
                IdFilm = ObjectId.GenerateNewId(),
                IdAbonne = ObjectId.GenerateNewId(),
                NoteValue = 8
            };


            // Act
            var isValid = note.IsValid();

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_InvalidNote_ReturnsFalse()
        {
            // Arrange
            var note = new ObservableNote
            {
                Id = ObjectId.GenerateNewId(),
                IdFilm = ObjectId.GenerateNewId(),
                IdAbonne = ObjectId.GenerateNewId(),
                NoteValue = 11
            };


            // Act
            var isValid = note.IsValid();

            // Assert
            Assert.False(isValid);
        }
    }
}
