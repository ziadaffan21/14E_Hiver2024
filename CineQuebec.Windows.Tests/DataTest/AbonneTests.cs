using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;

namespace CineQuebec.Windows.Tests.DataTest
{
    public class AbonneTests
    {
        #region CONSTANTES

        private const string USERNAME = "Username Abonne";
        private DateTime DATE = new DateTime(1990, 10, 10);
        private string STRING_UN_CARACTERE = "a";
        private string STRING_LONG = "nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
        private Abonne _abonne;

        #endregion CONSTANTES

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
        public void Username_Match_Abonne_Username()
        {
            Assert.Equal(USERNAME, _abonne.Username);
        }

        [Fact]
        public void Date_Match_Abonne_Date()
        {
            Assert.Equal(DATE, _abonne.DateAdhesion);
        }

        #endregion MÉTHODES
    }
}