using CineQuebec.Windows.DAL.Enums;

namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IFilm
    {
        Categories Categorie { get; set; }
        DateTime DateSortie { get; set; }
        string Titre { get; set; }

        string ToString();
    }
}