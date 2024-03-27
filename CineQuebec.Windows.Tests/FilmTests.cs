using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;
using CineQuebec.Windows.Exceptions.FilmExceptions.CategorieExceptions;
using CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests
{
    public class FilmTests
    {
        #region CONSTANTES
        private const string TITRE_FILM = "Titre Film";
        private DateTime DATE = new DateTime(1990, 10, 10);
        private string STRING_VIDE = "";
        private string STRING_UN_CARACTERE = "a";
        private string STRING_LONG = "nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
        #endregion

        #region MÉTHODES
        [Fact]
        public void Titre_Date_Categorie_Validation()
        {
            //Act et Assert
            Assert.Throws<TitreNullException>(() => new Film(STRING_VIDE, DATE, DAL.Enums.Categories.COMEDY));
            Assert.Throws<TitreLengthException>(() => new Film(STRING_LONG, DATE, DAL.Enums.Categories.COMEDY));
            Assert.Throws<TitreLengthException>(() => new Film(STRING_UN_CARACTERE, DATE, DAL.Enums.Categories.COMEDY));

            Assert.Throws<CategorieUndefinedException>(() => new Film(TITRE_FILM, DATE, (Categories)999));
        }
        [Fact]
        public void Username_Categorie_Proriete()
        {
            var film = new Film();
            film.Titre = TITRE_FILM;
            film.DateSortie = DATE;
            film.Categorie = Categories.ACTION;

            Assert.Equal(TITRE_FILM, film.Titre);
            Assert.Equal(DATE, film.DateSortie);
            Assert.Equal(Categories.ACTION, film.Categorie);
        }
        [Fact]
        public void Constructeur_Acteur_Doit_Creer_Un_Acteur()
        {
            var film = new Film(TITRE_FILM, DATE, Categories.ACTION);

            Assert.Equal(TITRE_FILM, film.Titre);
            Assert.Equal(DATE, film.DateSortie);
            Assert.Equal(Categories.ACTION, film.Categorie);
        }
        #endregion
    }
}
