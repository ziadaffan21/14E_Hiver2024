using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.AbonneExceptions.Password
{
    public class PasswordException(string message) : Exception(message)
    {
    }
}
