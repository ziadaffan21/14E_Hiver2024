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
        private readonly IFilmService _filmService;

        public AbonneHomeViewModel(IFilmService filmService)
        {
            _filmService = filmService;
            ChargerFilms();

        }
       
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

        private async void ChargerFilms()
        {
            var filmsCharge = await _filmService.GetAllFilms();

            //TODO : Mettre les films dans Films
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