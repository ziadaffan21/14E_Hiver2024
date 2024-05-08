using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;

namespace CineQuebec.Windows.Tests.DataTest
{
    public class FilmTests
    {
        #region CONSTANTES

        private const string TITRE_FILM = "Titre Film";
        private DateTime DATE = new DateTime(1990, 10, 10);
        private string STRING_VIDE = "";
        private string STRING_UN_CARACTERE = "a";
        private Categories categorie = Categories.ACTION;
        private Film film;
        private string STRING_LONG = "nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";

        #endregion CONSTANTES

        #region MÉTHODES

        public FilmTests()
        {
            film = new Film(TITRE_FILM, DATE, 120, categorie);
        }

        [Fact]
        public void Titre_Throw_Titre_Null_Exception_Si_Titre_Null()
        {
            //Act et Assert
            Assert.Throws<TitreNullException>(() => new Film(STRING_VIDE, DATE, 120, Categories.COMEDY));
        }

        [Fact]
        public void Titre_Throw_TitreLengthException_Si_Titre_Est_Long()
        {
            //Act et Assert
            Assert.Throws<TitreLengthException>(() => new Film(STRING_LONG, DATE, 120, Categories.COMEDY));
        }

        [Fact]
        public void Titre_Throw_TitreLengthException_Si_Titre_Est_Court()
        {
            //Act et Assert
            Assert.Throws<TitreLengthException>(() => new Film(STRING_UN_CARACTERE, DATE, 120, Categories.COMEDY));
        }

        [Fact]
        public void Categorie_Throw_CategorieUndefinedException_Si_Categorie_Est_Null()
        {
            //Act et Assert
            Assert.Throws<CategorieUndefinedException>(() => new Film(TITRE_FILM, DATE, 120, (Categories)999));
        }

        [Fact]
        public void Titre_Equal_A_Film_Titre()
        {
            //Act et Assert
            Assert.Equal(TITRE_FILM, film.Titre);
        }

        [Fact]
        public void Date_Equal_A_Film_Date()
        {
            //Act et Assert

            Assert.Equal(DATE, film.DateSortie);
        }

        [Fact]
        public void Categorie_Equal_A_Film_Categorie()
        {
            //Act et Assert

            Assert.Equal(Categories.ACTION, film.Categorie);
        }

        #endregion MÉTHODES
    }
}