using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.AbonneExceptions
{
    public class ExistingAbonneException(string message) : Exception(message)
    {
    }
}
