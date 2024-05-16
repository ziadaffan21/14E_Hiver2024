using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineQuebec.Windows.ViewModel
{
    public class ConsultationAbonnesViewModel : PropertyNotifier
    {
        private readonly IAbonneService _abonneService;
        

        public ObservableCollection<Abonne> Abonnes { get; init; } = new();

        public ConsultationAbonnesViewModel(IAbonneService abonneService)
        {
            _abonneService=abonneService;
        }

        internal void Load(object sender, RoutedEventArgs e)
        {
            Abonnes.Clear();
            foreach (Abonne abonne in  _abonneService.GetAllAbonnes())
            {
                Abonnes.Add(abonne);
            }
        }
    }
}
