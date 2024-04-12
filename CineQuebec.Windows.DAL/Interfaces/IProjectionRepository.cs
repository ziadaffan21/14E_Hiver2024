﻿using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesRepositorie
{
    public interface IProjectionRepository
    {
        Task AjouterProjection(Projection projection);

        List<Projection> ReadProjections();

        List<Projection> ReadProjectionsById(object idFilm);
        Task<Projection> GetProjectionByDateAndFilmId(DateTime dateProjection, string titreFilm);
    }
}