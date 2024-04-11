using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Enums
{
    public enum Categories
    {
        ACTION,
        COMEDY,
        DRAMA,
        HORROR,
        ROMANCE,
        [Description("Science-fiction")]
        SCIENCE_FICTION,
        THRILLER,
        DOCUMENTARY,
        ANIMATION,
        FANTASY,
        CRIME,
        ADVENTURE,
        FAMILY
    }

}
