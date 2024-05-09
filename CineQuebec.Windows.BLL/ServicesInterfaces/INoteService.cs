using CineQuebec.Windows.DAL.Entities;

namespace CineQuebec.Windows.BLL.ServicesInterfaces
{
    public interface INoteService
    {
        Task<Note> Add(Note note);
        Task<bool> Delete(Note noteToDelete);
        Task<List<Note>> GetAll();
        Task<float> GetRatingForFilm();
        Task<Note> Update(Note noteToUpdate);
    }
}