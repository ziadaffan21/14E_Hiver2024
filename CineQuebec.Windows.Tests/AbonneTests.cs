using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Password;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests
{
    public class AbonneTests
    {
        #region CONSTANTES
        private const string USERNAME = "Username Abonne";
        private DateTime DATE = new DateTime(1990, 10, 10);
        private string STRING_VIDE = "";
        private string PASSWORD = "123";
        private string STRING_UN_CARACTERE = "a";
        private string STRING_LONG = "nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
        #endregion

        #region MÉTHODES
        [Fact]
        public void Username_Password_Date_Validations()
        {
            Assert.Throws<UsernameNullException>(() => new Abonne(STRING_VIDE, DATE));
            Assert.Throws<UsernameLengthException>(() => new Abonne(STRING_UN_CARACTERE, DATE));
            Assert.Throws<UsernameLengthException>(() => new Abonne(STRING_LONG, DATE));

            Assert.Throws<PasswordNullException>(() => new Abonne(USERNAME, STRING_VIDE, DATE));
            Assert.Throws<PasswordLengthException>(() => new Abonne(USERNAME, STRING_LONG, DATE));
            Assert.Throws<PasswordLengthException>(() => new Abonne(USERNAME, STRING_UN_CARACTERE, DATE));
        }
        [Fact]
        public void Username_Password_Date_Prorietes()
        {
            var abonne = new Abonne(USERNAME, PASSWORD, DATE);

            Assert.Equal(USERNAME, abonne.Username);
            Assert.Equal(PASSWORD, abonne.Password);
            Assert.Equal(DATE, abonne.DateAdhesion);
        }
        [Fact]
        public void Constructeur_Abonne_Doit_Creer_Un_Acteur()
        {
            //Arrange
            var abonne = new Abonne("Username Abonne", "123", new DateTime(1990, 10, 10));

            //Act et Assert
            Assert.Equal(USERNAME, abonne.Username);
            Assert.Equal(PASSWORD, abonne.Password);
            Assert.Equal(DATE, abonne.DateAdhesion);
        }
        #endregion
    }
}
