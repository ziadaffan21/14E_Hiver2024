using CineQuebec.Windows.BLL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.DAL.Utils;
using CineQuebec.Windows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour FilmDetailsView.xaml
    /// </summary>
    public partial class FilmDetailsView : Window
    {
        public FilmDetailsViewModel _viewModel { get; set; } 

        public FilmDetailsView(Film film)
        {
            _viewModel = new(film, AbonneHomeControl.CurrentUser, AbonneHomeControl.ProjectionService);
            DataContext = _viewModel;
            
            InitializeComponent();
            InitialiserDetails();
        }

        private void InitialiserDetails()
        {
            Title = _viewModel.Film.Titre;
            txtTitre.Text = _viewModel.Film.Titre;

            //TODO : Remplacer par une proprieté
            txtDescription.Text = Film.PLACEHOLDER_DESC;

            FormaterAffichage();
        }

        private async void FormaterAffichage()
        {
            var film = _viewModel.Film;

            if (film.EstAffiche)
            {
                //TODO : Checker si projections

                bool hasUpcomingProjections = await _viewModel.HasUpcomingProjections();

                if (hasUpcomingProjections)
                {
                    txtIndisponible.Visibility = Visibility.Collapsed;
                }
                else
                {
                   // txtIndisponible.Visibility = Visibility.Visible;
                   // txtIndisponible.Text = $"Disponible le {film.DateSortie.Day} {Utils.GetMoisNom(film.DateSortie)} {film.DateSortie.Year}";
                }
            }

            if (await _viewModel.PeutNoter())
            {
                btNoter.Visibility = Visibility.Visible;
            }
            if (!await _viewModel.PeutReserver())
            {
                btReserver.Visibility = Visibility.Collapsed;
                txtIndisponible.Visibility = Visibility.Visible;
                txtIndisponible.Text = $"Aucune projections à venir";
            }
        }

        private void btNoter_Click(object sender, RoutedEventArgs e)
        {
            
        }

       

        private void btReserver_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void UpdateInterface()
        {
            //TODO : Implementer la mise à jour de l'interface
        }
    }
}
