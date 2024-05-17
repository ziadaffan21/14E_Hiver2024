using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Entities;
using MongoDB.Bson;

namespace CineQuebec.Windows.ViewModel.ObservableClass
{
    public class ObservableNote : PropertyNotifier
    {
        private Note _note;
        private ObjectId _idFilm;
        private ObjectId _id;

        public ObjectId Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public ObjectId IdFilm
        {
            get { return _idFilm; }
            set
            {
                _idFilm = value;
                OnPropertyChanged();
            }
        }

        private ObjectId _idAbonne;

        public ObjectId IdAbonne
        {
            get { return _idAbonne; }
            set
            {
                _idAbonne = value;
                OnPropertyChanged();
            }
        }
        private int _noteValue;

        public int NoteValue
        {
            get { return _noteValue; }
            set
            {
                _noteValue = value;
                OnPropertyChanged();
            }
        }
        public ObservableNote()
        {
            _note = new();   
        }
        public ObservableNote(Note note)
        {
            _note = note is null ? new() : note;
        }
        public Note Value()
        {
            _note.Id = Id;
            _note.FilmId = IdFilm;
            _note.AbonneId = IdAbonne;
            _note.NoteValue = NoteValue;
            return _note;
        }
        public bool IsValid()
        {
            if (NoteValue >= 0 && NoteValue <= 10)
                return true;
            return false ;
        }
        public override string ToString()
        {
            return _note.ToString();
        }
    }
}