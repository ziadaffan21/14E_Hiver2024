using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.AbonneExceptions.Username
{
    public class UsernameException(string message) : Exception(message)
    {
    }
}
