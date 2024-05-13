using CineQuebec.Windows.DAL.Entities;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.ServicesInterfaces
{
    public interface INoteService
    {
        Task<Note> Add(Note note);
        Task<bool> Delete(Note noteToDelete);
        Task<List<Note>> GetAll();
        Task<float> GetRatingForFilm();
        Task<Note> Update(Note noteToUpdate);
        Task<Note> FindById(ObjectId idFilm, ObjectId idAbonne);
    }
}