using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class FormulaireProjectionViewModel:PropertyNotifier
    {
        private Projection _projection;
        private List<Film> _films;
        public ICommand SaveCommand { get; init; }

        public Projection Projection {
            get { return _projection; } 
            set { _projection = value;
                OnPropertyChanged();
            }
        
        }

        public List<Film > Films { get { return _films; } private set { _films = value;  OnPropertyChanged(); } }

        private readonly IProjectionService _projectionService;

        private readonly IFilmService _filmService;


        public FormulaireProjectionViewModel(IProjectionService projectionService, IFilmService filmService)
        {
            _projectionService = projectionService;
            _filmService = filmService;
            Projection = new();
            SaveCommand = new DelegateCommand(Save, CanSave);            
            GetAllFIlm();
        }

        

        public async void GetAllFIlm()
        {
            Films= await  _filmService.GetAllFilms();
        }

        private bool CanSave()
        {
            return false;
        }

        private void Save()
        {
            _projectionService.AjouterProjection(Projection);
        }
    }
}
