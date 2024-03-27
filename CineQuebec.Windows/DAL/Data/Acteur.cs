﻿using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.Exceptions.ActeurExceptions.PrenomEtNom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Acteur : Entity, IActeur
    {

        #region CONSTANTES
        const byte NB_CARACTERE_MAX_NOM = 100;
        const byte NB_CARACTERE_MIN_NOM = 2;
        const byte NB_CARACTERE_MAX_PRENOM = 100;
        const byte NB_CARACTERE_MIN_PRENOM = 5;
        #endregion

        #region ATTRIBUTS
        private string _prenom;
        private string _nom;
        private DateTime _naissance;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new PrenomActeurNullException($"Le prenom ne peut pas etre null");
                if (value.Trim().Length < NB_CARACTERE_MIN_PRENOM || value.Trim().Length > NB_CARACTERE_MAX_PRENOM) throw new PrenomLengthException($"Le prenom doit etre entre {NB_CARACTERE_MIN_PRENOM} et {NB_CARACTERE_MAX_PRENOM}");
                _prenom = value;
            }
        }
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new NomActeurNullException($"Le nom ne peut pas etre null");
                if (value.Trim().Length < NB_CARACTERE_MIN_NOM || value.Trim().Length > NB_CARACTERE_MAX_NOM) throw new NomLengthException($"Le nom doit etre entre {NB_CARACTERE_MIN_NOM} et {NB_CARACTERE_MAX_NOM}");
                _nom = value;
            }
        }
        public DateTime Naissance
        {
            get { return _naissance; }
            set { _naissance = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public Acteur()
        {

        }

        public Acteur(string prenom, string nom, DateTime naissance)
        {
            Prenom = prenom;
            Nom = nom;
            Naissance = naissance;
        }

        #endregion

        #region MÉTHODES
        public override string ToString()
        {
            return $"{Prenom} {Nom}";
        }
        #endregion
    }
}
