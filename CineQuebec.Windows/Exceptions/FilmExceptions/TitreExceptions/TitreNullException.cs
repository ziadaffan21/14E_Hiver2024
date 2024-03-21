using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.FilmExceptions.TitreExceptions
{
    public class TitreNullException(string message) : TitreException(message)
    {
    }
}
