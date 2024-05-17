using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CineQuebec.Windows.ViewModel
{
    public class AbonneHomeViewModel : PropertyNotifier
    {
        private ObservableCollection<Film> _films = new ObservableCollection<Film>();

        private Abonne _user;

        public Abonne User
        {
            get { return _user; }
            set 
            {   
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public string UserImage { get { return $"https://robohash.org/{User.Username}?set=set3"; } }

        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set
            {
                if (_films != value)
                {
                    _films = value;
                    OnPropertyChanged(nameof(Films));
                }
            }
        }




        private readonly IFilmService _filmService;

        public AbonneHomeViewModel(IFilmService filmService, Abonne user)
        {
            _filmService = filmService;
            User = user;
            ChargerFilms();

        }
       
        private async void ChargerFilms()
        {
            List<Film> filmsCharge = await _filmService.GetAllFilms();

            Films = new(filmsCharge);


            for (int i = 0; i < Films.Count; i++)
            {
                var film = Films[i];
                if (!film.EstAffiche)
                {
                    Films.Remove(film);
                }
            }
        }

    }
}