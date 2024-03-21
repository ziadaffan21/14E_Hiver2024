using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.EntitysExceptions
{
    public class InvalidGuidException : Exception
    {
        public InvalidGuidException(string message) : base(message) { }

    }
}
