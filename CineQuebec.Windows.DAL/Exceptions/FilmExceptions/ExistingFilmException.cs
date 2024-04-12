using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Exceptions.FilmExceptions
{
    public class ExistingFilmException(string message) : Exception(message)
    {
    }
}
