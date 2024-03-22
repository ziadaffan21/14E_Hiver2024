using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL
{
    public static class GestionFilmAbonne
    {
        private static DatabasePeleMele BaseDeDonne= new DatabasePeleMele();

        public static void ModifierFilm(Film film)
        {
            if (film is null)
                throw new ArgumentNullException("Le film ne peut pas être null");
            BaseDeDonne.ModifierFilm(film);
        }

    }
}
