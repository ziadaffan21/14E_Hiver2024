using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Password;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.DataTest
{
    public class AbonneTests
    {
        #region CONSTANTES
        private const string USERNAME = "Username Abonne";
        private DateTime DATE = new DateTime(1990, 10, 10);
        private string PASSWORD = "123";
        private string STRING_UN_CARACTERE = "a";
        private string STRING_LONG = "nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
        private Abonne _abonne;
        #endregion

        #region MÉTHODES
        public AbonneTests()
        {
            _abonne = new Abonne(USERNAME, DATE);
        }

        [Fact]
        public void Validation_Fail_Si_Longueur_Username_Inferieur_A_Min()
        {
            Assert.Throws<UsernameLengthException>(() => new Abonne(STRING_UN_CARACTERE, DATE));
        }

        [Fact]
        public void Validation_Fail_Si_Longueur_Username_Superieur_A_Max()
        {
            Assert.Throws<UsernameLengthException>(() => new Abonne(STRING_LONG, DATE));
        }

        [Fact]
        public void Validation_Fail_Si_Username_Est_Null()
        {
            Assert.Throws<UsernameNullException>(() => new Abonne(null, DATE));
        }

        //[Fact]
        //public void Validation_Fail_Si_Longueur_Password_Inferieur_A_Min()
        //{
        //    Assert.Throws<PasswordLengthException>(() => new Abonne(USERNAME, STRING_UN_CARACTERE, DATE));
        //}

        //[Fact]
        //public void Validation_Fail_Si_Longueur_Password_Superieur_A_Max()
        //{
        //    Assert.Throws<PasswordLengthException>(() => new Abonne(USERNAME, STRING_LONG, DATE));
        //}

        //[Fact]
        //public void Validation_Fail_Si_Password_Est_Null()
        //{
        //    Assert.Throws<PasswordNullException>(() => new Abonne(USERNAME, null, DATE));
        //}

        [Fact]
        public void Username_Match_Abonne_Username()
        {
            Assert.Equal(USERNAME, _abonne.Username);
        }

        //[Fact]
        //public void Password_Match_Abonne_Password()
        //{
        //    Assert.Equal(PASSWORD, _abonne.Password);
        //}

        [Fact]
        public void Date_Match_Abonne_Date()
        {
            Assert.Equal(DATE, _abonne.DateAdhesion);
        }
        #endregion
    }
}
