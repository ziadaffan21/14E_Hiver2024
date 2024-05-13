using Amazon.Util.Internal;
using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Entities;
using CineQuebec.Windows.View;
using CineQuebec.Windows.ViewModel.ObservableClass;
using MongoDB.Bson;
using Prism.Commands;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class NoterFilmViewModel : PropertyNotifier
    {
        private INoteService _noteService;
        private ObservableNote _note = new(null);
        public ObservableNote Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }
        public ICommand ConfirmCommand { get; set; }

        public NoterFilmViewModel(INoteService noteService, ObjectId idFilm)
        {
            _noteService = noteService;
             GetNote(idFilm);
            ConfirmCommand = new DelegateCommand(Save, CanSave);
        }

        private async void GetNote(ObjectId idFilm)
        {
            var note = await _noteService.FindById(idFilm, ((ConnecteWindowPrincipal)Application.Current.MainWindow).User.Id);
            if (note is not null)
                Note = new ObservableNote
                {
                    IdAbonne = note.AbonneId,
                    IdFilm = note.FilmId,
                    NoteValue = note.NoteValue
                };
            else
                Note = new ObservableNote
                {
                    IdAbonne = ((ConnecteWindowPrincipal)Application.Current.MainWindow).User.Id,
                    IdFilm = idFilm
                };
        }

        private bool CanSave()
        {
            return Note.IsValid();
        }

        private void Save()
        {
            _noteService.Add(Note.Value());
        }
    }
}