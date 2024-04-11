﻿using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions;
using CineQuebec.Windows.Exceptions.AbonneExceptions;
using CineQuebec.Windows.Ressources.i18n;
using System.Text;
using System.Windows;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour AjoutUser.xaml
    /// </summary>
    public partial class AjoutUser : Window
    {
        private Abonne _abonne;

        private readonly IAbonneService _abonneService;

        public AjoutUser(IAbonneService abonneService)
        {
            InitializeComponent();
            _abonneService = abonneService;
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidationFormulaire())
                {
                    _abonne = new Abonne(txtUsername.Text, DateTime.Now);
                    _abonne.Salt = PasswodHasher.CreateSalt();
                    _abonne.Password = PasswodHasher.HashPassword(txtPassword.Password.ToString(), _abonne.Salt);
                    bool result = await _abonneService.Add(_abonne);
                    if (result)
                    {
                        MessageBox.Show(Resource.ajoutUser, Resource.ajout, MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show(Resource.errorAjoutUser, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);

                        DialogResult = false;
                    }
                }
            }
            catch (ExistingAbonneException ex)
            {
                MessageBox.Show(ex.Message, Resource.existingAbonneTitre, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MongoDataConnectionException ex)
            {
                MessageBox.Show(ex.Message, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(Resource.erreurGenerique, Resource.erreur, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool ValidationFormulaire()
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text.Trim().Length < Abonne.NB_MIN_CARACTERES_USERNAME || txtUsername.Text.Trim().Length > Abonne.NB_MAX_CARACTERES_USERNAME)
                sb.AppendLine($"Le username doit contenir entre {Abonne.NB_MIN_CARACTERES_USERNAME} et {Abonne.NB_MAX_CARACTERES_USERNAME} caractères.");

            if (string.IsNullOrWhiteSpace(txtPassword.Password) || txtPassword.Password.Trim().Length < Abonne.NB_MIN_CARACTERES_PASSWORD || txtPassword.Password.Trim().Length > Abonne.NB_MAX_CARACTERES_PASSWORD)
                sb.AppendLine($"Le mot de passe doit contenir entre {Abonne.NB_MIN_CARACTERES_PASSWORD} et {Abonne.NB_MAX_CARACTERES_PASSWORD} caractères.");

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Errors", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}