﻿using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
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
    /// Logique d'interaction pour FormulaireProjection.xaml
    /// </summary>
    public partial class FormulaireProjection : Window
    {
        private StringBuilder sb = new();

        private readonly FormulaireProjectionViewModel _viewModel;
        private Etat Etat = Etat.Ajouter;

        public FormulaireProjection(IProjectionService projectionService, IFilmService filmService, IEventAggregator eventAggregator, Projection projection = null)
        {
            InitializeComponent();
            _viewModel = new FormulaireProjectionViewModel(projectionService, filmService, eventAggregator,projection);
            DataContext = _viewModel;
            calendrier.DisplayDateStart = DateTime.Now;
            _viewModel.ErrorOcuured += ViewModel_ErrorOccured;
            _viewModel.AjoutModif += ViewModel_AjoutModif;
            Unloaded += AjoutDetailProjection_Unloaded;
            Etat = projection == null ? Etat.Ajouter : Etat.Modifier;
            if (Etat == Etat.Modifier)
            {
                _viewModel.ProjectionTime = projection.Date;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiserFormulaire();
        }

        private void InitialiserFormulaire()
        {
            cboFilm.Focus();
            txtPlace.Focus();

            switch (Etat)
            {
                case Etat.Ajouter:
                    lblTitre.Text = "Ajouter un film";
                    btnAjouterModifier.Content = "Ajouter";
                    break;

                case Etat.Modifier:
                    lblTitre.Text = "Modifier un film";
                    btnAjouterModifier.Content = "Modifier";
                    
                    break;

                default:
                    DialogResult = false;
                    break;
            }   
        }

        private void AjoutDetailProjection_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.AjoutModif -= ViewModel_AjoutModif;
            _viewModel.ErrorOcuured -= ViewModel_ErrorOccured;

        }

        private void ViewModel_AjoutModif(bool etat)
        {
            if (etat)
                MessageBox.Show(Resource.ajoutReussiProjection, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
            //DialogResult = true;
        }

        private void ViewModel_ErrorOccured(string error)
        {
            MessageBox.Show(error, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
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
     

        private void horloge_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            MessageBox.Show("OK");
        }
    }
}