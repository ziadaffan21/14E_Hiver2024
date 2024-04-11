using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Exceptions.ActeurExceptions;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;
using CineQuebec.Windows.DAL.Interfaces;
using Moq;
using CineQuebec.Windows.DAL.Data.Personne;


namespace CineQuebec.Windows.Tests.DataTest
{
    public class ActeurTests
    {
        #region CONSTANTES
        private const string NOM_ACTEUR = "Nom Acteur";
        private const string PRENOM_ACTEUR = "Prenom Acteur";
        private DateTime DATE = new DateTime(1990, 10, 10);
        private string STRING_VIDE = "";
        private string STRING_UN_CARACTERE = "a";
        private string STRING_LONG = "nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
        #endregion

        #region MÉTHODES
        [Fact]
        public void Prenom_Nom_Validation()
        {
            //Act et Assert
            Assert.Throws<PrenomActeurNullException>(() => new Acteur(STRING_VIDE, NOM_ACTEUR, DATE));
            Assert.Throws<PrenomLengthException>(() => new Acteur(STRING_UN_CARACTERE, NOM_ACTEUR, DATE));
            Assert.Throws<PrenomLengthException>(() => new Acteur(STRING_LONG, NOM_ACTEUR, DATE));

            Assert.Throws<NomActeurNullException>(() => new Acteur(PRENOM_ACTEUR, STRING_VIDE, DATE));
            Assert.Throws<NomLengthException>(() => new Acteur(PRENOM_ACTEUR, STRING_UN_CARACTERE, DATE));
            Assert.Throws<NomLengthException>(() => new Acteur(PRENOM_ACTEUR, STRING_LONG, DATE));
        }
        [Fact]
        public void Prenom_Nom_Proriete()
        {
            //Arrange
            var acteur = new Acteur();
            acteur.Nom = "Nom Acteur";
            acteur.Prenom = "Prenom Acteur";

            //Act et Assert
            Assert.Equal(NOM_ACTEUR, acteur.Nom);
            Assert.Equal(PRENOM_ACTEUR, acteur.Prenom);
        }
        [Fact]
        public void Constructeur_Acteur_Doit_Creer_Un_Acteur()
        {
            //Arrange
            var acteur = new Acteur("Prenom Acteur", "Nom Acteur", new DateTime(1990, 10, 10));
            //Act et Assert
            Assert.Equal(PRENOM_ACTEUR, acteur.Prenom);
            Assert.Equal(NOM_ACTEUR, acteur.Nom);
            Assert.Equal(DATE, acteur.Naissance);
        }
        #endregion
    }
}