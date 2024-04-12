using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Exceptions.ProjectionException
{
    public class ExistingProjectionException(string message) : Exception(message)
    {
    }
}
