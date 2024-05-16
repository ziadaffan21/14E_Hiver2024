using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View;
using CineQuebec.Windows.ViewModel.ObservableClass;
using MongoDB.Bson;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using ZstdSharp.Unsafe;


namespace CineQuebec.Windows.ViewModel
{
    public class NoterViewModel : PropertyNotifier
    {
        private INoteService _noteService;
        public event Action<string> SuccessMessage;
        private Film _film;
        private float _noteTotal;
        private ObservableNote _note = new(null);
        private bool noteExiste = false;
        private Window _window;
        public ICommand EnregistrerNoteCommand { get; set; }

        public ObservableNote Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }
        public Film Film
        {
            get => _film;
            set
            {
                _film = value;
                OnPropertyChanged();
            }
        }

        public float NoteTotal
        {
            get { return _noteTotal; }
            set
            {
                _noteTotal = value;
                OnPropertyChanged();
            }
        }

        public NoterViewModel(INoteService noteService, Film film, Window window)
        {
            this._noteService = noteService;
            this.Film = film;
            EnregistrerNoteCommand = new DelegateCommand(Save);
            GetNote(film.Id);
            _window = window;
        }

        private void Save()
        {
            if (!noteExiste)
            {
                _noteService.Add(Note.Value());
                _window.DialogResult = true;

            }

            else
            {
                _noteService.Update(Note.Value());
            SuccessMessage.Invoke("La note a été enregistrée avec succès.");
            GetNoteForFilm();
            }
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            GetNoteForFilm();
        }

        private async void GetNoteForFilm()
        {
            NoteTotal = await _noteService.GetRatingForFilm(_film.Id);
        }
        private async void GetNote(ObjectId idFilm)
        {
            var note = await _noteService.FindById(idFilm, ((ConnecteWindowPrincipal)Application.Current.MainWindow).User.Id);
            if (note is not null)
            {
                noteExiste = true;
                Note = new ObservableNote
                {
                    Id = note.Id,
                    IdAbonne = note.AbonneId,
                    IdFilm = note.FilmId,
                    NoteValue = note.NoteValue
                };
            }
            else
                Note = new ObservableNote
                {
                    IdAbonne = ((ConnecteWindowPrincipal)Application.Current.MainWindow).User.Id,
                    IdFilm = idFilm
                };
        }

    }
}