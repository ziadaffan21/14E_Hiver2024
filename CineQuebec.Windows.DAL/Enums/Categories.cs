using System.ComponentModel;

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