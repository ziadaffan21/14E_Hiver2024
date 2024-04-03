﻿using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Services
{
    public class FilmService : IFilmService
    {
        private readonly FilmRepository _filmRepository;
        public FilmService(FilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task AjouterFilm(Film film)
        {
            await _filmRepository.AjouterFilm(film);
        }

        public List<Film> GetAllFilms()
        {
            return _filmRepository.ReadFilms();
        }

        public async Task ModifierFilm(Film film)
        {
            await _filmRepository.ModifierFilm(film);
        }
    }
}