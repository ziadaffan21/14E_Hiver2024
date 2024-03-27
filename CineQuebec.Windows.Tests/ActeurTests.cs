using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Exceptions.ActeurExceptions;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;
using CineQuebec.Windows.DAL.Interfaces;
using Moq;


namespace CineQuebec.Windows.Tests
{
    public class ActeurTests
    {
        private Acteur CreerActeur()
        {
            return new Acteur("Un prenom d'acteur", "Un nom d'acteur", new DateTime(1990, 1, 1));
        }
        [Fact]
        public void Prenom_Set_Devrait_Lancer_PrenomNullException_Quand_Value_Est_Vide()
        {
            //Act et Assert
            Assert.Throws<PrenomActeurNullException>(() => new Acteur("", "Nom Acteur", new DateTime(1999, 9, 9)));
        }
        [Fact]
        public void Prenom_Set_Devrait_Lancer_PrneomLenghtException_Quand_Value_Est_Plus_Petit_Que_NB_CARACTERE_MIN_PRENOM()
        {
            //Act et Assert
            Assert.Throws<PrenomLengthException>(() => new Acteur("n", "Nom Acteur", new DateTime(1999, 9, 9)));
        }
        [Fact]
        public void Prenom_Set_Devrait_Lancer_PrneomLenghtException_Quand_Value_Est_Plus_Grand_Que_NB_CARACTERE_MAX_PRENOM()
        {
            //Act et Assert
            Assert.Throws<PrenomLengthException>(() => new Acteur("nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss", "Nom Acteur", new DateTime(1999, 9, 9)));
        }
        [Fact]
        public void Nom_Set_Devrait_Lancer_PrenomNullException_Quand_Value_Est_Vide()
        {
            //Act et Assert
            Assert.Throws<NomActeurNullException>(() => new Acteur("Prenom Acteur", "", new DateTime(1999, 9, 9)));
        }
        [Fact]
        public void Nom_Set_Devrait_Lancer_PrneomLenghtException_Quand_Value_Est_Plus_Petit_Que_NB_CARACTERE_MIN_NOM()
        {
            //Act et Assert
            Assert.Throws<NomLengthException>(() => new Acteur("Prenom Acteur", "N", new DateTime(1999, 9, 9)));
        }
        [Fact]
        public void Nom_Set_Devrait_Lancer_PrneomLenghtException_Quand_Value_Est_Plus_Grand_Que_NB_CARACTERE_MAX_NOM()
        {
            //Act et Assert
            Assert.Throws<NomLengthException>(() => new Acteur("Prenom acteur","nssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss", new DateTime(1999, 9, 9)));
        }
    }
}