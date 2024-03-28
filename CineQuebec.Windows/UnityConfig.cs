using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace CineQuebec.Windows
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Enregistrement des services
            container.RegisterType<IActeurService, ActeurService>();
            container.RegisterType<ActeurRepository>();
            container.RegisterType<IFilmService, FilmService>();
            container.RegisterType<FilmService>();
            container.RegisterType<IProjectionService, ProjectionService>();
            container.RegisterType<ProjectionService>();
            container.RegisterType<IAbonneService, AbonneService>();
            container.RegisterType<AbonneService>();

            // Enregistrement du conteneur dans le conteneur lui-même
            container.RegisterInstance<IUnityContainer>(container);

            // Enregistrement du conteneur dans le conteneur de ressources de l'application
            Application.Current.Resources.Add("UnityContainer", container);
        }
    }
}
