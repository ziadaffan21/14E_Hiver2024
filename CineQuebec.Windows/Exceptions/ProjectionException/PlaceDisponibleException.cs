using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Exceptions.ProjectionException
{
    public class PlaceDisponibleException:Exception
    {
        public PlaceDisponibleException(string message):base(message)
        {
            
        }
    }
}
