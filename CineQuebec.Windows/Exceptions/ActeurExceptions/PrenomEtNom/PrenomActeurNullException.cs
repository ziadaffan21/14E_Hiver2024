using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom
{
    public class PrenomActeurNullException(string message) : ActeurException(message)
    {
    }
}
