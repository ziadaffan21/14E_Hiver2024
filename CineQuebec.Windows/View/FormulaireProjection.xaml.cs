using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions.ProjectionException;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Ressources.i18n;
using CineQuebec.Windows.ViewModel;
using Prism.Events;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour AjoutDetailProjection.xaml
    /// </summary>
    public partial class AjoutDetailProjection : Window
    {
        private StringBuilder sb = new();

        private readonly FormulaireProjectionViewModel _viewModel;

        public AjoutDetailProjection(IProjectionService projectionService, IFilmService filmService,IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _viewModel=new FormulaireProjectionViewModel(projectionService, filmService, eventAggregator);
            DataContext = _viewModel;
            calendrier.DisplayDateStart = DateTime.Now;
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            if (ValiderForm())
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show(sb.ToString(), Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DateTime GetDateAndTime(DateTime date, DateTime time)
        {
            DateTime newDate = new(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            return newDate;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_viewModel.Projection.PlaceDisponible.ToString());
            MessageBox.Show(_viewModel.Projection.Date.ToString());
            DialogResult = false;
        }

        private bool ValiderForm()
        {
            sb.Clear();
           
            if (calendrier.SelectedDate is null || calendrier.SelectedDate < DateTime.Today)
                sb.AppendLine($"La date sélectionnée doit être plus grande ou égale à {DateTime.Today}.");
            //if (horloge.SelectedTime is null)
            //    sb.AppendLine($"Il faut sélectionner une heure pour la projection.");
            if (cboFilm.SelectedIndex == -1)
                sb.AppendLine($"Vous devez assigner un film");
            if (string.IsNullOrWhiteSpace(txtPlace.Text) || int.Parse(txtPlace.Text) < Projection.NB_PLACE_MIN)
                sb.AppendLine($"Le nombre de place doit être plus grand que {Projection.NB_PLACE_MIN}.");

            if (sb.Length > 0)
                return false;

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiserFormulaireAjout();
        }

        private void InitialiserFormulaireAjout()
        {
            cboFilm.Focus();
            txtPlace.Focus();
        }

        private void horloge_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            MessageBox.Show("OK");
        }
    }
}