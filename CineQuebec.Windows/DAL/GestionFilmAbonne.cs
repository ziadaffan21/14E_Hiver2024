using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Data.Personne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CineQuebec.Windows.DAL
{
    public static class GestionFilmAbonne
    {
        private static readonly DatabasePeleMele BaseDeDonne= new DatabasePeleMele();

        public async static Task ModifierFilm(Film film)
        {
            if (film is null)
                throw new ArgumentNullException("Le film ne peut pas être null");
            await BaseDeDonne.ModifierFilm(film);
        }

        public async static Task AjouterFilm(Film film)
        {
            if (film is null)
                throw new ArgumentNullException("Le film ne peut pas être null");
            await BaseDeDonne.AjouterFilm(film);
        }

        public static List<Film> ReadFilms()
        {
            return BaseDeDonne.ReadFilms();
        }

        public static List<Abonne> ReadAbonne()
        {
            return BaseDeDonne.ReadAbonnes();
        }

        public async static Task AjouterProjection(Projection projection)
        {
            if (projection is null)
                throw new ArgumentNullException("Lq projection ne peut pas être null");
            await BaseDeDonne.AjouterProjection(projection);
        }

        public static List<Acteur> ReadActeurs()
        {
            return BaseDeDonne.ReadActeurs();
        }
    }
}
