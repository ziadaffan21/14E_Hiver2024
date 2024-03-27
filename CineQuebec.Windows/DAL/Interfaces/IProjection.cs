using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IProjection
    {
        DateTime Date { get; set; }
        ObjectId IdFilm { get; set; }
        int PlaceDisponible { get; set; }

        string ToString();
    }
}