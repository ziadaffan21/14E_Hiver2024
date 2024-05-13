
using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Entities;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Tests
{
    public class NoteService : INoteService
    {
        private INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<List<Note>> GetAll()
        {
            return await _noteRepository.GetAll();
        }

        public async Task<Note> Add(Note note)
        {
            return await _noteRepository.Add(note);
        }

        public async Task<Note> Update(Note noteToUpdate)
        {
            return await _noteRepository.Update(noteToUpdate);
        }

        public async Task<bool> Delete(Note noteToDelete)
        {
            return await _noteRepository.Delete(noteToDelete);
        }

        public async Task<float> GetRatingForFilm()
        {
            var notes = await GetAll();
            if (notes == null || !notes.Any())
            {
                return 0;
            }

            float totalRating = notes.Sum(note => note.NoteValue);
            float averageRating = totalRating / notes.Count;

            return averageRating;
        }

        public async Task<Note> FindById(ObjectId idFilm, ObjectId idAbonne)
        {
            var foundedNote = await _noteRepository.FindById(idFilm, idAbonne);
            return foundedNote;
        }
    }
}