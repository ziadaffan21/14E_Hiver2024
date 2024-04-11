using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions
{
    public class MongoDataConnectionException:Exception
    {
        public MongoDataConnectionException(string message) : base(message) { }
    }
}
