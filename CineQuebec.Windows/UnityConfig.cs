using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System.Windows;
using Unity;

namespace CineQuebec.Windows
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Enregistrement des services.
            container.RegisterSingleton<IFilmService, FilmService>();
            container.RegisterSingleton<IFilmRepository, FilmRepository­>();
            container.RegisterSingleton<IProjectionService, ProjectionService>();
            container.RegisterSingleton<IProjectionRepository, ProjectionRepository>();
            container.RegisterSingleton<IAbonneService, AbonneService>();
            container.RegisterSingleton<IAbonneRepository, AbonneRepository>();
            container.RegisterSingleton<IRealisateurRepository, RealisateurRepository>();
            container.RegisterSingleton<IActeurRepository, ActeurRepository>();
            container.RegisterSingleton<IDataBaseUtils, DataBaseUtils>();
            container.RegisterSingleton<IDataBaseSeeder, DataBaseSeeder>();

            // Enregistrement du conteneur dans le conteneur lui-même
            container.RegisterInstance<IUnityContainer>(container);

            // Enregistrement du conteneur dans le conteneur de ressources de l'application
            Application.Current.Resources.Add("UnityContainer", container);
        }
    }
}