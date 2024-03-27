using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests
{
    public class GestionFilmAbonneTests
    {

        [Fact]
        public void Gestion_Film_Abonne_ReadActeurs_Doit_Lire_Tout_Les_Acteurs()
        {
            //Arrange
            List<Acteur> acteurs = new List<Acteur>();

            //Act
            int nbActeurs = 10;
            acteurs = GestionFilmAbonne.ReadActeurs();

            //Assert
            Assert.Equal(10, acteurs.Count);
        }
    }
}
