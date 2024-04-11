using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.ActeurExceptions
{
    public class ActeurException(string message) : Exception(message)
    {
    }
}
