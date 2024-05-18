using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class DetailAbonnesViewModel:PropertyNotifier
    { 
        private Abonne _abonne;
        private IProjectionService _projectionService;
        private int nbreProjection;
        
        public Abonne Abone {  get { return _abonne; } set { _abonne = value; OnPropertyChanged();  } }
        public int NbreProjection { get { return nbreProjection; }  set { nbreProjection = value;OnPropertyChanged(); } }
        public string Titre {  get; set; }

        public Action<bool> OuvrirRecompense;

        public ICommand SaveCommand { get; init; }
        public DetailAbonnesViewModel(Abonne abonne, IProjectionService projectionService)
        {
            Abone = abonne;
            Titre = Abone.Username;
            _projectionService = projectionService;
            SaveCommand = new DelegateCommand(Recompenser, CanRecompenser);
        }

        private bool CanRecompenser()
        {
            return true;
        }

        private void Recompenser()
        {
            OuvrirRecompense?.Invoke(true);
        }

        public async void Load(object sender, RoutedEventArgs e)
        {
            NbreProjection=await _projectionService.NombreProjectionPourAbonne(Abone.Id);
        }
        
    }
}
