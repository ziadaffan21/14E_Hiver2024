using CineQuebec.Windows.DAL.Entities;
using Moq;

namespace CineQuebec.Windows.BLL.Tests
{
    public class NoteServiceTests
    {
        Mock<INoteRepository> _noteRepo;
        List<Note> notes;
        public NoteServiceTests()
        {
            _noteRepo = new Mock<INoteRepository>();
            notes = new List<Note>
            {
                new Note(new MongoDB.Bson.ObjectId(),new MongoDB.Bson.ObjectId(),6),
                new Note(new MongoDB.Bson.ObjectId(),new MongoDB.Bson.ObjectId(),8),
                new Note(new MongoDB.Bson.ObjectId(),new MongoDB.Bson.ObjectId(),4)
            };
        }
        [Fact]
        public async Task GetAll_Should_Return_all_Notes()
        {

            _noteRepo.Setup(x => x.GetAll()).ReturnsAsync(notes);

            var noteService = new NoteService(_noteRepo.Object);

            List<Note> result = await noteService.GetAll();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Add_Should_AddOneNote_In_Notes()
        {
            Note note = new Note (new MongoDB.Bson.ObjectId(),new MongoDB.Bson.ObjectId(),10);
            _noteRepo.Setup(x => x.Add(note)).ReturnsAsync(note);
            var noteService = new NoteService(_noteRepo.Object);

            Note result = await noteService.Add(note);

            Assert.Equal(note, result);
        }

        [Fact]
        public async Task Update_Should_Update_Note()
        {
            Note noteToUpdate = new Note { NoteValue = 10 };
            Note updatedNote = new Note { NoteValue = 20 };
            _noteRepo.Setup(x => x.Update(It.IsAny<Note>())).ReturnsAsync(updatedNote);

            var noteService = new NoteService(_noteRepo.Object);

            
            Note result = await noteService.Update(noteToUpdate);

            
            Assert.Equal(updatedNote, result);
        }

        [Fact]
        public async Task Delete_Should_Delete_Note()
        {
            
            Note noteToDelete = new Note { NoteValue = 10 };
            _noteRepo.Setup(x => x.Delete(It.IsAny<Note>())).ReturnsAsync(true);

            var noteService = new NoteService(_noteRepo.Object);

            
            bool result = await noteService.Delete(noteToDelete);

            
            Assert.True(result);
        }
        [Fact]
        public async void Find_By_Id_Should_Return_One_Note_With_Its_Id()
        {
            Note noteToFind = new Note { NoteValue = 10 };
            _noteRepo.Setup(x => x.FindById(noteToFind.Id)).ReturnsAsync(noteToFind);

            var noteService = new NoteService(_noteRepo.Object);

            Note foundedNote = await noteService.FindById(noteToFind.Id);

            Assert.Equal(noteToFind, foundedNote);
        }

        [Fact]
        public async void GetRatingForFilm_Should_Return_TheRating()
        {
            _noteRepo.Setup(x => x.GetAll()).ReturnsAsync(notes);
            var noteService = new NoteService(_noteRepo.Object);

            float expectedRating = notes.Any() ? notes.Sum(note => note.NoteValue) / notes.Count : 0;

            var result = await noteService.GetRatingForFilm();

            Assert.Equal(expectedRating, result);
        }

    }
}